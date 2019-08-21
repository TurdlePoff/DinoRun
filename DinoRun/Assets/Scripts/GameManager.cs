using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GameManager : MonoBehaviour
{
    // Runtime variables
    private PlayerMovement m_rPlayer;
    private static bool s_bIsPlayerAuthenticated = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find a reference to the player controller
        GameObject rPlayer = GameObject.Find("Player");
        if (rPlayer) {
            m_rPlayer = rPlayer.GetComponent<PlayerMovement>();
        } else {
            Debug.LogError("ERROR: GameManager could not find player.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Used to update the player's lifetime run distance
    /// </summary>
    /// <param name="_fDistance">The distance covered from the most recent run</param>
    public static void UpdatePlayerLifetimeRunDistance(float _fDistance) {
        float fCurrentDistance = PlayerPrefs.GetFloat("PlayerRunDistance", 0.0f);
        PlayerPrefs.SetFloat("PlayerRunDistance", fCurrentDistance + _fDistance);
        // Check for run achievements
    }

    private static void CheckForRunAchievements() {

    }

    /// <summary>
    /// Performs all necessary setup at runtime for GooglePlay Services
    /// </summary>
    /// <returns></returns>
    public static bool InitialiseGooglePlay() {
        // Activate PlayGames platform and debugging
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;

        // Attempt to authenticate the player
        if (!s_bIsPlayerAuthenticated) {
            Social.localUser.Authenticate((bool bSuccess) => {
                if (bSuccess) {
                    Debug.Log("Log in successful.");
                    s_bIsPlayerAuthenticated = true;
                } else {
                    Debug.LogError("ERROR: Unable to complete user authentication.");
                }
            });
        }


        return false;
    }
}
