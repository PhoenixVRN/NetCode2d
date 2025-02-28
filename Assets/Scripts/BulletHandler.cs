using System;
using Unity.Netcode;
using UnityEngine;

public class BulletHandler : NetworkBehaviour
{
    public float damage;
    public float speed = 10f; // Скорость пули
    public Vector2 direction = Vector2.right; // Направление движения пули
    public float lifetime = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (IsServer)
        {
            Destroy(gameObject, lifetime);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log($"OnTriggerEnter2D");
            if (collision.gameObject.TryGetComponent(out MovementBot handlerBullet))
            {
                handlerBullet.TakeHitServerRpc(damage);
                if (handlerBullet.hitPoints < 1)
                {
                    DestroyEnemyServerRpc(collision.gameObject);
                }
            }
            DestroyBulletServerRpc();
        }
    }
     [ServerRpc(RequireOwnership = false)]
    void DestroyEnemyServerRpc(NetworkObjectReference enemyRef)
    {
        if (enemyRef.TryGet(out NetworkObject enemy))
        {
            // Debug.Log($"Деспавним врага");
            enemy.Despawn(); // Деспавним врага
        }
    }
    [ServerRpc(RequireOwnership = false)]
    void DestroyBulletServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(); // Деспавним пулю
    }
}