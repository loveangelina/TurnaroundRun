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
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();//플레이어컨트롤 가지고있는 애들 찾아서 배열에 넣음
        TotalPlayerCount = allPlayers.Length; //모든플레이어 수
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// 플레이어 태그를 가지고 있으면
        {
            player = other.gameObject.GetComponent<PlayerController>();
            player.GetComponent<FootStep>().SetIsFoot(false); // 결승점 들어오면 발소리 제거
            if (player != null)
            {
                ComePlayer.Add(player);//플레이어 추가
                SoundMgr.Instance.ComeIn();//들어오는 소리
                if (ComePlayer.Count == TotalPlayerCount)//모든 플레이어가 결승선에 들어오면
                {
                    StartCoroutine(DefeatPlayerAfterDelay());
                }
            }
        }
    }
    
    IEnumerator DefeatPlayerAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (ComePlayer.Count > 0)
        {
            lastPlayer = ComePlayer[ComePlayer.Count - 1];
            foreach (PlayerController player in ComePlayer)
            {
                if (player != lastPlayer)
                {
                    Debug.Log("승리!");
                }
            }

            lastPlayer.ChangeState(PlayerController.State.Stop);
            lastPlayer.animator.SetBool("Die", true);

            // CameraController에 lastPlayer 전달
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
        Debug.Log("생성");
        GameObject LastPlayer = Instantiate(lastPlayer.gameObject,new Vector3(0,0,80),Quaternion.identity);
        LastPlayer.GetComponent<PlayerController>().enabled = false;
    }
}
