using UnityEngine;
using Unity.Netcode;
using UnityEngine.PlayerLoop;

public class MovementBot : NetworkBehaviour
{
    public float maxHitpoints;
    public HealthBar healthBar;
    public NetworkVariable<float> health = new NetworkVariable<float>(20, NetworkVariableReadPermission.Everyone, 
        NetworkVariableWritePermission.Server);
    [HideInInspector] public float hitPoints;
    public float timeToMove;
    public float speedBot;
    public Rigidbody2D rb;
    public float maxDistance = 5f; // Максимальная дистанция от центра
    private Vector2 centerPosition; // Центр (например, начальная позиция объекта)
    private float lastTime;
    private Vector2 randomPosition;
    void Start()
    {
        // health = new NetworkVariable<float>(maxHitpoints, NetworkVariableReadPermission.Everyone);
        health.OnValueChanged += OnHealthChanged;
        // health.Value = maxHitpoints;
        hitPoints = maxHitpoints;
        healthBar.SetHealth(hitPoints, maxHitpoints);
        lastTime = Time.time + timeToMove;
        // Запоминаем начальную позицию как центр
        centerPosition = Vector2.zero;
    }

    private void OnHealthChanged(float previousvalue, float newvalue)
    {
        // Debug.Log($"OnHealthChanged {previousvalue}/{newvalue}");
        healthBar.SetHealth(newvalue,maxHitpoints);
    }

    void Update()
    {
        if (lastTime < Time.time) // Пример: перемещение по нажатию клавиши
        {
            lastTime += timeToMove;
            Vector2 randomDirection = Random.insideUnitCircle;

            // Масштабируем точку на максимальную дистанцию
            randomPosition = centerPosition + randomDirection * maxDistance;
        }

        if (randomPosition != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomPosition, speedBot * Time.deltaTime);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void TakeHitServerRpc(float damage)
    {
        hitPoints -= damage;
        health.Value = hitPoints;
    }

    public override void OnNetworkDespawn()
    {
        health.OnValueChanged -= OnHealthChanged;
    }
}
