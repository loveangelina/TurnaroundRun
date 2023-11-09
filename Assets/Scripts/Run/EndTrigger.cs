using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// 플레이어 태그를 가지고 있으면
        {
            other.gameObject.SetActive(false);
        }

    }
    

}
