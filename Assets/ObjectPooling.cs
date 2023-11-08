using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{

    public GameObject[] platforms; // 플랫폼 프리팹 배열
    public int poolSize = 6; // 풀의 크기
    public float platformLength = 33f; // 플랫폼의 길이
    public Transform cameraTransform; // 카메라의 Transform

    private Queue<GameObject> platformPool; // 플랫폼 풀
    private Vector3 lastSpawnPoint; // 마지막으로 생성된 플랫폼의 위치

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        // 처음에 풀의 크기만큼 플랫폼을 생성합니다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platforms[Random.Range(0, platforms.Length)]);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        // 시작 지점 초기화
        lastSpawnPoint = transform.position;

        for (int i = 0; i < poolSize; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        // 카메라가 다가오면 새로운 플랫폼을 생성하고 오래된 플랫폼을 재사용합니다.
        if (cameraTransform.position.z > lastSpawnPoint.z - (poolSize * platformLength) + platformLength)
        {
            SpawnPlatform();
            RecyclePlatform();
        }
    }

    private void SpawnPlatform()
    {
        if (platformPool.Count == 0) return;

        GameObject platform = platformPool.Dequeue();
        platform.transform.position = lastSpawnPoint;
        platform.SetActive(true);

        lastSpawnPoint = new Vector3(lastSpawnPoint.x, lastSpawnPoint.y, lastSpawnPoint.z + platformLength);
    }

    private void RecyclePlatform()
    {
        // 풀에서 사용하지 않는 플랫폼을 찾아 재사용합니다.
        foreach (var platform in platformPool)
        {
            if (platform.activeSelf && (cameraTransform.position.z - platform.transform.position.z) > (poolSize * platformLength))
            {
                platform.SetActive(false);
                platformPool.Enqueue(platform);
                return;
            }
        }
    }
}
