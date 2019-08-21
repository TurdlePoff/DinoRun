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
    [SerializeField][Tooltip("The minimum distance a touch must move to count as a swipe.")]
    private float m_fSwipeDistance = 10.0f;
    private Touch m_LastTouch;
    private Touch[] m_Touches;
    private bool m_bIsSwiping = false;

    [Header("Swipe Events")]
    public UnityEvent onSwipeUp;
    public UnityEvent onSwipeDown;
    public UnityEvent onTap;

    private void Awake() {
        if (!s_rInstance) {
            s_rInstance = this;
        }
    }

    private void Update() {
        // Check if a touch event is happening
        if (IsTouching(out m_Touches)) {
            m_LastTouch = Input.GetTouch(0);

            switch (m_LastTouch.phase) {

                // A tap
                case TouchPhase.Began: {
                    //print("Tap event");
                    //onTap.Invoke();
                    break;
                }

                // Finger movement should be tracked

                case TouchPhase.Moved: {
                    m_bIsSwiping = true;
                    break;
                }

                // Check for end of a touch event 
                case TouchPhase.Ended: {
                    // If we were swiping, check for swipe events
                    if (m_bIsSwiping) {
                        CheckForSwipeEvent();
                        // We are now no longer swiping
                        m_bIsSwiping = false;
                    } else {
                        onTap.Invoke();
                    }
                                       
                    break;
                }

                // A swipe is cancelled 
                case TouchPhase.Canceled: {
                    m_bIsSwiping = false;
                    break;
                }

                default:break;
            }

        }
    }

    /// <summary>
    /// Assesses if a swipe event has happened in the up or down direction, and invokes respective functions
    /// </summary>
    private void CheckForSwipeEvent() {
        Vector2 swipe = m_LastTouch.deltaPosition;

        // Assess magnitude
        if (swipe.sqrMagnitude >= m_fSwipeDistance) {
            // Assess y direction for swipe
            if (swipe.y > 0.0f) {
                onSwipeUp.Invoke();
            } else {
                onSwipeDown.Invoke();
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
