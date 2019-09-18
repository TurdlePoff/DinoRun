using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMatChange : MonoBehaviour
{
    public int m_FloorNumber = -100;

    private Renderer m_MyMaterial;

    private void Start()
    {
        m_MyMaterial = GetComponent<Renderer>();
    }

    public void ChangeMaterial()
    {
        if(-100 == m_FloorNumber)
        {
            return;
        }

        if(null == m_MyMaterial)
        {
            Debug.LogError("Material Not Valid");
        }

        m_FloorNumber += 3;

        m_FloorNumber %= 7;

        switch (m_FloorNumber)
        {
            case 0:
                m_MyMaterial.material = (Material)Resources.Load("Material/Sand", typeof(Material));
                break;
            case 1:
                m_MyMaterial.material = (Material)Resources.Load("Material/Dirt", typeof(Material));
                break;
            case 2:
                m_MyMaterial.material = (Material)Resources.Load("Material/Grass", typeof(Material));
                break;
            case 3:
                m_MyMaterial.material = (Material)Resources.Load("Material/Snow", typeof(Material));
                break;
            case 4:
                m_MyMaterial.material = (Material)Resources.Load("Material/CobbleStone", typeof(Material));
                break;
            case 5:
                m_MyMaterial.material = (Material)Resources.Load("Material/Stone", typeof(Material));
                break;
            case 6:
                m_MyMaterial.material = (Material)Resources.Load("Material/Lava", typeof(Material));
                break;
            default:
                break;
        }
    }
}
