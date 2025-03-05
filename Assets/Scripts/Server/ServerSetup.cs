using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class ServerSetup : MonoBehaviour
{
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
        unityTransport.SetConnectionData("0.0.0.0", 7777); // Принимаем подключения на всех интерфейсах
        // unityTransport.SetServerBindAddress(NetworkEndpoint.AnyIpv4); // Привязка ко всем интерфейсам

        // Запускаем сервер
        networkManager.StartServer();
    }
}