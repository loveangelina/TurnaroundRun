using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterGroup;       // 캐릭터 프리팹들이 들어감
    [SerializeField] public List<GameObject> selectedCharacter;    // 선택된 캐릭터
    List<int> selectedIndex;

 
    void Awake()
    { 
        selectedIndex = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

        SetCharactersPosition();

        CharacterManager.Instance.characters = selectedCharacter;
    }


    public void SetCharactersPosition()
    {
        int selectedNum = selectedIndex.Count;
        float firstPosition = 0 - 1f * (selectedNum - 1);

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
