using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
public class CustomUnityTransport : MonoBehaviour
{
    void Start()
    {
        // Получаем компонент NetworkManager
        var networkManager = GetComponent<NetworkManager>();

        // Убедимся, что используется Unity Transport
        if (networkManager.NetworkConfig.NetworkTransport == null)
        {
            networkManager.NetworkConfig.NetworkTransport = gameObject.AddComponent<UnityTransport>();
        }
    }
}
