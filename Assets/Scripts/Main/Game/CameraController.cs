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
    [SerializeField] CinemachineVirtualCamera punisherCamera2;

    [SerializeField] GameObject firstPlaceCharacter;
    [SerializeField] GameObject boostCharacter;
    [SerializeField] GameObject lastPlaceCharacter;

    GameUIManager gameUIManager;

    void Start()
    {
        gameUIManager = FindObjectOfType<GameUIManager>();
        StartCoroutine(ControlCamera());
    }

    void Update()
    {
        
    }

    IEnumerator ControlCamera()
    {
        // 3�ʰ� ����ϰ� �ִ� �÷��̾ �� ������ 
        waitCamera.gameObject.SetActive(true);
        // TODO :
        // ������ ĳ���� ����ŭ ������ 

        yield return new WaitForSeconds(3f);

        // 3�ʰ� �޸��� �����ϴ� ��� ��ü�� ������
        waitCamera.gameObject.SetActive(false);
        startCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (3�ʰ�) 1������ �޸��� �ִ� ����� ������
        startCamera.gameObject.SetActive(false);
        firstPlaceCharacter = FindFirstPlace();
        firstPlaceCamera.Follow = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.LookAt = firstPlaceCharacter.GetComponent<Transform>();
        firstPlaceCamera.transform.position = new Vector3(firstPlaceCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        firstPlaceCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        // (5�ʰ�) �ν��� �޸��� �ִ� ����� ������ 
        firstPlaceCamera.gameObject.SetActive(false);
        boostCharacter = FindBooster();
        boostCamera.Follow = boostCharacter.GetComponent<Transform>();
        boostCamera.LookAt = boostCharacter.GetComponent<Transform>();
        boostCamera.transform.position = new Vector3(boostCharacter.transform.position.x, 2.91f, firstPlaceCharacter.transform.position.z + 4.1f);
        boostCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        // (3�ʰ�) ��¼��� �����ϴ� ������� ������ 
        boostCamera.gameObject.SetActive(false);
        finalCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);

        // ��Ģ�ڰ� �������� ��Ģ�ڸ� Ȯ���ؼ� ������
        while (!lastPlaceCharacter)
        {
            yield return null;
        }
        Debug.Log("�ݺ��� Ż��");
        yield return new WaitForSeconds(1f);
        finalCamera.gameObject.SetActive(false);
        punisherCamera.LookAt = lastPlaceCharacter.GetComponent<Transform>();
        punisherCamera.transform.position = new Vector3(lastPlaceCharacter.transform.position.x, 2.91f, lastPlaceCharacter.transform.position.z);
        punisherCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);

        punisherCamera.gameObject.SetActive(false);
        punisherCamera2.gameObject.SetActive(true);
        gameUIManager.LooserUI();

    }

    // ���� 1������ �޸��� �ִ� ĳ���͸� ã��
    private GameObject FindFirstPlace()
    {
        return CharacterManager.Instance.SelectFirstPlace();
    }

    // �ν��ͷ� �޸��� �ִ� ĳ���͸� ã��
    private GameObject FindBooster()
    {
        return CharacterManager.Instance.GetBoostCharacter();
    }

    public void SetLastPlaceCharacter(GameObject player)
    {
        lastPlaceCharacter = player;
        Debug.Log("��Ģ�ڴ� " + player.name);
    }
}
