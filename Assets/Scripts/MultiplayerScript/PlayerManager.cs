using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Com.Serzz0.Ballkour;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI nameDisplayer;
    public PhotonView playerPrefab;

    private void Awake()
    {
        PhotonNetwork.Instantiate(playerPrefab.name,Vector3.zero, Quaternion.identity);
        nameDisplayer.text = PhotonNetwork.NickName;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
