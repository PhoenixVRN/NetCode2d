using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Start()
    {
        _inputReader.MoveEvent += HandleMove;
    }

    private void HandleMove(Vector2 move)
    {
        Debug.Log($"move {move}");
    }

    private void OnDestroy()
    {
        _inputReader.MoveEvent -= HandleMove;
    }
}