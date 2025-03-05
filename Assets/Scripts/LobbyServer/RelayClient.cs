using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class RelayClient : MonoBehaviour
{
    private TcpClient tcpClient;
    private NetworkStream stream;
    private RelayClient client;
    private void Start()
    {
        client = new RelayClient();
        client.Connect("109.195.51.60", 7777); // Подключение к серверу через белый IP

        // while (true)
        // {
            // string message = Console.ReadLine();
            // string message = "Hello World! Hello RelayClient!";
            // client.Send(message);
        // }
    }

    public void SendMessage()
    {
        string message = "Hello World! Hello RelayClient 2!";
        client.Send(message);
    }
    public void Connect(string serverIP, int port)
    {
        tcpClient = new TcpClient(serverIP, port);
        stream = tcpClient.GetStream();
        Console.WriteLine("Подключено к серверу!");

        Thread receiveThread = new Thread(ReceiveData);
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                // Console.WriteLine($"Получено от сервера: {data}");
                Debug.Log($"Получено от сервера: {data}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка: {e.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

    public void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    public void Disconnect()
    {
        stream.Close();
        tcpClient.Close();
        Console.WriteLine("Отключено от сервера.");
    }
}
