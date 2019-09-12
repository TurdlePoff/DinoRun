using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int ScoreIncrease = 5;

    Vector3 positionAbove = new Vector3(0.0f, 2.0f, 0.0f);

    private bool m_bFoundTargetLocation = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().useGravity = true;
        m_bFoundTargetLocation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (-5 > transform.position.y)
        {
            gameObject.SetActive(false);
        }
        transform.Rotate(Vector3.up, 10.0f); // Rotate in place
    }

    private void OnTriggerEnter(Collider other)
    {
        if("Player" == other.tag)
        {
            GameManager.s_iScore += ScoreIncrease;
            gameObject.SetActive(false);
        }
        else if(!m_bFoundTargetLocation)
        {
            transform.position = other.transform.position + positionAbove;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            m_bFoundTargetLocation = true;
        }
    }
}
