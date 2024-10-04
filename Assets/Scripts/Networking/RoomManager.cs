using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public TMP_InputField roomNumberInput;


    #region buttonOnClick
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomNumberInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomNumberInput.text);
    }

    #endregion
}
