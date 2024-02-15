using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // 목표: 마우스를 조작하면 플레이어를 좌우방향으로 회전 시키고 싶다.
    public float RotationSpeed = 200;
    private float _mx = 0;
    // 필요 속성:
    // - 회전 속도
    private void Update()
    {
        // 1. 마우스 입력(drag) 받는다
        float mouseX = Input.GetAxis("Mouse X");
        // 2. 마우스 입력값을 x만큼 누적한다ㅛ
        _mx += mouseX * RotationSpeed * Time.deltaTime;
        //_mx = Mathf.Clamp(_my, -270f, 270f);

        transform.eulerAngles = new Vector3(0f, _mx, 0f);

        if (!CameraManager.Focus)
        {
            return;
        }
    }
    public void ResetX()
    {
        _mx = 0;
    }
}
