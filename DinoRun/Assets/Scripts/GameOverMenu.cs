using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void OpenSettings()
    {
        //put code here
    }

    public void OpenShop()
    {
        //put code here
    }

    public void ShareToFacebook()
    {
        //put code here
    }

    public void ShareToTwitter()
    {
        //put code here
    }
}
