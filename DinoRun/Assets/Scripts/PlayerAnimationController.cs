using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAnimationController : MonoBehaviour
{
    public GameObject m_rGameOverPanel;
    public TextMeshProUGUI m_strScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        GameManager.s_bIsRunning = false;

        if (null != m_strScore) {
            m_strScore.text = GameManager.s_iScore.ToString();
        }

        //if (null != m_DeathEffect) {
        //    m_DeathEffect.Play();
        //}


        m_rGameOverPanel.SetActive(true);
        GameOverMenu.OnGameOver();
    }
}
