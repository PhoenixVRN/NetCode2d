using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class LobbyServer : MonoBehaviour
{
    public static LobbyServer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Настраиваем Unity Transport
        // var unityTransport = GetComponent<UnityTransport>();
        // unityTransport.SetConnectionData("127.0.0.1", 7777); // Указываем порт
        //
        // // Запускаем сервер
        // NetworkManager.Singleton.StartServer();
        // Debug.Log($"Start Server");
    }
    [ServerRpc(RequireOwnership = false)]
    public void JoinRoomServerRpc(int roomIndex)
    {
        Debug.Log($"JoinRoomServerRpc {roomIndex}");
        // if (roomIndex >= 0 && roomIndex < rooms.Count)
        // {
        //     rooms[roomIndex].JoinRoomServerRpc();
        // }
    }

    [ServerRpc(RequireOwnership = false)]
    public void LeaveRoomServerRpc(int roomIndex)
    {
        Debug.Log($"LeaveRoomServerRpc {roomIndex}");
        // if (roomIndex >= 0 && roomIndex < rooms.Count)
        // {
        //     rooms[roomIndex].LeaveRoomServerRpc();
        // }
    }
}