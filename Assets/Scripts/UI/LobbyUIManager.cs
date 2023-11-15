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

    private Dictionary<int, GameObject> prefabInstances = new Dictionary<int, GameObject>();

    private List<int> selectedToggleIndexes = new List<int>();

    public GameObject pnlSelected;

    

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
    }



    public void SelectChar()
    {
        for (int i = 0; i < chartoggles.Length; i++)
        {
            if (chartoggles[i].isOn)
            {
                // 토글이 선택되면 해당 인덱스로 프리팹을 생성하는 함수 호출
                GeneratePrefabForToggle(i);
                // 선택된 토글 인덱스를 리스트에 추가
                if (!selectedToggleIndexes.Contains(i))
                {
                    selectedToggleIndexes.Add(i);
                }

            }
            else
            {
                // 토글이 선택되지 않으면 해당 인덱스로 프리팹을 삭제하는 함수 호출
                DestroyPrefabForToggle(i);

                // 선택 해제된 토글 인덱스를 리스트에서 제거
                selectedToggleIndexes.Remove(i);
             
            }

        }
        // 남은 프리팹의 위치 정렬
        RearrangePrefabPositions();
    }


    // 토글 선택 시 프리팹 생성 함수
    private void GeneratePrefabForToggle(int toggleIndex)
    {
        Debug.Log("선택된 토글 인덱스: " + toggleIndex);

        if (!prefabInstances.ContainsKey(toggleIndex))
        {   
            // 인덱스에 따라 다른 프리팹 생성 로직을 추가
            switch (toggleIndex)
            {
                case 0:
                    InstantiatePrefab("Prefab0",toggleIndex);
                    break;
                case 1:
                    InstantiatePrefab("Prefab1",toggleIndex);
                    break;
                case 2:
                    InstantiatePrefab("Prefab2",toggleIndex);
                    break;
                case 3:
                    InstantiatePrefab("Prefab3",toggleIndex);
                    break;
                case 4:
                    InstantiatePrefab("Prefab4",toggleIndex);
                    break;
                case 5:
                    InstantiatePrefab("Prefab5",toggleIndex);
                    break;
                case 6:
                    InstantiatePrefab("Prefab6",toggleIndex);
                    break;
                case 7:
                    InstantiatePrefab("Prefab7",toggleIndex);
                    break;
            }
        }
    }

    // 토글 선택 해제 시 프리팹 삭제 함수
    private void DestroyPrefabForToggle(int toggleIndex)
    {
        Debug.Log("선택이 해제된 토글 인덱스: " + toggleIndex);

        // 인덱스에 해당하는 프리팹 인스턴스가 있다면 삭제
        if (prefabInstances.ContainsKey(toggleIndex))
        {
            DestroyPrefab(toggleIndex);
        }
    }



    // 실제 프리팹 생성 함수 (프리팹 이름과 토글 인덱스를 받아 생성)
    private void InstantiatePrefab(string prefabName, int toggleIndex)
    {
        // Resources 폴더에 있는 프리팹 로드
        GameObject prefab = Resources.Load<GameObject>(prefabName);

        if (prefab != null)
        {
            // 프리팹을 인스턴스화하여 생성
            Vector3 spawnPosition = new Vector3(toggleIndex * prefabSpacing, 0f, 0f); // 프리팹 생성 위치 계산
            GameObject instance = Instantiate(prefab, spawnPosition, Quaternion.identity);

            // 생성된 프리팹 인스턴스를 딕셔너리에 추가
            prefabInstances.Add(toggleIndex, instance);

            Debug.Log("프리팹 생성: " + prefabName);
        }
        else
        {
            Debug.LogError("프리팹을 찾을 수 없습니다: " + prefabName);
        }
    }

    // 실제 프리팹 삭제 함수 (토글 인덱스를 받아 삭제)
    private void DestroyPrefab(int toggleIndex)
    {
        // 딕셔너리에서 해당 인덱스의 프리팹 인스턴스를 찾아 삭제
        GameObject prefabInstance;
        if (prefabInstances.TryGetValue(toggleIndex, out prefabInstance))
        {
            prefabInstances.Remove(toggleIndex);

            // 프리팹 인스턴스를 파괴하여 삭제
            Destroy(prefabInstance);
            Debug.Log("프리팹 삭제: " + toggleIndex);
        }
        else
        {
            Debug.LogError("삭제할 프리팹 인스턴스를 찾을 수 없습니다: " + toggleIndex);
        }
    }
    private void RearrangePrefabPositions()
    {
        float offset = prefabSpacing * 0.5f;

        var sortedPrefabInstances = prefabInstances.OrderBy(pair => pair.Key);

        int index = 0;
        foreach (var pair in sortedPrefabInstances)
        {
            Vector3 newPosition = new Vector3(index * prefabSpacing, 0f, 0f);

            // 중앙 정렬
            float totalWidth = (prefabInstances.Count - 1) * prefabSpacing;
            newPosition.x -= totalWidth / 2f;

            // 다른 프리팹들과의 충돌 체크 및 조정
            foreach (var otherPair in sortedPrefabInstances)
            {
                if (pair.Key != otherPair.Key)
                {
                    float distance = Mathf.Abs(newPosition.x - otherPair.Value.transform.position.x);
                    float minDistance = prefabSpacing;

                    if (distance < minDistance)
                    {
                        float overlap = minDistance - distance;
                        newPosition.x += overlap * Mathf.Sign(newPosition.x - otherPair.Value.transform.position.x);
                    }
                }
            }

            pair.Value.transform.position = newPosition;
            index++;
        }
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
}



