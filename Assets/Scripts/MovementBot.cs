using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementBot : MonoBehaviour
{
    public float timeToMove;
    public float speedBot;
    public Rigidbody2D rb;
    public float maxDistance = 5f; // Максимальная дистанция от центра
    private Vector2 centerPosition; // Центр (например, начальная позиция объекта)
    private float lastTime;
    private Vector2 randomPosition;
    void Start()
    {
        lastTime = Time.time + timeToMove;
        // Запоминаем начальную позицию как центр
        centerPosition = Vector2.zero;
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
    
}
