using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnTree : Obstacles
{
    [SerializeField] GameObject m_sandTree;
    [SerializeField] GameObject m_dirtTree;
    [SerializeField] GameObject m_grassTree;
    [SerializeField] GameObject m_snowTree;
    [SerializeField] GameObject m_cobblestoneTree;
    [SerializeField] GameObject m_stoneTree;
    [SerializeField] GameObject m_lavaTree;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void SetCurrentTheme(ECurrentTheme _eTheme) 
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        m_rRotationOffset.y = Random.Range(0.0f, 360.0f);

        switch (_eTheme)
        {
            case ECurrentTheme.e_Sand:
                {
                    m_RefToChild = Instantiate(m_sandTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Dirt:
                {
                    m_RefToChild = Instantiate(m_dirtTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Grass:
                {
                    m_RefToChild = Instantiate(m_grassTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Snow:
                {
                    m_RefToChild = Instantiate(m_snowTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_CobbleStone:
                {
                    m_RefToChild = Instantiate(m_cobblestoneTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Stone:
                {
                    m_RefToChild = Instantiate(m_stoneTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Lava:
                {
                    m_RefToChild = Instantiate(m_lavaTree, transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            default:
                break;
        }
    }
}
