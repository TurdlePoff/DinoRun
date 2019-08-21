using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookTwitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenTwitter()
    {
        print("OpenTwitter");
        string twitterAddress = "http://twitter.com/intent/tweet";
        string message = "GET THIS AWERSOME GAME";//text string
        string descriptionParameter = "Punchy Punch";
        string appStoreLink = "https://play.google.com/store/apps/details? id = com.growlgamesstudio.pizZapMania";

        Application.OpenURL(twitterAddress + "?text=" +
        WWW.EscapeURL(message + "\n" +
        descriptionParameter + "\n" +
        appStoreLink));
        // UnityWebRequest
    }
}
