using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float m_fMovementSpeed = -3;

    private Vector3 m_vMovementAddition = Vector3.zero;
    private Vector3 m_AddPosition = new Vector3(600.0f, 0.0f, 0.0f);
    private GroundMatChange ground;

    // Start is called before the first frame update
    void Start()
    {
        m_vMovementAddition = new Vector3(m_fMovementSpeed, 0.0f, 0.0f);

        ground = GetComponent<GroundMatChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.s_bIsRunning)
        {
            return;
        }
        
        transform.position = transform.position + m_vMovementAddition * Time.deltaTime;

        if ("Ground" == gameObject.tag)
        {
            if(-200 > transform.position.x)
            {
                transform.position += m_AddPosition;

                if(null != ground)
                {
                    ground.ChangeMaterial();
                }
            }
        }
        else if (-5 > transform.position.x)
        {
            if (GetComponent<PathwayBlocks>())
            {
                Vector3 tempPos = GetComponentInParent<Spawner>().SendToStart(gameObject);
                transform.position = new Vector3(Mathf.RoundToInt(tempPos.x), tempPos.y, tempPos.z);
            }
            else if (GetComponent<Obstacles>())
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
