using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ServerManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {

        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Successfully");
        PhotonNetwork.LoadLevel("SampleGameScene");
        base.OnJoinedRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room has been created");
        base.OnCreatedRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room, Room cannot be found");
        base.OnJoinRoomFailed(returnCode, message);
    }
}
