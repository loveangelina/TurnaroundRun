using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    private static BoostManager instance;
    public static BoostManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoostManager>();
                if(instance = null)
                {
                    GameObject obj = new GameObject("BoostManager");
                    instance = obj.AddComponent<BoostManager>();
                }
            }
            return instance;
        }
    }

    private List<PlayerController> players = new List<PlayerController>();
    public int currentBoostPlayerIndex;


    void Start()
    {
        List<GameObject> findPlayers = CharacterManager.Instance.characters;
        foreach(GameObject character in findPlayers)
        {
            players.Add(character.GetComponent<PlayerController>());
        }
        currentBoostPlayerIndex = UnityEngine.Random.Range(0, players.Count);//랜덤으로 플레이어 설정
        setCurrentBoostPlayer(currentBoostPlayerIndex);//선택된 플레이어의 부스터 활성화
        Debug.Log(currentBoostPlayerIndex);
    }
    public void setCurrentBoostPlayer(int index) //특정 인덱스의 플레이어만 부스트 사용할수 있게 설정
    {
        foreach (PlayerController player in players) //모든 플레이어 부스트 비활성화
        {
            player.canBoost = false;
        }
        players[index].canBoost = true; //현재 부스트를 사용할수 있는 플레이어만 활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
