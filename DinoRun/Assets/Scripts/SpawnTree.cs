using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnTree : Obsticles
{

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
        switch (_eTheme)
        {
            case ECurrentTheme.e_Sand:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/GreenLightTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Dirt:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/GreenMedTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Grass:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/GreenDarkTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Snow:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/BlueLightTreeTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_CobbleStone:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/BlueMedTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Stone:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/GreyTreeRoundTiny.prefab", typeof(GameObject)), transform);
                    m_RefToChild.transform.position = m_RefToChild.transform.position + m_vOffSet;
                    m_RefToChild.transform.rotation = m_RefToChild.transform.rotation * m_rRotationOffset;
                    m_RefToChild.transform.localScale = m_vScale;
                    break;
                }
            case ECurrentTheme.e_Lava:
                {
                    m_RefToChild = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Model/BlocksAndTreesPack/RoundTrees/RoundTinyTrees/RedTreeRoundTiny.prefab", typeof(GameObject)), transform);
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
