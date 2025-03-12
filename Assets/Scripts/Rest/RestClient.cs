using System.Collections;
using Newtonsoft.Json;
using Promul.Runtime.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    [SerializeField] private string _url = "http://109.195.51.60/";
    [SerializeField] private CustomPromulTransport _promulTransport;
    public TMP_InputField inputField;
    public TextMeshProUGUI joinCode;
    public TextMeshProUGUI relayAddres;
    public TextMeshProUGUI relayPort;
    
    public void Create() => StartCoroutine(Create_Coroutine()); //TODO
    public void Join() => StartCoroutine(Join_Coroutine());

    IEnumerator Create_Coroutine()
    {
        var dataToSend = inputField.text != "" ? inputField.text : "TEST";
        JoinCodeData data = new JoinCodeData {joinCode = dataToSend};
        string json = JsonUtility.ToJson(data);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest request = UnityWebRequest.Put("http://109.195.51.60:3000/session/create", postData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Успех: " + request.downloadHandler.text);
            MyResponseObject responseObject =
                JsonConvert.DeserializeObject<MyResponseObject>(request.downloadHandler.text);
            _promulTransport.NameRoom = responseObject.joinCode;
            joinCode.text = responseObject.joinCode;
            relayAddres.text = responseObject.relayAddress;
            relayPort.text = responseObject.relayPort.ToString();
        }
        else
        {
            Debug.LogError("Ошибка: " + request.error);
        }
    }


    IEnumerator Join_Coroutine()
    {
        var dataToSend = inputField.text != "" ? inputField.text : "TEST";
        Debug.Log($"Join_Coroutine {dataToSend}");
        JoinCodeData data = new JoinCodeData {joinCode = dataToSend};
        string json = JsonUtility.ToJson(data);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest request = UnityWebRequest.Put("http://109.195.51.60:3000/session/join", postData);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Успех: " + request.downloadHandler.text);
            MyResponseObject responseObject =
                JsonConvert.DeserializeObject<MyResponseObject>(request.downloadHandler.text);
            _promulTransport.NameRoom = responseObject.joinCode;
            joinCode.text = responseObject.joinCode;
            relayAddres.text = responseObject.relayAddress;
            relayPort.text = responseObject.relayPort.ToString();
        }
        else
        {
            Debug.LogError("Ошибка: " + request.error);
        }
    }
}

[System.Serializable]
public class JoinCodeData
{
    public string joinCode;
}

[System.Serializable]
public class MyResponseObject
{
    public string joinCode;
    public string relayAddress;
    public int relayPort;
}