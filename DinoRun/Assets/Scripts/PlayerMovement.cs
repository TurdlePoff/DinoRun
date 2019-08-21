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
    public float m_fPlayerJumpHeight = 300.0f;
    public float m_fPlayerHighJumpHeight = 30.0f;

    // RigidBody
    private Rigidbody m_Myrigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Ste up the default values
        m_Myrigidbody = GetComponent<Rigidbody>();
        m_vHalfScale = transform.localScale / 2.0f;
        m_vDefaultScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Scales the ducking
        if(m_bDucking)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vHalfScale, ref m_vScaleVelocity, 0.5f);

        }
        else
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vDefaultScale, ref m_vScaleVelocity, 0.5f);
        }
    }

    // Normal Jumping
    public void PlayerJump()
    {
        print("Jump");
        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerJumpHeight);
    }

    // High Jumping
    public void PlayerHighJump()
    {
        print("High Jump");
        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight);
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
