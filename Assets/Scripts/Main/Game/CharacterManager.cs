using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public List<GameObject> characters;       // 달리고 있는 캐릭터들

    public static CharacterManager Instance
    {
        get 
        { 
            if(!instance)
            {
                instance = FindObjectOfType<CharacterManager>();

                // 씬에 CharacterManager가 없는 경우 새로 생성
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("CharacterManager");
                    instance = singletonObject.AddComponent<CharacterManager>();
                }
            }

            return instance; 
        }
    }


    void Start()
    {
        SelectFirstPlace();
    }

    void Update()
    {
        
    }

    private void SelectFirstPlace()
    {

    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
    }
}
