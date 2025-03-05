using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport;
using UnityEngine;

public class ServerListener : MonoBehaviour
{
    public string bindIP = "192.168.0.107"; // Конкретный IP-адрес для привязки
    public ushort port = 7777; // Порт для прослушивания

    void Start()
    {
        var networkManager = GetComponent<NetworkManager>();

        // Убедимся, что используется Unity Transport
        if (networkManager.NetworkConfig.NetworkTransport == null)
        {
            networkManager.NetworkConfig.NetworkTransport = gameObject.AddComponent<UnityTransport>();
        }

        // Настраиваем Unity Transport
        var unityTransport = networkManager.GetComponent<UnityTransport>();
        unityTransport.SetConnectionData(bindIP, port); // Принимаем подключения только на указанном IP

        // Запускаем сервер
        networkManager.StartServer();

        Debug.Log($"Сервер запущен и слушает порт {port} на IP {bindIP}");
    }
}
