using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class BulletSpawn : NetworkBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда вылетает пуля
    public float bulletSpeed = 10f; // Скорость пули

    void Update()
    {
        if (IsOwner && Input.GetMouseButtonDown(0)) 
        {
            Debug.Log($"Fire");
            Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnBulletServerRpc(targetPosition);
        }
    }
    [ServerRpc]
    public void SpawnBulletServerRpc(Vector3 position)
    { 
    GameObject  bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    NetworkObject networkObject = bullet.GetComponent<NetworkObject>();
    networkObject.Spawn();
    Vector2 direction = ((Vector2)position - (Vector2)firePoint.position).normalized;
    
    BulletHandler bulletScript = bullet.GetComponent<BulletHandler>();
    bulletScript.direction = direction; // Направление пули
    bulletScript.speed = bulletSpeed; // Скорость пули
    }
}
