using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public enum ECurrentTheme
{
    e_Sand = 0,
    e_Dirt,
    e_Grass,
    e_Snow,
    e_CobbleStone,
    e_Stone,
    e_Lava,
}
public enum EEvents
{
    e_MissingBlock = 0,
    e_Tree,
}

public class Spawner : MonoBehaviour
{
    // Number of Zones Passed
    private int m_iMinObsticleSpawnRate = 10;
    private int m_iMaxObsticleSpawnRate = 80;
    public Slider m_BlockProgression;
    public GameObject m_ZoneOBJ;
    public GameObject m_ScoreOBJ;
    private TextMeshProUGUI m_strZone;
    private TextMeshProUGUI m_strScore;

    // Theme
    public int m_iNumberOfBlocksBetweenThemes = 100;
    private int m_iCurrentBlock = 0;
    private ECurrentTheme m_eCurrentTheme = ECurrentTheme.e_Sand;

    // Pooling Pathway
    public List<GameObject> pooledPathway;
    public GameObject Pathway;
    public int pathwayAmountToPool = 100;
    public GameObject m_ObjectSpawnLocation;
    [SerializeField] float m_fDistanceBetweenBlocks = 2.0f;

    // Pooling Obsticle
    public List<GameObject> pooledObsticles;
    public GameObject ObsticleObjects;
    public int obsticleAmountToPool = 55;
    private Vector3 m_vObsticleOffSet = new Vector3(0.0f, 1.5f, 0.0f);
    private Quaternion m_ObsticleRotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);
    
    // Pooling Collectables
    public List<GameObject> pooledCollectables;
    public GameObject CollectableObject;
    public int CollectableAmountToPool = 100;
    private Vector3 m_vCollectableOffSet = new Vector3(-10.0f, 40.0f, 0.0f);
    private Quaternion m_CollectableRotation = new Quaternion(0.0f, 180.0f, 0.0f, 0.0f);

    // Events
    bool m_bDidEventOccurLastBlock = false;
    bool m_bDidEventOccurLastBlock2 = false;
    bool m_bBlockMissing = false;
    Vector3 m_vMissingBlockOffset = new Vector3(0.0f, 0.0f, -100.0f);


    // Start is called before the first frame update
    void Start()
    {
        GameManager.s_iZone = 0;
        GameManager.s_iScore = 0;

        m_strZone = m_ZoneOBJ.GetComponent<TextMeshProUGUI>();
        if (null != m_strZone)
        {
            m_strZone.text = GameManager.s_iZone.ToString();
            print("IZone Success: " + m_strZone.text);
        }
        else
        {
            print("Failed");
        }
        m_strScore = m_ScoreOBJ.GetComponent<TextMeshProUGUI>();
        if (null != m_strScore)
        {
            m_strScore.text = GameManager.s_iScore.ToString();
            print("iScore Success: " + m_strScore.text);
        }
        else
        {
            print("Failed");
        }

        pooledPathway = new List<GameObject>();
        for (int i = 0; i < pathwayAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(Pathway, transform);
            obj.SetActive(false);
            pooledPathway.Add(obj);
        }

        pooledObsticles = new List<GameObject>();
        for (int i = 0; i < obsticleAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(ObsticleObjects, m_ObjectSpawnLocation.transform.position + m_vObsticleOffSet, m_ObsticleRotation, transform);
            obj.SetActive(false);
            obj.transform.localScale *= 0.8f;
            pooledObsticles.Add(obj);
        }

        pooledCollectables = new List<GameObject>();
        for (int i = 0; i < CollectableAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(CollectableObject, m_ObjectSpawnLocation.transform.position + m_vCollectableOffSet, m_CollectableRotation, transform);
            obj.SetActive(false);
            pooledCollectables.Add(obj);
        }

        // Spawn Pathway
        int iSafeZone = 10;
        for (int i = 0; i < pooledPathway.Count; i++)
        {
            GameObject Path = GetPooledPathway();
            if (null != Path)
            {
                m_iCurrentBlock += 1;
                Path.transform.position = m_ObjectSpawnLocation.transform.position;
                Path.SetActive(true);

                if (0 > iSafeZone)
                {
                    RandomEventOccur();

                    if (-3 > iSafeZone)
                    {
                        SpawnCollectable();
                    }
                }
                iSafeZone -= 1;

                m_ObjectSpawnLocation.transform.position = new Vector3(m_ObjectSpawnLocation.transform.position.x + m_fDistanceBetweenBlocks, m_ObjectSpawnLocation.transform.position.y, m_ObjectSpawnLocation.transform.position.z);

                if (m_bBlockMissing)
                {
                    Path.transform.position += m_vMissingBlockOffset;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (null != m_strScore)
        {
            m_strScore.text = GameManager.s_iScore.ToString();
        }
    }

    public ECurrentTheme GetCurrentTheme()
    {
        return m_eCurrentTheme;
    }

    public Vector3 SendToStart(GameObject thisObject)
    {
        m_iCurrentBlock += 1;
        
        m_BlockProgression.value = m_iCurrentBlock;

        // Change themes
        if (m_iNumberOfBlocksBetweenThemes <= m_iCurrentBlock)
        {
            if (ECurrentTheme.e_Lava != m_eCurrentTheme)
            {
                m_eCurrentTheme += 1;
            }
            else
            {
                m_eCurrentTheme = ECurrentTheme.e_Sand;
            }
            GameManager.s_iZone += 1;
            if (null != m_strZone)
            {
                m_strZone.text = GameManager.s_iZone.ToString();
            }
            m_iCurrentBlock = 0;
        }

        /// Current Theme
        ChangeBlocksTheme(thisObject);

        // Random Event
        RandomEventOccur();

        //Spawn Collectable
        SpawnCollectable();

        if(m_bBlockMissing)
        {
            return m_ObjectSpawnLocation.transform.position + m_vMissingBlockOffset;
        }

        return m_ObjectSpawnLocation.transform.position;
    }
    private void ChangeBlocksTheme(GameObject thisObject)
    {
        int iRandomChanceOfOtherTheme = 0;
        int iRandomNumber = Random.Range(0, 100);
        // Closer to 0 or 100 = greater chance of next theme 
        // 25-75 = current theme only

        PathwayBlocks currentObjectPathwayBlock = thisObject.GetComponent<PathwayBlocks>();
        // If block is < 25 spawn some with slight chance of previous theme
        if (25 > m_iCurrentBlock)
        {
            int iCurrentPercentage = 25 - m_iCurrentBlock;
            iCurrentPercentage *= 4;

            if (iRandomNumber < iCurrentPercentage)
            {
                iRandomChanceOfOtherTheme = -1;
            }

            if (ECurrentTheme.e_Sand != m_eCurrentTheme)
            {
                if (null != currentObjectPathwayBlock)
                {
                    currentObjectPathwayBlock.SetCurrentTheme(m_eCurrentTheme + iRandomChanceOfOtherTheme);
                }
            }
            else // Sand = current theme
            {
                if (0 == iRandomChanceOfOtherTheme || 0 == GameManager.s_iZone)
                {
                    if (null != currentObjectPathwayBlock)
                    {
                        currentObjectPathwayBlock.SetCurrentTheme(m_eCurrentTheme);
                    }
                }
                else
                {
                    if (null != currentObjectPathwayBlock)
                    {
                        currentObjectPathwayBlock.SetCurrentTheme(ECurrentTheme.e_Lava);
                    }
                }
            }
        }
        // If Block is > 75 spawn some with slight chance of next theme
        else if (75 > m_iCurrentBlock)
        {
            int iCurrentPercentage = m_iCurrentBlock - 75;
            iCurrentPercentage *= 4;

            if (iRandomNumber < iCurrentPercentage)
            {
                iRandomChanceOfOtherTheme = 1;
            }

            if (ECurrentTheme.e_Lava != m_eCurrentTheme)
            {
                if (null != currentObjectPathwayBlock)
                {
                    currentObjectPathwayBlock.SetCurrentTheme(m_eCurrentTheme + iRandomChanceOfOtherTheme);
                }
            }
            else
            {
                if (0 == iRandomChanceOfOtherTheme)
                {
                    if (null != currentObjectPathwayBlock)
                    {
                        currentObjectPathwayBlock.SetCurrentTheme(m_eCurrentTheme);
                    }
                }
                else
                {
                    if (null != currentObjectPathwayBlock)
                    {
                        currentObjectPathwayBlock.SetCurrentTheme(ECurrentTheme.e_Sand);
                    }
                }
            }
        }
        else
        {
            if (null != currentObjectPathwayBlock)
            {
                currentObjectPathwayBlock.SetCurrentTheme(m_eCurrentTheme);
            }
        }
    }

    private void RandomEventOccur()
    {
        int iRandomChanceOfEvent = Random.Range(0, 100);
        m_bBlockMissing = false;

        if (!m_bDidEventOccurLastBlock && !m_bDidEventOccurLastBlock2)
        {
            if(iRandomChanceOfEvent < GameManager.s_iZone + m_iMinObsticleSpawnRate && iRandomChanceOfEvent < m_iMaxObsticleSpawnRate)
            {
                EventToOccur();
                m_bDidEventOccurLastBlock = true;
                m_bDidEventOccurLastBlock2 = true;
            }
        }
        else if(m_bDidEventOccurLastBlock2)
        {
            m_bDidEventOccurLastBlock2 = false;
        }
        else
        {
            m_bDidEventOccurLastBlock = false;
        }
    }
    private void EventToOccur()
    {
        int iRandomEventToOccur = Random.Range(0, 2); // Last number is the number of events
        EEvents eEvent = (EEvents)iRandomEventToOccur;
        switch(eEvent)
        {
            case EEvents.e_MissingBlock:
                {
                    m_bBlockMissing = true;
                    print("BlockMissing");
                    break;
                }
            case EEvents.e_Tree:
                {
                    GameObject obsticle = GetPooledObsticles();
                    if(null == obsticle) { return; }
                    obsticle.SetActive(true);
                    obsticle.GetComponent<SpawnTree>().SetCurrentTheme(m_eCurrentTheme);
                    obsticle.transform.position = m_ObjectSpawnLocation.transform.position + m_vObsticleOffSet;
                    print("SpawnTree");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void SpawnCollectable()
    {
        GameObject collectable = GetPooledCollectables();
        if (null == collectable) { return; }
        collectable.SetActive(true);
        collectable.transform.position = m_ObjectSpawnLocation.transform.position + m_vCollectableOffSet;
    }

    public GameObject GetPooledPathway()
    {
        //1
        for (int i = 0; i < pooledPathway.Count; i++)
        {
            //2
            if (!pooledPathway[i].activeInHierarchy)
            {
                return pooledPathway[i];
            }
        }
        //3   
        return null;
    }
    public GameObject GetPooledObsticles()
    {
        //1
        for (int i = 0; i < pooledObsticles.Count; i++)
        {
            //2
            if (!pooledObsticles[i].activeInHierarchy)
            {
                return pooledObsticles[i];
            }
        }
        //3   
        return null;
    }
    public GameObject GetPooledCollectables()
    {
        //1
        for (int i = 0; i < pooledCollectables.Count; i++)
        {
            //2
            if (!pooledCollectables[i].activeInHierarchy)
            {
                return pooledCollectables[i];
            }
        }
        //3   
        return null;
    }
}
