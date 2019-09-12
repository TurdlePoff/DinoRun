using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float m_fMovementSpeed = -2;

    private Vector3 m_vMovementAddition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_vMovementAddition = new Vector3(m_fMovementSpeed, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.s_bIsRunning)
        {
            return;
        }
        
        transform.position = transform.position + m_vMovementAddition * Time.deltaTime;

        if (-5 > transform.position.x)
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
        }
        
    }
}
