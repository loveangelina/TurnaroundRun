using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterGroup;       // 캐릭터 프리팹들이 들어감
    [SerializeField] List<GameObject> selectedCharacter;    // 선택된 캐릭터
    List<int> selectedIndex;

    void Start()
    {
        // TODO : 게임시작 연결 시 바꾸기 selectedIndexe
        selectedIndex = new List<int>();
        selectedIndex.Add(0);
        selectedIndex.Add(1);
        //selectedIndexe = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

        SetCharactersPosition();
    }

    void Update()
    {
        
    }

    private void SetCharactersPosition()
    {
        int selectedNum = selectedIndex.Count;
        float firstPosition = 0 - 0.5f * (selectedNum - 1);

        // selectedIndex에 있는 값들을 selectedCharacter에 넣어주기 
        // 선택된 캐릭터들만 화면에 보이게 활성화
        for (int i = 0; i < selectedIndex.Count; i++)
        {
            GameObject cha = Instantiate(characterGroup[selectedIndex[i]], new Vector3(firstPosition, 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);

            firstPosition += 2;
        }
    }
}
