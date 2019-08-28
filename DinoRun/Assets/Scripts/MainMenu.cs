using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;
    private bool m_bIdle = true;

    // Start is called before the first frame update
    void Start()
    {
        m_rAnimator.SetBool("MainMenu", true);
    }

    public void OpenAchievements()
    {
        //put code here
    }

    public void OpenSettings()
    {
        //put code here
    }

    public void OpenShop()
    {
        //put code here
    }

    public void PlayGame()
    {
        //put code here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenLeaderboard()
    {
        //put code here
    }

    public void ShareToTwitter()
    {
        //put code here
    }

    public void InteractWithDino()
    {
        m_rAnimator.SetTrigger("Jump");
    }
}
