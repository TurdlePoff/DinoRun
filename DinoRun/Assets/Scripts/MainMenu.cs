using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator m_rAnimator;
    private bool m_bIdle = true;
    private IAPManager m_rIAP;
    private bool m_adTest = false;
    public Text m_DebugText;

    // Start is called before the first frame update
    void Start()
    {
        // Initialise GooglePlay
        if (!GameManager.s_bIsPlayerAuthenticated || !Social.localUser.authenticated) {
            GameManager.GooglePlayGamesInitialisation();
        }


        m_rAnimator.SetBool("MainMenu", true);
        m_rIAP = GetComponent<IAPManager>();

        // remove shop button if ads have been removed
        CheckForShopButton();

    }

    private void Update() {
        //if (!GameManager.s_bIsPlayerAuthenticated) {
        //    GameManager.InitialiseGooglePlay();
        //    Debug.LogWarning("Attempting to initialise Google Play Services");
        //}
    }

    private void CheckForShopButton() {
        if(PlayerPrefs.GetInt("NoAds", 0) == 1) {
            GameObject shopButton = GameObject.Find("ShopButton");
            if (shopButton) {
                Debug.Log("Shop Exists!");
                //shopButton.SetActive(false);
            } else {
                Debug.Log("Could not find shop button");
            }
        }
    }

    public void OpenAchievements()
    {
        //put code here
        GameManager.OpenAchievements();
    }

    public void OpenSettings()
    {
        //put code here
    }

    public void OpenShop()
    {
        //put code here
        
    }

    public void RemoveAds()
    {
        if(!m_adTest)
        {
            m_adTest = true;
            m_rIAP.BuyProductID(IAPManager.s_RemoveAds);
        }
    }

    public void PlayGame()
    {
        //put code here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenLeaderboard()
    {
        GameManager.OpenLeaderboard();
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
