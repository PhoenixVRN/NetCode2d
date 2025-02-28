using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HostIPDisplay : MonoBehaviour
{
    public TextMeshProUGUI ipText;

    void Start()
    {
        // Получаем локальный IP-адрес
        string localIP = GetLocalIPAddress();
        ipText.text = "Host IP: " + localIP;
    }

    string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "No network adapters with an IPv4 address found!";
    }
}
