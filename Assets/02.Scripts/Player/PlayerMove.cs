using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목표: 키보드 방향키(wasd)를 누르면 캐릭터를 바라보는 방향 기준으로 이동시키고 싶다.
    // 속성:
    // - 이동속도
    public float Speed;
    public float MoveSpeed = 5f;
    public float RunSpeed = 10f;

    public float Stamina = 100;
    public const float MaxStamina = 100;

    public float StaminaConsumeSpeed = 33f; // 초당 스태미나 소모량
    public float StaminaChargeSpeed = 50f;  // 초당 스태미나 충전량
    // 구현순서
    // 1. 키 입력 받기
    // 2. '캐릭터가 바라보는 방향'을 기준으로 방향구하기
    // 3. 이동하기


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 키 입력받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2. '캐릭터가 바라보는 방향'을 기준으로 방향구하기
        Vector3 dir = new Vector3(h, 0, v); // 로컬 좌표계 (나만의 동서남북)
        dir.Normalize();
        // Transforms direction from local space to world space.(대충 글로벌좌표를 로컬로 바꿔줌)
        dir = Camera.main.transform.TransformDirection(dir);  // 글로벌 좌표계 (세상의 동서남북)

        // 3. 이동하기
        transform.position += Speed * dir * Time.deltaTime;

        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = RunSpeed;
            Stamina -= StaminaConsumeSpeed * Time.deltaTime;
        }
        else
        {
            Speed = MoveSpeed;
            Stamina += StaminaChargeSpeed * Time.deltaTime;
        }

        if (Stamina <= 0)
        {
            Speed = MoveSpeed;
        }

        Stamina = Mathf.Clamp(Stamina, 0, MaxStamina);
    }
    
}