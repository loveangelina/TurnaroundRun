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

    public GameObject SelectFirstPlace()
    {
        float maxZ = 0;
        GameObject firstPlace = new GameObject();
        foreach(GameObject character in characters)
        {
            float zPosition = character.transform.position.z;
            if (maxZ < zPosition)
            {
                maxZ = zPosition;
                firstPlace = character;
            }
        }

        return firstPlace;
    }

    public GameObject GetBoostCharacter()
    {
        BoostManager boostManager = FindObjectOfType<BoostManager>();
        Debug.Log("Boost 캐릭터는 " + boostManager.currentBoostPlayerIndex);
        return characters[boostManager.currentBoostPlayerIndex];
    }
}
