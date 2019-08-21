using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchManager : MonoBehaviour
{
    // Singleton instance
    private static TouchManager s_rInstance = null;

    [SerializeField]
    private Camera m_rCamera = null;
    private Touch m_LastTouch;
    private Touch[] m_Touches;

    [Header("Swipe Events")]
    public UnityEvent onSwipeUp;
    public UnityEvent onSwipeDown;

    private void Awake() {
        if (!s_rInstance) {
            s_rInstance = this;
        }
    }

    private void Update() {
        // Check if a touch event is happening
        if (IsTouching(out m_Touches)) {
            m_LastTouch = Input.GetTouch(0);
            
            // Look for the end of a touch event
            if(m_LastTouch.phase == TouchPhase.Ended) {
                // See is a swipe happened
                Vector2 swipe = m_LastTouch.deltaPosition;

                // Assess magnitude
                if(swipe.sqrMagnitude > 0.0f) {
                    // Assess y direction for swipe
                    if(swipe.y > 0.0f) {
                        onSwipeUp.Invoke();
                    } else {
                        onSwipeDown.Invoke();
                    }
                }
            }
        }
    }

    // Finds the world space position of the zeroth index touch
    public static Vector3 GetTouchInWorldSpace() {
        if (!s_rInstance) {
            return Vector3.zero;
        }else if(s_rInstance.m_rCamera == null) {
            return Vector3.zero;
        }
        // Cast screen to world point
        return s_rInstance.m_rCamera.ScreenToWorldPoint(Input.touches[0].position);
    }

    // Checks if a touch event is happening
    public static bool IsTouching() {
        return (Input.touchCount > 0);
    }

    // As above, but populates an array with touch information
    public static bool IsTouching(out Touch[] _touches) {
        _touches = Input.touches; // assuming array is empty even if no touches
        return (Input.touchCount > 0);
    }
}
