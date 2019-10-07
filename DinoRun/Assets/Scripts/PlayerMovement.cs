using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // Ducking
    private bool m_bDucking = false;
    [SerializeField] float m_fDuckingTimeDelay = 1.0f;
    private Vector3 m_vHalfScale = Vector3.zero;
    private Vector3 m_vDefaultScale = Vector3.zero;
    private Vector3 m_vScaleVelocity = Vector3.zero;
    [SerializeField] private TextMeshProUGUI m_strScore;

    // Jumping
    private bool m_bJumpEnd = true;
    [SerializeField] float m_fPlayerJumpHeight = 5.0f;
    [SerializeField] float m_fPlayerHighJumpHeight = 7.5f;

    // RigidBody
    private Rigidbody m_Myrigidbody;
    private Animator m_animator;
    private Vector3 m_vStartXZ = Vector3.zero;
    [SerializeField] GameObject m_rGameOverPanel;

    private bool m_bPlayerIsDead = false;
    public bool isDead { get { return m_bPlayerIsDead; } }

    public AudioSource m_JumpEffect;
    public AudioSource m_DeathEffect;
    public AudioSource m_Background;
    public AudioClip m_BackgroundGameplay;
    public AudioClip m_BackgroundGameover;

    // Start is called before the first frame update
    void Start()
    {
        m_Background.clip = m_BackgroundGameplay;
        m_Background.Play();

        // Ste up the default values
        m_Myrigidbody = GetComponent<Rigidbody>();
        m_rGameOverPanel.SetActive(false);

        //m_Myrigidbody.centerOfMass = m_Myrigidbody.centerOfMass + new Vector3(0.0f, 0.0f, 0.5f);
        m_vHalfScale = transform.localScale / 2.0f;
        m_vDefaultScale = transform.localScale;
        m_vStartXZ = transform.position;

        m_animator = GetComponentInChildren<Animator>();
        m_animator.SetTrigger("StartRunning");
        m_bPlayerIsDead = false;

        if (null != m_strScore)
        {
            m_strScore.text = GameManager.s_iScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(m_vStartXZ.x, transform.position.y, m_vStartXZ.z);
        // Scales the ducking
        //if (m_bDucking)
        //{
        //    transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vHalfScale, ref m_vScaleVelocity, 0.5f);
        //}
        //else
        //{
        //    transform.localScale = Vector3.SmoothDamp(transform.localScale, m_vDefaultScale, ref m_vScaleVelocity, 0.5f);
        //}

        // Land
        //if ( m_bJumpEnd)
        //{
        //    RaycastHit hit;
        //    // Does the ray intersect any objects excluding the player layer
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        //    {
        //        if (hit.transform.gameObject.tag.Contains("Floor"))
        //        {
        //            m_animator.speed = 1.0f;
        //        }
        //    }
        //}

        //If dino nfalls down hole
        if(transform.position.y < 0.75f && !m_bPlayerIsDead)
        {
            // Stop everything in scene
            // Menu pops up
            GameManager.s_bIsRunning = false;
            StartCoroutine(GameOver());
        }
    }

    public Animator GetAnimator()
    {
        return m_animator;
    }

    // Normal Jumping
    public void PlayerJump()
    {
        if (m_bPlayerIsDead)
            return;

        m_animator.SetTrigger("Jump");

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        {
            if (hit.transform.gameObject.tag.Contains("Floor"))
            {
                m_Myrigidbody.AddForce(Vector3.up * m_fPlayerJumpHeight, ForceMode.Impulse);
                m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
                m_JumpEffect.Play();
            }
        }
        //else if (Physics.Raycast(transform.position + Vector3.right * 0.1f, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        //{
        //    if (hit.transform.gameObject.tag.Contains("Floor"))
        //    {
        //        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
        //        m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
        //        m_JumpEffect.Play();
        //    }
        //}
        //else if (Physics.Raycast(transform.position - Vector3.right * 0.1f, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        //{
        //    if (hit.transform.gameObject.tag.Contains("Floor"))
        //    {
        //        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
        //        m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
        //        m_JumpEffect.Play();
        //    }
        //}

        m_bJumpEnd = false;
        Invoke("EndJump", 1.0f);
    }

    // High Jumping
    public void PlayerHighJump()
    {
        if (m_bPlayerIsDead)
            return;

        m_animator.SetTrigger("Jump");

        print("High Jump Attempt");
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        {
            if (hit.transform.gameObject.tag.Contains("Floor"))
            {
                m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
                m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
                m_JumpEffect.Play();
            }
        }
        //else if(Physics.Raycast(transform.position + Vector3.right * 0.1f, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        //{
        //    if (hit.transform.gameObject.tag.Contains("Floor"))
        //    {
        //        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
        //        m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
        //        m_JumpEffect.Play();
        //    }
        //}
        //else if (Physics.Raycast(transform.position - Vector3.right * 0.1f, transform.TransformDirection(-Vector3.up), out hit, 0.25f))
        //{
        //    if (hit.transform.gameObject.tag.Contains("Floor"))
        //    {
        //        m_Myrigidbody.AddForce(Vector3.up * m_fPlayerHighJumpHeight, ForceMode.Impulse);
        //        m_JumpEffect.pitch = Random.Range(0.9f, 1.15f);
        //        m_JumpEffect.Play();
        //    }
        //}

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

    public IEnumerator GameOver()
    {
        // Find player and perform animation
        m_animator.SetTrigger("Death");
        if (null != m_DeathEffect)
        {
            m_DeathEffect.Play();
        }
        m_bPlayerIsDead = true;

        m_Background.Stop();
        m_Background.clip = m_BackgroundGameover;
        m_Background.Play();

        GameManager.s_bIsRunning = false;

        if (null != m_strScore)
        {
            m_strScore.text = GameManager.s_iScore.ToString();
        }

        if (null != m_DeathEffect)
        {
            m_DeathEffect.Play();
        }

        yield return new WaitForSeconds(1.0f);
        print("GAMEOVER");
        m_rGameOverPanel.SetActive(true);
        GameOverMenu.OnGameOver();
    }
}
