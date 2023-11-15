using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
    public PlayerController lastPlayer;
    public PlayerController player;
    int TotalPlayerCount;
    float delayTime;
    private void Start()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();//플레이어컨트롤 가지고있는 애들 찾아서 배열에 넣음
        TotalPlayerCount = allPlayers.Length; //모든플레이어 수
        delayTime = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// 플레이어 태그를 가지고 있으면
        {
            player = other.gameObject.GetComponent<PlayerController>();
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
    private void SetCameraLastPlayer(PlayerController lastPlayer)
    {
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.SetLastPlaceCharacter(lastPlayer.gameObject);
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
            Debug.Log("죽음");

            // CameraController에 lastPlayer 전달
            SetCameraLastPlayer(lastPlayer);
        }
    }
}
