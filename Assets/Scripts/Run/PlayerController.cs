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
        Idle,Run,Stop
    }

    State PlayerState;

    private Rigidbody rigid;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60; //60프레임 고정(버벅거림 방지, Fixed Timestep을 (1 / 60) = 0.016667 로 설정)
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerState = State.Idle;
        WaitTime = 0f;
        IsCoolTime = false;
        MaxSpeed = 10f;
        MinSpeed = 3f;      
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
                if (WaitTime >= 15f) //시간이 15초 이상이면
                {
                    ChangeState(State.Stop);
                }
                else
                {
                    if (!IsCoolTime)//시간셋팅 쿨타임이 아닌경우
                    {
                        StartCoroutine(CollTime());
                    }                  
                    rigid.velocity = new Vector3(0, 0, normarSpeed);

                    WaitTime += Time.deltaTime; //시간 증가
                }
                break;


            case State.Stop://멈춘상태
                animator.SetFloat("Speed", 0);//블렌드 트리 파라미터 Speed값 0으로 설정(아이들 상태 모션)
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
