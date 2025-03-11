using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    [SerializeField] private string _url = "http://109.195.51.60/";

    public void Create() => StartCoroutine(Create_Coroutine("TEST")); //TODO
    public void Join() => StartCoroutine(Join_Coroutine("TEST"));

    IEnumerator Create_Coroutine(string dataToSend)
    {
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
        }
        else
        {
            Debug.LogError("Ошибка: " + request.error);
        }
    }


    IEnumerator Join_Coroutine(string dataToSend)
    {
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