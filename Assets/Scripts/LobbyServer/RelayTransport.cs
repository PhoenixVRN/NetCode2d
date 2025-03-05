using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class RelayTransport : MonoBehaviour
{
    public string serverIP = "109.195.51.60"; // Белый IP-адрес Relay Server
    public ushort serverPort = 7777; // Порт Relay Server

    void Start()
    {
        var networkManager = GetComponent<NetworkManager>();

        // Убедимся, что используется Unity Transport
        if (networkManager.NetworkConfig.NetworkTransport == null)
        {
            networkManager.NetworkConfig.NetworkTransport = gameObject.AddComponent<UnityTransport>();
        }

        // Настраиваем Unity Transport для работы с Relay Server
        var unityTransport = networkManager.GetComponent<UnityTransport>();
        unityTransport.SetConnectionData(serverIP, serverPort);
    }
}