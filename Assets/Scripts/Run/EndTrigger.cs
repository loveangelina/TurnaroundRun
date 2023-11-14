using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
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
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                ComePlayer.Add(player);//플레이어 추가
                player.ChangeState(PlayerController.State.Stop);//스탑상태로 변경

                if (ComePlayer.Count ==TotalPlayerCount)//모든 플레이어가 결승선에 들어오면
                {
                    DefeatPlayer();//패배자 선정
                }
            }   
        }
    }
    public void DefeatPlayer()//패배자 선정 함수
    {
        if(ComePlayer.Count > 0)
        {
            PlayerController lastPlayer = ComePlayer[ComePlayer.Count-1];//마지막으로 들어온 플레이어 찾기
            foreach(PlayerController player in ComePlayer)
            {
                if(player != lastPlayer)
                {
                    Debug.Log("승리!");
                    player.animator.SetBool("Victory", true);//나머지 플레이어 승리모션
                }
            }
            lastPlayer.animator.SetBool("Die", true);//플레이어 죽음 모션
            Debug.Log("죽음");
        }
    }
    

}
