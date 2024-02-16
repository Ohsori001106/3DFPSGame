using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // 목표: 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다.
    // 필요 속성
    // - 수류탄 프리팹
    public GameObject BombPrefab;
    // - 수류탄 던지는 위치
    public Transform FirePosition;
    // - 수류탄 던지는 파워
    public float TrowPower = 15f;

    public Text BombScoreTextUI;
    public int bombCount;
    public int bombCountMax = 3;

    public Text BulletTextUI;
    
    // 피격 이펙트 오브젝트
    public GameObject bulletEffect;

    // 피격 이펙트 파티클 시스템
    ParticleSystem ps;

    public float Firetime;
    public float FireCooltime = 0.2f;


    public int BulletRemainCount;
    public int BulletMaxCount = 30;


    public int PoolSize = 3;
    public List<Bomb> _BombPool;
    private void Awake()
    {
        _BombPool = new List<Bomb>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bomb = Instantiate(BombPrefab);
            _BombPool.Add(bomb.GetComponent<Bomb>());
            bomb.SetActive(false);
        }
    }
    public void Start()
    {
        BulletRemainCount = BulletMaxCount;
        bombCount = bombCountMax;
        RefreshUI();
        RefreshGunUI();
        Firetime = FireCooltime;

        // 피격 이펙트 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        Firetime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletRemainCount = BulletMaxCount;
            RefreshGunUI();
        }
        /* 수류탄 투척 */
        // 구현 순서:
        // 1. 마우스 오른쪽 버튼을 감지
        if (Input.GetMouseButtonDown(1)&&(bombCount > 0)) // 우클릭은 1 좌클릭은 0
        {
            // 2. 수류탄 던지는 위치에다가 수류탄 생성
            //GameObject bomb = Instantiate(BombPrefab);
            // bomb.transform.position = FirePosition.position;
            //bomb.transform.position = FirePosition.transform.position;
            Bomb bomb = null;
            foreach (Bomb b in _BombPool)
            {
                //만약에 꺼져있다면
                if (b.gameObject.activeInHierarchy == false)
                {
                    bomb = b;
                    break;  //찾았기 때문에 그 뒤까지 찾을 필요가 없다
                }
            }
            bomb.transform.position = FirePosition.transform.position;
            bomb.gameObject.SetActive(true);

            // 3. 시선이 바라보는 방향(카메라가 바라보는 방향 = 카메라의 전방)으로 수류탄 투척
            Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * TrowPower,ForceMode.Impulse); // AddForce(방향 * 힘, 작용 방식)

            bombCount--;
            RefreshUI();

           
        }
        if (Input.GetMouseButton(0)&& BulletRemainCount>0 && Firetime>=FireCooltime)
        {
            // 레이를 생성한 후 발사될 위치와 진행 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 레이가 부딪힌 대상의 정보를 저장할 변수를 생성한다.
            RaycastHit hitInfo = new RaycastHit();
            // 레이를 발사한 후 만일 부딪힌 물체가 있으면 피격 이펙트를 표시한다.
            if (Physics.Raycast(ray, out hitInfo))
            {
                // 피격 이펙트의 위치를 레이가 부딪힌 지점으로 이동시킨다.
                bulletEffect.transform.position = hitInfo.point;

                // 피격 이펙트의 forward 방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                bulletEffect.transform.forward = hitInfo.normal;

                // 피격 이펙트를 플레이한다.
                ps.Play();
                Firetime = 0;
                BulletRemainCount--;
                RefreshGunUI();

            }
        }
        

    }
    
    private void RefreshUI()
    {
        BombScoreTextUI.text = $"{bombCount}/{bombCountMax}";
    }

    private void RefreshGunUI()
    {
        BulletTextUI.text = $"{BulletRemainCount:d2}/{BulletMaxCount}";
    }
}
