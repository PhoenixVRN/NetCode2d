using TMPro;
using UnityEngine;
using Unity.Netcode;

public class Test : NetworkBehaviour
{
    public TextMeshProUGUI text;
    public NetworkVariable<int> health = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone, 
        NetworkVariableWritePermission.Server);
    void Start()
    {
        // Подписываемся на событие изменения значения
        health.OnValueChanged += OnHealthChanged;
    }

    void OnHealthChanged(int oldValue, int newValue)
    {
        Debug.Log($"Health changed from {oldValue} to {newValue}");
        text.text = newValue.ToString();
    }

    public void Set1()
    {
        SetValueServerRpc();
    }
    [ServerRpc(RequireOwnership = false)]
    private void SetValueServerRpc()
    {
        health.Value -= 10;
    }
    // void Update()
    // {
    //     if (IsServer) // Только сервер может изменять значение
    //     {
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             health.Value -= 10; // Уменьшаем здоровье на 10
    //         }
    //     }
    // }
}