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
    public static bool s_bIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        s_bIsRunning = true; 


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
        float fCurrentDistance = PlayerPrefs.GetFloat("PlayerLifeRunDistance", 0.0f);
        PlayerPrefs.SetFloat("PlayerLifeRunDistance", fCurrentDistance + _fDistance);
        // Check for run achievements
    }

    private static void CheckForRunAchievements() {
        // Obtain a reference to the users achievements
        IAchievement[] achievements = null;
        Social.LoadAchievements(userAchievements => { achievements = userAchievements; });

        // Check longstrider achievements (only if user does not have highest level)

        // Find that achievement
        IAchievement bestLongstrider = null;
        foreach(IAchievement achievement in achievements) {
            if(achievement.id == SpeedyBoiAchievements.achievement_longstrider_x10000) {
                bestLongstrider = achievement;
                break;
            }
        }

        // If the achievement cannot be found, escape
        if (bestLongstrider == null) {
            Debug.LogError("ERROR: Lifetime run achievements could not been updated.");
            return;
        }

        // Next check that the achievement has not been met
        if (!bestLongstrider.completed) {
            // Find lifetime run distance
            float fLifeRun = PlayerPrefs.GetFloat("PlayerLifeRunDistance", 0.0f);

            // Compare to thresholds
            if(fLifeRun >= 10000.0f) {
                Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x10000, 100.0, (bool success) => { });
            }
            else if(fLifeRun >= 1000.0f) {
                Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x1000, 100.0, (bool success) => { });
            }
            else if(fLifeRun >= 100.0f) {
                Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x100, 100.0, (bool success) => { });
            }else if(fLifeRun >= 0.0f) {
                Social.ReportProgress(SpeedyBoiAchievements.achievement_baby_steps, 100.0, (bool success) => { });
            }
        }



    }

    /// <summary>
    /// Performs all necessary setup at runtime for GooglePlay Services
    /// </summary>
    /// <returns></returns>
    public static bool InitialiseGooglePlay() {
        // Activate PlayGames platform and debugging
        //PlayGamesPlatform.Activate();
        //PlayGamesPlatform.DebugLogEnabled = true;

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
