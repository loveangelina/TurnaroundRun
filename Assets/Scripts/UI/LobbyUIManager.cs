using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LobbyUIManager : MonoBehaviour
{
    public Toggle[] chartoggles;
    public Button btnStart;
    private float prefabSpacing = 2f; // 프리팹 간격

    public GameObject pnlSelected;

    [SerializeField] List<GameObject> prefabs;              // 전체 프리팹 캐릭터들
    [SerializeField] List<GameObject> selectedCharacter;    // 선택된 캐릭터
    [SerializeField] List<int> selectedToggleIndexes;       // 선택된 캐릭터들의 인덱스
    [SerializeField] List<float> characterPosition;         // 선택된 캐릭터들의 생성 위치

    public List<int> GetSelectedToggleIndexes()
    {
        return selectedToggleIndexes;    
    }

    // *** GetSelectedToggleIndexes() 사용법 ***

    // 다른 스크립트에서 호출하는 부분
    //List<int> selectedIndexes = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

    // 선택된 토글의 인덱스 목록을 사용
    //foreach (int index in selectedIndexes)
    //{
    //    Debug.Log("선택된 토글 인덱스: " + index);
    //}

    private void Start()
    {
        foreach (Toggle toggle in chartoggles)
        {
            toggle.onValueChanged.AddListener(delegate { SoundMgr.Instance.PlayButtonClickSound(); });
        }

        selectedCharacter = new List<GameObject>();
        characterPosition = new List<float>();
    }

    public void SelectChar()
    {
        for (int i = 0; i < chartoggles.Length; i++)
        {
            if (chartoggles[i].isOn)
            {
                // 선택된 토글 인덱스를 리스트에 추가
                if (!selectedToggleIndexes.Contains(i))
                {
                    selectedToggleIndexes.Add(i);
                }

                DestroyPrefabForToggle();
            }
            else
            {
                 // 선택 해제된 토글 인덱스를 리스트에서 제거
                selectedToggleIndexes.Remove(i);

                // 토글이 선택되지 않으면 해당 인덱스로 프리팹을 삭제하는 함수 호출
                DestroyPrefabForToggle();
            }
        }
    }

    // 토글 선택 시 프리팹 생성 함수
    private void GeneratePrefabForToggle()
    {
        InitializeList();

        int selectedNum = selectedToggleIndexes.Count;
        float firstPosition = 0 - 1f * (selectedNum - 1);
        characterPosition.Add(firstPosition);

        // selectedIndex 값에 따라서 위치 정해진 list 만들기 
        for (int i = 1; i < selectedNum; i++)
        {
            characterPosition.Add(characterPosition[i - 1] + prefabSpacing);
        }

        // selectedIndex에 있는 값들을 selectedCharacter에 넣어주기 
        // 선택된 캐릭터들만 화면에 보이게 활성화
        for (int i = 0; i < selectedToggleIndexes.Count; i++)
        {
            GameObject cha = Instantiate(prefabs[selectedToggleIndexes[i]], new Vector3(characterPosition[i], 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);
        }
    }

    // 토글 선택 해제 시 프리팹 삭제 함수
    private void DestroyPrefabForToggle()
    {
        foreach (GameObject character in selectedCharacter)
        {
            Destroy(character);
        }

        // 선택된 캐릭터들만 다시 재생성
        GeneratePrefabForToggle();
    }

    public void ChangeGameScene()
    {
        if (selectedToggleIndexes.Count >= 2)
        {
            SoundMgr.Instance.ChangeBGMForScene();//사운드 변경
            SoundMgr.Instance.countDown();
            SceneManager.LoadScene("Game");
        }
        else
        {
            StartCoroutine(ShowPanelAndHideAfterDelay());
        }
    }

    private IEnumerator ShowPanelAndHideAfterDelay()
    {
        UIManager.Instance.ActivePnl(pnlSelected);

        // 2초 대기
        yield return new WaitForSeconds(2f);

        // 패널 다시 끄기
        UIManager.Instance.ActivePnl(pnlSelected); 
        
    }

    private void InitializeList()
    {
        selectedCharacter.Clear();
        characterPosition.Clear();
    }
}



