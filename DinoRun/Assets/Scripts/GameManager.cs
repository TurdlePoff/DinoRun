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
    public static bool s_bIsPlayerAuthenticated = false;
    public static bool s_bIsRunning = false;
    public static int s_iScore = 0;
    public static int s_iZone = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InitialiseGooglePlay();

        s_bIsRunning = true; 


        // Find a reference to the player controller
        m_rPlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if (!m_rPlayer) {
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
        CheckForRunAchievements();
    }

    /// <summary>
    /// Checks and updates run achievements as needed
    /// </summary>
    private static void CheckForRunAchievements() {
        // If the highest run achievement has been completed, we can leave
        if (!IsAchievementComplete(SpeedyBoiAchievements.achievement_longstrider_x10000)) {
            // Find lifetime run distance
            float fLifeRun = PlayerPrefs.GetFloat("PlayerLifeRunDistance", 0.0f);

            // Compare to thresholds
            if(fLifeRun >= 10000.0f) {
                Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x10000, 100.0, (bool success) => { });
            }
            else if(fLifeRun >= 1000.0f) {
                // Check the achievement is not complete
                if (!IsAchievementComplete(SpeedyBoiAchievements.achievement_longstrider_x1000)) {
                    // Mark complete
                    Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x1000, 100.0, (bool success) => { });
                }
            }
            else if(fLifeRun >= 100.0f) {
                // Check for completion
                if (!IsAchievementComplete(SpeedyBoiAchievements.achievement_longstrider_x100)) {
                    Social.ReportProgress(SpeedyBoiAchievements.achievement_longstrider_x100, 100.0, (bool success) => { });
                }
            }else if(fLifeRun >= 0.0f) {
                if (!IsAchievementComplete(SpeedyBoiAchievements.achievement_baby_steps)) {
                    Social.ReportProgress(SpeedyBoiAchievements.achievement_baby_steps, 100.0, (bool success) => { });
                }
            }
        }

    }

    /// <summary>
    /// Checks if an achievement is completed
    /// </summary>
    /// <param name="_id">The unique ID of the achievement</param>
    /// <returns></returns>
    private static bool IsAchievementComplete(string _id) {
        // Get achievement reference
        IAchievement achievement = GetAchievement(_id);
        
        // Check that it exists
        if(achievement != null) {
            return achievement.completed;
        }

        return false;
    }

    /// <summary>
    /// Checks if an achievement with a given ID has been completed.
    /// </summary>
    /// <param name="_id"></param>
    /// <returns></returns>
    private static IAchievement GetAchievement(string _id) {
        if (!Social.localUser.authenticated) {
            Debug.LogError("ERROR: Cannot retrieve achievement - user is not authenticated.");
            return null;
        }

        // Obtain a reference to the users achievements
        IAchievement[] achievements = null;
        Social.LoadAchievements(userAchievements => { achievements = userAchievements; });

        // If achievements are found
        if(achievements != null) {
            // Find that achievement
            foreach (IAchievement achievement in achievements) {
                if (achievement.id == _id) {
                    return achievement;
                }
            }
        }


        return null;
    }

    /// <summary>
    /// Performs all necessary setup at runtime for GooglePlay Services
    /// </summary>
    /// <returns></returns>
    public static bool InitialiseGooglePlay() {
        // Activate PlayGames platform and debugging
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;

        // Attempt to authenticate the player
        if (!s_bIsPlayerAuthenticated) {
            PlayGamesPlatform.Instance.Authenticate((bool bSuccess) => { //Social.localUser
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

    public static void GooglePlayGamesInitialisation() {
        // Activate PlayGames platform and debugging
        PlayGamesClientConfiguration config = new
    PlayGamesClientConfiguration.Builder()
    .Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.DebugLogEnabled = true;

        // Attempt to authenticate the player
        if (!s_bIsPlayerAuthenticated) {
            PlayGamesPlatform.Instance.Authenticate((bool bSuccess) => { //Social.localUser
                if (bSuccess) {
                    Debug.Log("Log in successful.");
                    s_bIsPlayerAuthenticated = true;
                } else {
                    Debug.LogError("ERROR: Unable to complete user authentication.");
                }
            });
        }
    }

    public static void OpenAchievements() {
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                Debug.Log("You've successfully logged in.");
                Social.ShowAchievementsUI();
            } else {
                Debug.Log("Login failed for some reason.");
            }
        });
    }

    public static void OpenLeaderboard() {
        Social.localUser.Authenticate((bool success)=>{
            if (success) {
                Debug.Log("Successfully opened leaderboard");
                Social.ShowLeaderboardUI();
            } else {
                Debug.Log("Count not authenticate user to open leaderboard");
            }
        });
    }

    public static void AddScoreToLeaderboard(float _fScore) {
        Social.ReportScore((long)_fScore, SpeedyBoiAchievements.leaderboard_high_score, (bool success) => { });
    }

    public static void CheckForAd() {
        // Update ad count

        int iPlayCount = (PlayerPrefs.GetInt("PlayCount", 0) + 1) % 3;
        PlayerPrefs.SetInt("PlayCount", iPlayCount);
        if (iPlayCount == 0) {
            Adverts.s_Instance.SkippableVideoAd();
        }
    }

}
