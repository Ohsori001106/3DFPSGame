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
    private CharacterController _characterController;


    // 목표: 스페이스바를 누르면 캐릭터를 점프하고 싶다.
    // 필요 속성:
    // - 점프 파워 값
    public float JumpPower = 10f;
    // 구현 순서:
    // 1. 만약에 [Spacebar] 버튼을 누르면..
    // 2. 플레이어에게 y축에 있어 점프 파워를 적용한다






    // 목표: 캐릭터에게 중력을 적용하고 싶다.
    // 필요 속성:
    // - 중력 값
    private float _gravity = -20;
    // - 누적할 중력 변수
    private float _yVelocity = 0f;
    // 구현 순서:
    // 1. 중력 가속도가 누적된다.
    // 2. 플레이어에게 y축에 있어 중력을 적용한다.
    


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

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

        // 3-1. 중력 가속도 계산
         _yVelocity = _yVelocity + _gravity *Time.deltaTime;
        // 2. 플레이어에게 y축에 있어 중력을 적용한다.
        dir.y = _yVelocity;

        // 3-2. 이동하기
        //transform.position += Speed * dir * Time.deltaTime;
        _characterController.Move(dir*Speed*Time.deltaTime);

        

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

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CameraManager.Instance.SetCameraMode(CameraMode.FPS);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CameraManager.Instance.SetCameraMode(CameraMode.TPS);
        }


        // 1. 만약에 [Spacebar] 버튼을 누르는 순간 && 땅이면...
        if (Input.GetKeyDown(KeyCode.Space)&&_characterController.isGrounded)
        {
            // 2. 플레이어에게 y축에 있어 점프 파워를 적용한다
            _yVelocity = JumpPower;
        }

    }

}
