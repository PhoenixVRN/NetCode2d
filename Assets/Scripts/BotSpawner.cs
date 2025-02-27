using UnityEngine;
using Unity.Netcode;

public class BotSpawner : NetworkBehaviour
{
    public GameObject botPrefab;
    
    [ServerRpc]
    public void SpawnBotServerRpc(Vector3 position, Quaternion rotation)
    {
        // Спавним бота на сервере
        GameObject botInstance = Instantiate(botPrefab, position, rotation);
        NetworkObject networkObject = botInstance.GetComponent<NetworkObject>();
        networkObject.Spawn(); // Спавним объект в сети
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log($"Spawn Bot");
            // Пример: спавним бота по нажатию клавиши
            Vector2 pos = new Vector2(Random.Range(0, 3), Random.Range(0, 3));
            SpawnBotServerRpc(pos, Quaternion.identity);
        }
    }
}
