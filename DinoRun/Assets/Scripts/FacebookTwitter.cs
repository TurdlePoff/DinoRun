using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookTwitter : MonoBehaviour
{

    public void OpenTwitter()
    {
        print("OpenTwitter");
        string twitterAddress = "http://twitter.com/intent/tweet";
        string message = "GET THIS EPIC ENDLESS RUNNER";//text string
        string descriptionParameter = "Dino Dasher by Dinosaur Forestry Club";
        string appStoreLink = "https://play.google.com/apps/testing/com.DinosaurForestryClub.SpeedyBoi"; // Immediatly after i in Boi -> ? id = com.DinosaurForestryClub.SpeedyBoi

        Application.OpenURL(twitterAddress + "?text=" +
        WWW.EscapeURL(message + "\n" +
        descriptionParameter + "\n" +
        appStoreLink));
        // UnityWebRequest
    }
}
