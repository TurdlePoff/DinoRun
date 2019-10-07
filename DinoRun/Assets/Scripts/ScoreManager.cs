using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
#pragma warning disable IDE0044 // Add readonly modifier
    // Score
    private static float s_fPlayerScore = 0;
    public static float fPlayerScore { get { return s_fPlayerScore; } set { s_fPlayerScore = value; } }
    private bool m_bIsRunning = false;
    private static float s_fRunDistance = 0.0f;
    public float m_fRunSpeed = 3.0f;
    public PlayerMovement m_rPlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        s_fRunDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Only process the score while the raptor is running
        if (!m_rPlayerMovement.isDead) {
            // Increase how far we've run
            s_fRunDistance += m_fRunSpeed * Time.deltaTime;          
        }
    }

    /// <summary>
    /// Used to reset the run distance
    /// </summary>
    public static void ResetRunDistance() {
        s_fRunDistance = 0.0f;
    }

    /// <summary>
    /// Checks to see if the player has run to a new best distance
    /// </summary>
    public static void EndRunComparison() {
#if UNITY_ANDROID
        // Update the lifetime run distance
        GameManager.UpdatePlayerLifetimeRunDistance(s_fRunDistance);
        GameManager.AddScoreToLeaderboard(s_fPlayerScore);

        // See if the distance from the current run is greater than what is stored
        if (s_fRunDistance >= PlayerPrefs.GetFloat("PlayerBestRunDistance", 0.0f) ){
            // Set the new best run distance
            PlayerPrefs.SetFloat("PlayerBestRunDistance", s_fRunDistance);
        }
#endif
    }
}
