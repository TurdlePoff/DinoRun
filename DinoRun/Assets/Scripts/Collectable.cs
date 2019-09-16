using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int ScoreIncrease = 1;
    public Vector2 m_RotationSpeed = new Vector2(2.0f, 7.0f);

    public GameObject[] Eggs;

    Vector3 positionAboveFloor = new Vector3(0.0f, 2.0f, 0.0f);
    Vector3 positionAboveFloorJump = new Vector3(0.0f, 2.5f, 0.0f);
    Vector3 positionAboveFloorHighJump = new Vector3(0.0f, 3.0f, 0.0f);
    Vector3 positionAbove = new Vector3(0.0f, 1.25f, 0.0f);

    private bool m_bFoundTargetLocation = false;
    private int m_iEggToDisplay;
    private float m_fRotationSpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpEggs();
    }

    private void OnEnable()
    {
        GetComponent<Rigidbody>().useGravity = true;
        m_bFoundTargetLocation = false;

        SetUpEggs();

        m_fRotationSpeed = Random.Range(m_RotationSpeed.x, m_RotationSpeed.y);
    }

    private void SetUpEggs()
    {
        if (0 >= Eggs.Length)
        {
            Debug.LogError("Eggs Array Size = 0");
        }

        m_iEggToDisplay = Random.Range(0, Eggs.Length);

        for(int i = 0; i < Eggs.Length; ++i)
        {
            Eggs[i].SetActive(false);
        }

        Eggs[m_iEggToDisplay].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (-5 > transform.position.y)
        {
            gameObject.SetActive(false);
        }
        transform.Rotate(Vector3.up, m_fRotationSpeed); // Rotate in place
    }

    private void OnTriggerEnter(Collider other)
    {
        if("Player" == other.tag)
        {
            GameManager.s_iScore += ScoreIncrease;

            Eggs[m_iEggToDisplay].SetActive(false);

            gameObject.SetActive(false);
        }
        else if("Collectable" == other.tag)
        {
            // Ignore this object
        }
        else if(!m_bFoundTargetLocation)
        {
            if("Floor" == other.tag)
            {
                int randomNum = Random.Range(0, 2);
                if(0 == randomNum)
                {
                    transform.position = other.transform.position + positionAboveFloor;
                }
                else if(1 == randomNum)
                {
                    transform.position = other.transform.position + positionAboveFloorJump;
                }
                else
                {
                    transform.position = other.transform.position + positionAboveFloor;
                }
            }
            else
            {
                transform.position = other.transform.position + positionAbove;
            }

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            m_bFoundTargetLocation = true;
        }
    }
}
