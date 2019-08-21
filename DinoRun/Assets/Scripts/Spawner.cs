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

    public void SpawnBlock()
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
            m_iCurrentBlock = 0;
        }

        // If block is < 25 spawn some with slight chance of previous theme
        if(25 > m_iCurrentBlock)
        {
            if(ECurrentTheme.e_Sand != m_eCurrentTheme)
            {
                //GameObject Path = GetPooledObject();
                //if (Path != null)
                //{
                //    Path.transform.position = m_ObjectSpawnLocation.transform.position;
                //    Path.SetActive(true);
                //}
            }
        }
        // If Block is > 75 spawn some with slight chance of next theme


        GameObject Path = GetPooledObject();
        if (Path != null)
        {
            Path.transform.position = m_ObjectSpawnLocation.transform.position;
            Path.SetActive(true);
        }
        print("Spawn");
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
