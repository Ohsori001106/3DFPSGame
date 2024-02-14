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
}
