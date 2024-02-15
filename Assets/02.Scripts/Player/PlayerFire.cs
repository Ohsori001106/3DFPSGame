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
        bombCount = bombCountMax;
        RefreshUI();
    }
    private void Update()
    {
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
        
        
    }
    
    private void RefreshUI()
    {
        BombScoreTextUI.text = $"{bombCount}/{bombCountMax}";
    }
}
