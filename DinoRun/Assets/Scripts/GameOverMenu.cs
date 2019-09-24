using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject m_rPausePanel;
    [SerializeField] Toggle m_rAudioToggle;
    private bool m_bToggleAssigned = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rAudioToggle.isOn = GameManager.GetIsAudioOn();
        AudioListener.pause = m_rAudioToggle.isOn;
        m_bToggleAssigned = true;
    }
    public void Retry()
    {
        //put code here
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        //put code here
        SceneManager.LoadScene(0);
    }

    public void OpenAchievements()
    {
        //put code here
        GameManager.OpenAchievements();
    }

    public void OpenSettings(bool _open)
    {
        //Open pause panel and pause the game if bool is true
        m_rPausePanel.SetActive(_open);
        GameManager.s_bIsRunning = !_open;
    }
    
    public void OpenLeaderboards()
    {
        GameManager.OpenLeaderboard();
    }
    
    public void MuteAudio()
    {
        if (!m_bToggleAssigned)
            return;

        GameManager.MuteAudio();
    }

    public static void OnGameOver() {
        ScoreManager.EndRunComparison();
        GameManager.CheckForAd();
    }
}
