using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera waitCamera;
    [SerializeField] CinemachineVirtualCamera startCamera;
    [SerializeField] CinemachineVirtualCamera firstPlaceCamera;
    [SerializeField] CinemachineVirtualCamera boostCamera;
    [SerializeField] CinemachineVirtualCamera finalCamera;
    [SerializeField] CinemachineVirtualCamera punisherCamera;

    [SerializeField] GameObject firstPlaceCharacter;
    [SerializeField] GameObject boostCharacter;
    [SerializeField] GameObject lastPlaceCharacter;

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

        // 3초간 달리기 시작하는 모습 전체를 보여줌
        waitCamera.gameObject.SetActive(false);
        startCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (3초간) 1등으로 달리고 있는 사람을 보여줌
        startCamera.gameObject.SetActive(false);
        firstPlaceCharacter = FindFirstPlace();
        firstPlaceCamera.Follow = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.LookAt = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.transform.position = new Vector3(firstPlaceCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        firstPlaceCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (5초간) 부스터 달리고 있는 사람을 보여줌 
        firstPlaceCamera.gameObject.SetActive(false);
        boostCharacter = FindBooster();
        boostCamera.Follow = boostCharacter.GetComponent<Transform>();
        boostCamera.LookAt = boostCharacter.GetComponent<Transform>();
        boostCamera.transform.position = new Vector3(boostCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        boostCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        // (3초간) 결승선에 도착하는 사람들을 보여줌 
        boostCamera.gameObject.SetActive(false);
        finalCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        // 벌칙자가 정해지면 벌칙자를 확대해서 보여줌
        while (lastPlaceCharacter == null)
        {
            if (!lastPlaceCharacter)
                break;
        }
        yield return new WaitForSeconds(1f);
        finalCamera.gameObject.SetActive(false);
        punisherCamera.LookAt = lastPlaceCharacter.GetComponent<Transform>();
        //punisherCamera.transform.position = new Vector3(lastPlaceCharacter.transform.position.x, 2.91f, lastPlaceCharacter.transform.position.z);
        punisherCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);

        // 부스터 된 사람은 최대속도로 달리므로, 
        // 5초간 최대속도로 달릴 때 결승선이 나올 수 있는 지점에서 
        // 부스터 된 사람을 계속 보여주며 
        // 결승선에 가까워지면 결승선 카메라로 변환되고
        //
        // 부스터 될 사람 선택하기 전까지는 1등 전체로 보여주기 
    }

    // 현재 1등으로 달리고 있는 캐릭터를 찾음
    private GameObject FindFirstPlace()
    {
        return CharacterManager.Instance.SelectFirstPlace();
    }

    // 부스터로 달리고 있는 캐릭터를 찾음
    private GameObject FindBooster()
    {
        return CharacterManager.Instance.GetBoostCharacter();
    }

    public void SetLastPlaceCharacter(GameObject player)
    {
        lastPlaceCharacter = player;
    }
}
