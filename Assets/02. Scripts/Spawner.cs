using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("References")]
    public GameObject[] gameObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable() // 오브젝트가 활성화될때 실행됨
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    void OnDisable(){
        CancelInvoke();
    }
    void Spawn(){
            GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
            Instantiate(randomObject, transform.position, Quaternion.identity);
            Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
