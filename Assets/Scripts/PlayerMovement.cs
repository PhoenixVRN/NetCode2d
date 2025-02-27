using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Refs")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Rigidbody2D _rb;

    [Header("Settings")] 
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _weaponTurn;
    private float lastTime;
    private Vector2 randomPosition;

    private Vector2 _previousMovementInput;

    public override void OnNetworkSpawn()
    {
        // lastTime = Time.time + 3;
       // if (!IsOwner) return;
       // _inputReader.MoveEvent += HandleMove;
    }


    public override void OnNetworkDespawn()
    {
        // if (!IsOwner) return;
        // _inputReader.MoveEvent -= HandleMove;
    }

    private void HandleMove(Vector2 movementInput)
    {
        Debug.Log($"HandleMove {movementInput}");
        // _previousMovementInput = movementInput;
        Vector2 movement = new Vector2(movementInput.x, movementInput.y).normalized;
        transform.Translate(movement * _movementSpeed * Time.deltaTime);
        // _rb.linearVelocity = movementInput * _movementSpeed;
    }
    private void Update()
    {
        if (!IsOwner) return;
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");  
        
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        transform.Translate(movement * _movementSpeed * Time.deltaTime);
        
        // if (!IsOwnedByServer)
        // {
        //     if (lastTime < Time.time) // Пример: перемещение по нажатию клавиши
        //     {
        //         lastTime += 3;
        //         Vector2 randomDirection = Random.insideUnitCircle;
        //
        //         // Масштабируем точку на максимальную дистанцию
        //         randomPosition = Vector2.zero + randomDirection * 4;
        //     }
        //
        //     if (randomPosition != null)
        //     {
        //         transform.position = Vector2.MoveTowards(transform.position, randomPosition, 2 * Time.deltaTime);
        //     }
        // }
    }
}
