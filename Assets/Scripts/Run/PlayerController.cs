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
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        PlayerState = State.Idle;
        WaitTime = 0f;
        IsCoolTime = false;
        MaxSpeed = 10f;
        MinSpeed = 1f;      
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerState)
        {
            case State.Idle:
                //3초뒤에 Run상태로 바뀜
                if (WaitTime >= 3f)
                {
                    ChangeState(State.Run);
                    WaitTime = 0;
                }
                else
                {
                    WaitTime += Time.deltaTime;
                }
                break;


            case State.Run:
                //normalSpeed로 달림
                if (WaitTime >= 10f)
                {
                    ChangeState(State.Stop);
                }
                else
                {
                    if (!IsCoolTime)
                    {
                        StartCoroutine(CollTime());
                    }
                    Debug.Log(normarSpeed);
                    rigid.velocity = new Vector3(0, 0, normarSpeed);
                    WaitTime += Time.deltaTime;
                }
                break;


            case State.Stop:
                Debug.Log(PlayerState);
                break;
        }
    }
    void ChangeState(State state)
    {
        PlayerState = state;
    }
    IEnumerator CollTime()
    {
        normarSpeed = Random.Range(MinSpeed, MaxSpeed);
        IsCoolTime = true;
        yield return new WaitForSeconds(2f);
        IsCoolTime = false;
    }
}
