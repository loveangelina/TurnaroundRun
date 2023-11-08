using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameractrl : MonoBehaviour
{
    public float cameraSpeed = 10.0f; // 카메라 이동 속도
    public float startPosition = -210f; // 시작 위치
    public float endPosition = 510f; // 끝 위치
    private bool movingForward = true; // 이동 방향

    void Update()
    {
        // 카메라의 현재 위치
        Vector3 currentPosition = transform.position;

        // 카메라를 Z 방향으로 이동
        if (movingForward)
        {
            currentPosition.z += cameraSpeed * Time.deltaTime;
            if (currentPosition.z >= endPosition)
            {
                currentPosition.z = endPosition;
                movingForward = false;
            }
        }
        else
        {
            currentPosition.z -= cameraSpeed * Time.deltaTime;
            if (currentPosition.z <= startPosition)
            {
                currentPosition.z = startPosition;
                movingForward = true;
            }
        }

        transform.position = currentPosition;
    }

}
