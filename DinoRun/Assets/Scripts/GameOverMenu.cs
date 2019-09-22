using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject m_rPausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Retry()
    {
        //put code here
        GameManager.CheckForAd();
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

    public void ShareToFacebook()
    {
        //put code here
    }

    public void ShareToTwitter()
    {
        //put code here
    }

    public static void OnGameOver() {
        ScoreManager.EndRunComparison();
        GameManager.CheckForAd();
    }
}
