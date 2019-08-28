using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Ducking
    private bool m_bDucking = false;
    public float m_fDuckingTimeDelay = 1.0f;
    private Vector3 m_vHalfScale = Vector3.zero;
    private Vector3 m_vDefaultScale = Vector3.zero;
    private Vector3 m_vScaleVelocity = Vector3.zero;

    // Jumping
    private bool m_bJumpEnd = true;
    public float m_fPlayerJumpHeight = 5.0f;
    public float m_fPlayerHighJumpHeight = 7.5f;

    // RigidBody
    private Rigidbody m_Myrigidbody;
    private Vector3 m_vStartXZ = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Ste up the default values
        m_Myrigidbody = GetComponent<Rigidbody>();
        //m_Myrigidbody.centerOfMass = m_Myrigidbody.centerOfMass + new Vector3(0.0f, 0.0f, 0.5f);
        m_vHalfScale = transform.localScale / 2.0f;
        m_vDefaultScale = transform.localScale;
        m_vStartXZ = transform.position;

        GetComponent<Animator>().SetTrigger("StartRunning");
        GetComponent<Animator>().speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(m_vStartXZ.x, transform.position.y, m_vStartXZ.z);
        // Scales the ducking
        if (m_bDucking)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vHalfScale, ref m_vScaleVelocity, 0.5f);

        }
        else
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vDefaultScale, ref m_vScaleVelocity, 0.5f);
        }

        // Land
        if (2.0f >= GetComponent<Animator>().speed && m_bJumpEnd)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
            {
                if (hit.transform.gameObject.tag.Contains("Floor"))
                {
                    GetComponent<Animator>().speed = 1.0f;
                }
            }
        }

        if(transform.position.y < 0.75f)
        {
            // Stop everything in scene
            // Menu pops up
            GameManager.s_bIsRunning = false;
        }
    }

    // Normal Jumping
    public void PlayerJump()
    {
        GetComponent<Animator>().SetTrigger("Jump");
        GetComponent<Animator>().speed = 1.0f;

        print("Jump Attempt");
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        {
            if (hit.transform.gameObject.tag.Contains("Floor"))
            {
                m_Myrigidbody.AddForce(Vector3.up * m_fPlayerJumpHeight, ForceMode.Impulse);
            }
        }

        m_bJumpEnd = false;
        Invoke("EndJump", 1.0f);
    }

    // High Jumping
    public void PlayerHighJump()
    {
        GetComponent<Animator>().SetTrigger("Jump");
        GetComponent<Animator>().speed = 1.0f;

        print("High Jump Attempt");
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        {
            if (hit.transform.gameObject.tag.Contains("Floor"))
            {
                m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
            }
        }

        m_bJumpEnd = false;
        Invoke("EndJump", 2.0f);
    }
    // End of Jump
    private void EndJump()
    {
        m_bJumpEnd = true;
    }

    // Ducking
    public void PlayerDuck()
    {
        m_bDucking = true;
        Invoke("StopDuck", 1.0f);
    }
    // Stop Ducking
    private void StopDuck()
    {
        m_bDucking = false;
    }
}
