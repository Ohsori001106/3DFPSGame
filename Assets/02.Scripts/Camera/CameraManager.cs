using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    FPS,
    TPS,
    TOP,

}
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public FPSCamera fpsCameraScript;
    public TPSCamera tpsCameraScript;

    public CameraMode Mode = CameraMode.FPS;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCameraMode(CameraMode mode)
    {
        Mode = mode;

        //fpsCameraScript.enabled = (mode == CameraMode.FPS);
        //tpsCameraScript.enabled = (mode == CameraMode.TPS);
    }
    public void SwitchToFPS()
    {
        fpsCameraScript.enabled = true;
        tpsCameraScript.enabled = false;
    }

    public void SwitchToTPS()
    {
        fpsCameraScript.enabled = false;
        tpsCameraScript.enabled = true;
    }
    public static bool Focus
    {
        get
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                return false;
            }

            Vector3 mousePosition = Input.mousePosition;
            bool isScreen = mousePosition.x < 0 ||
                            mousePosition.x > Screen.width ||
                            mousePosition.y < 0 ||
                            mousePosition.y > Screen.height;
            return !isScreen;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        
    }
}
