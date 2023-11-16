using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
    PlayerController lastPlayer;
    PlayerController player;
    int TotalPlayerCount;
    private void Start()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();//�÷��̾���Ʈ�� �������ִ� �ֵ� ã�Ƽ� �迭�� ����
        TotalPlayerCount = allPlayers.Length; //����÷��̾� ��
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// �÷��̾� �±׸� ������ ������
        {
            player = other.gameObject.GetComponent<PlayerController>();
            player.GetComponent<FootStep>().SetIsFoot(false); // ����� ������ �߼Ҹ� ����
            if (player != null)
            {
                ComePlayer.Add(player);//�÷��̾� �߰�
                SoundMgr.Instance.ComeIn();//������ �Ҹ�
                if (ComePlayer.Count == TotalPlayerCount)//��� �÷��̾ ��¼��� ������
                {
                    StartCoroutine(DefeatPlayerAfterDelay());
                }
            }
        }
    }
    
    IEnumerator DefeatPlayerAfterDelay()
    {
        yield return new WaitForSeconds(0.8f);

        if (ComePlayer.Count > 0)
        {
            lastPlayer = ComePlayer[ComePlayer.Count - 1];
            foreach (PlayerController player in ComePlayer)
            {
                if (player != lastPlayer)
                {
                    Debug.Log("�¸�!");
                }
            }

            lastPlayer.ChangeState(PlayerController.State.Stop);
            lastPlayer.animator.SetBool("Die", true);

            // CameraController�� lastPlayer ����
            StartCoroutine(SetCameraLastPlayer(lastPlayer));
            StartCoroutine(SoundMgr.Instance.Nagative());
        }
    }
    
    IEnumerator SetCameraLastPlayer(PlayerController lastPlayer)
    {
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.SetLastPlaceCharacter(lastPlayer.gameObject);
        }
        yield return new WaitForSeconds(3f);
        Debug.Log("����");
        GameObject LastPlayer = Instantiate(lastPlayer.gameObject,new Vector3(0,0,80),Quaternion.identity);
        LastPlayer.GetComponent<PlayerController>().enabled = false;
    }
}
