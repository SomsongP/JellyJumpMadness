using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Interactions;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public delegate void TouchEvent(Vector2 screenPosition, float time);
    public event TouchEvent OnTouchDown;
    public event TouchEvent OnTouchHold;
    private TouchControls touchControls;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        touchControls.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += HandleTouchDown;
    }
    private void OnDisable()
    {
        touchControls.Disable();
        EnhancedTouchSupport.Disable();
        Touch.onFingerDown -= HandleTouchDown;
    }
    private void HandleTouchDown(Finger finger)
    {
        OnTouchDown?.Invoke(finger.screenPosition, Time.time);
        Debug.Log("Finger down at: " + finger.screenPosition);
    }
    //private void HandleTouchDowns(InputAction.CallbackContext context)
    //{
    //    if (context.interaction is HoldInteraction)
    //    {
    //        Debug.Log("Hold triggered!");
    //        // ทำฟังก์ชันแบบ hold เช่น ลอยตัว
    //    }
    //    else
    //    {
    //        Debug.Log("Simple press (tap)!");
    //        // กระโดดปกติ
    //    }
    //}



}
