using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    
    public float normarSpeed { get ;set; }
    public float MaxSpeed {  get; set; }
    public float MinSpeed { get; set; }
    
    public float WaitTime { get; set; }
    public bool IsCoolTime { get; private set; }

    public enum State
    {
        Idle,Run,Stop,Boost
    }

    State PlayerState;

    public Boost.BoostEvent onBoost;

    private float plusSpeed;

    private Rigidbody rigid;

    public bool canBoost;

    private Animator animator;

    private float BoostTime;

    public GameObject BoostParticlePrefab;//부스터 파티클 게임 오브젝트 
    private GameObject BoostParticleInstance;//그 오브젝트를 복사한 인스턴스 
    // Start is called before the first frame update
    private void Awake()
    {
        onBoost = new Boost.BoostEvent();
        onBoost.AddListener(BoostPlayerSpeed);
        Application.targetFrameRate = 60; //60프레임 고정(버벅거림 방지, Fixed Timestep을 (1 / 60) = 0.016667 로 설정)
    }

    private void BoostPlayerSpeed()//부스터 함수
    {
        plusSpeed = MaxSpeed - normarSpeed;
        normarSpeed = normarSpeed + plusSpeed; //스피드를 최대로 함
    }
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerState = State.Idle;
        WaitTime = 0f;
        BoostTime = 0f;
        IsCoolTime = false;
        MaxSpeed = 10f;
        MinSpeed = 3f;
        BoostParticleInstance = Instantiate(BoostParticlePrefab, transform.position, Quaternion.identity, transform); //부스터 프리팹을 플레이어 중앙에 복사 > 부스터 인스턴스 대입
        BoostParticleInstance.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (PlayerState)
        {
            case State.Idle://아이들상태
                if (WaitTime >= 3f)
                {
                    ChangeState(State.Run);//3초후 런상태
                    WaitTime = 0;
                }
                else
                {
                    WaitTime += Time.deltaTime;
                }
                break;


            case State.Run://달리는상태
                //normalSpeed로 달림
                //Debug.Log("달리기");
                if (WaitTime > 15f) //시간이 15초 이상이면
                {
                    ChangeState(State.Stop);
                }
               
                else
                {
                    if (canBoost == true && WaitTime >= 7f)// 부스트 사용가능상태 이고 시간이 7초 이상이면
                    {
                        Debug.Log("부스터 상태로 변환");
                        ChangeState(State.Boost);//부스터 상태로 변환
                    }
                    if (!IsCoolTime)//시간셋팅 쿨타임이 아닌경우
                    {
                        StartCoroutine(CollTime());
                    }                  
                    rigid.velocity = new Vector3(0, 0, normarSpeed);

                    WaitTime += Time.deltaTime; //시간 증가
                }
                break;


            case State.Stop://멈춘상태
                Debug.Log("골인!");
                animator.SetFloat("Speed", 0);//블렌드 트리 파라미터 Speed값 0으로 설정(아이들 상태 모션)
                break;

            case State.Boost://부스트 상태일때
                Debug.Log("부스트");
                WaitTime += Time.deltaTime; //시간 증가
                if (BoostTime >= 3f)
                {
                    BoostParticleInstance.SetActive(false);//만들어진 파티클 비활성화                   
                    normarSpeed = normarSpeed - plusSpeed; //스피드 원래 스피드
                    rigid.velocity = new Vector3(0, 0, normarSpeed);
                    animator.SetFloat("Speed", normarSpeed);//애니메이션 속도도 원래대로
                    canBoost = false;//부스트 비활성화
                    ChangeState(State.Run);//달리기 상태로 변환
                }
                else
                {
                    BoostParticleInstance.transform.rotation = Quaternion.Euler(0f,180f,0f);//부스터 회전 
                    BoostParticleInstance.SetActive(true);//부스터 활성화
                    BoostPlayerSpeed();//최대 속도
                    rigid.velocity = new Vector3(0, 0, normarSpeed);
                    animator.SetFloat("Speed", 10);//애니메이션도 최대속도
                    BoostTime += Time.deltaTime;//시간증가
                }              
                break;
        }
    }
    void ChangeState(State state)
    {
        PlayerState = state; //상태변경
    }
    IEnumerator CollTime()
    {
        normarSpeed = Random.Range(MinSpeed, MaxSpeed);//표준 스피드 랜덤하게 설정(최소,최대)
        animator.SetFloat("Speed", normarSpeed);//애니메이터 블렌드 트리 파라미터 Speed설정
        IsCoolTime = true;
        yield return new WaitForSeconds(2f);//2초뒤 속도 변경 , 랜덤으로 바꿔도 됨
        IsCoolTime = false;
    }
}
