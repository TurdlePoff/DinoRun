using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class Spawner : MonoBehaviour
{
    // Number of Zones Passed
    private int m_iNumberOfZones = 0;

    // Theme
    public int m_iNumberOfBlocksBetweenThemes = 100;
    private int m_iCurrentBlock = 0;
    private ECurrentTheme m_eCurrentTheme = ECurrentTheme.e_Sand;

    // Pooling Pathway
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool = 100;
    public GameObject m_ObjectSpawnLocation;


    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool, transform);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }


        for (int i = 0; i < pooledObjects.Count; i++)
        {
            GameObject Path = GetPooledObject();
            if (null != Path)
            {
                m_iCurrentBlock += 1;
                Path.transform.position = m_ObjectSpawnLocation.transform.position;
                Path.SetActive(true);
                m_ObjectSpawnLocation.transform.position = new Vector3(m_ObjectSpawnLocation.transform.position.x + 1, m_ObjectSpawnLocation.transform.position.y, m_ObjectSpawnLocation.transform.position.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ECurrentTheme GetCurrentTheme()
    {
        return m_eCurrentTheme;
    }

    public Vector3 SendToStart(GameObject thisObject)
    {
        m_iCurrentBlock += 1;

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
            m_iNumberOfZones += 1;
            m_iCurrentBlock = 0;
        }

        int iRandomChanceOfOtherTheme = 0;
        int iRandomNumber = Random.Range(0, 100);
        // Closer to 0 or 100 = greater chance of next theme 
        // 25-75 = current theme only


        // If block is < 25 spawn some with slight chance of previous theme
        if (25 > m_iCurrentBlock)
        {
            int iCurrentPercentage = 25 - m_iCurrentBlock;
            iCurrentPercentage *= 4;

            if(iRandomNumber < iCurrentPercentage)
            {
                iRandomChanceOfOtherTheme = -1;
            }

            if (ECurrentTheme.e_Sand != m_eCurrentTheme)
            {
                thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(m_eCurrentTheme + iRandomChanceOfOtherTheme);
            }
            else // Sand = current theme
            {
                if (0 == iRandomChanceOfOtherTheme || 0 == m_iNumberOfZones)
                {
                    thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(m_eCurrentTheme); 
                }
                else
                {
                    thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(ECurrentTheme.e_Lava);
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
                thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(m_eCurrentTheme + iRandomChanceOfOtherTheme);
            }
            else
            {
                if (0 == iRandomChanceOfOtherTheme)
                {
                    thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(m_eCurrentTheme); 
                }
                else
                {
                    thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(ECurrentTheme.e_Sand);
                }
            }
        }
        else
        {
            thisObject.GetComponent<PathwayBlocks>().SetCurrentTheme(m_eCurrentTheme);
        }

        return m_ObjectSpawnLocation.transform.position;
    }

    public GameObject GetPooledObject()
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //3   
        return null;
    }
}
