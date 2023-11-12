using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waitCamera;
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera firstPlaceCamera;

    // TODO : 1등인 캐릭터 찾아서 현재 변수 대체
    [SerializeField] GameObject firstPlaceCharacter;

    void Start()
    {
        StartCoroutine(ControlCamera());
    }

    void Update()
    {
        
    }

    IEnumerator ControlCamera()
    {
        // 3초간 대기하고 있는 플레이어를 쭉 보여줌 
        waitCamera.gameObject.SetActive(true);
        // TODO :
        // 생성된 캐릭터 수만큼 움직임 

        yield return new WaitForSeconds(3f);

        // 3초간 달리는 모습 전체를 보여줌
        waitCamera.gameObject.SetActive(false);
        startCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (3초간) 1등으로 달리고 있는 사람을 보여줌
        startCamera.enabled = false;
        firstPlaceCamera.Follow = FindFirstPlace();
        firstPlaceCamera.enabled = true;

        // (2초간) 1, 2등 주변으로 보여줌 

        // (2초간) 1등 전체로 보여줌

        // 부스터 된 사람은 최대속도로 달리므로, 
        // 5초간 최대속도로 달릴 때 결승선이 나올 수 있는 지점에서 
        // 부스터 된 사람을 계속 보여주며 
        // 결승선에 가까워지면 결승선 카메라로 변환되고
        //
        // 부스터 될 사람 선택하기 전까지는 1등 전체로 보여주기 
    }

    // 현재 1등으로 달리고 있는 캐릭터를 찾음
    private Transform FindFirstPlace()
    {
        return firstPlaceCharacter.GetComponent<Transform>();
    }

    IEnumerator ShowWaitingCharaters()
    {
        yield return null;
    }
}
