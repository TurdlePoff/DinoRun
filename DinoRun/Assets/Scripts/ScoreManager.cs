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
    public float m_fRunSpeed = 0.01f;
    private TextMeshProUGUI m_ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        s_fRunDistance = 0.0f;
        m_ScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only process the score while the raptor is running
        if (m_bIsRunning) {
            // Increase how far we've run
            s_fRunDistance += m_fRunSpeed * Time.deltaTime;

            // Update the score
            m_ScoreText.SetText("Score: " + s_fRunDistance);
        }
    }


    /// <summary>
    /// Checks to see if the player has run to a new best distance
    /// </summary>
    public static void EndRunComparison() {
        // Update the lifetime run distance
        GameManager.UpdatePlayerLifetimeRunDistance(s_fRunDistance);

        // See if the distance from the current run is greater than what is stored
        if (s_fRunDistance >= PlayerPrefs.GetFloat("PlayerBestRunDistance", 0.0f) ){
            // Set the new best run distance
            PlayerPrefs.SetFloat("PlayerBestRunDistance", s_fRunDistance);
        }
    }
}
