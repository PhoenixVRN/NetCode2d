using Unity.Netcode;
using UnityEngine;

public class Room : NetworkBehaviour
{
    public NetworkVariable<int> PlayerCount = new NetworkVariable<int>(0);

    [ServerRpc]
    public void JoinRoomServerRpc()
    {
        PlayerCount.Value++;
        Debug.Log($"Player joined room. Current players: {PlayerCount.Value}");
    }

    [ServerRpc]
    public void LeaveRoomServerRpc()
    {
        PlayerCount.Value--;
        Debug.Log($"Player left room. Current players: {PlayerCount.Value}");
    }
}