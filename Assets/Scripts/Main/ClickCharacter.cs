using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCharacter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<GameObject> characterGroup;       // 캐릭터 프리팹들이 들어감
    [SerializeField] List<GameObject> selectedCharacter;    // 선택된 캐릭터
    [SerializeField] List<int> selectedIndex;               // 선택된 UI 이미지 인덱스
    [SerializeField] List<float> characterPosition;         // 선택된 캐릭터들의 생성 위치

    void Start()
    {
        selectedCharacter = new List<GameObject>();
        characterPosition = new List<float>();
    }

    void Update()
    {
        // TODO : 
        // UI 이미지에서 이미 선택되어 있으면 선택할 수 없게 하는 게 필요
        // 지금은 list안에 이미 해당 게임오브젝트가 있는지 체크안하고 넣다보니
        // 계속 생성되고 있음 

        if(Input.GetKeyDown(KeyCode.Space))
        {
            selectedIndex.Clear();
            selectedIndex.Add(1);
            selectedIndex.Add(2);
            OnClickCharacter();
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            selectedIndex.Clear();
            selectedIndex.Add(1);
            OnDeselectCharacter();
        }
    }

    public void OnClickCharacter()
    {
        InitializeList();

        int selectedNum = selectedIndex.Count;
        int firstPosition = 0 - 1 * (selectedNum - 1);
        characterPosition.Add(firstPosition);
        // selectedIndex 값에 따라서 위치 정해진 list 만들기 
        for (int i = 1; i < selectedNum; i++)
        {
            characterPosition.Add(characterPosition[i - 1] + 2);
        }

        // selectedIndex에 있는 값들을 selectedCharacter에 넣어주기 
        // 선택된 캐릭터들만 화면에 보이게 활성화
        for (int i = 0; i < selectedIndex.Count; i++)
        {
            GameObject cha = Instantiate(characterGroup[selectedIndex[i]], new Vector3(characterPosition[i], 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);
        }
    }

    public void OnDeselectCharacter()
    {
        foreach(GameObject character in selectedCharacter)
        {
            Destroy(character);
        }

        // 선택된 캐릭터들만 다시 재생성
        OnClickCharacter();
    }

    private void InitializeList()
    {
        selectedCharacter.Clear();
        characterPosition.Clear();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 사용자가 클릭한 ui image에 해당하는 캐릭터 id를 가져와서
        // 그 아이디를 selectedIndex에 넣기 
        // 이미 ui image가 선택되어있는지 bool 변수줘서
        // 이미 ui image 선택되어있으면 index에서 빼기 
        
        // 이렇게하면 선택한게 게임오브젝트가 아니라 ui image라서 널참조오류 뜸 
        Debug.Log(eventData.selectedObject.name);
    }
}
