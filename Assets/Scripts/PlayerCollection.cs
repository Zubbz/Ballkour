using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCollection
{
    public Player[] players;
}

[System.Serializable]
public class Player
{
    [Header("Login Credentials")]
    public string playerUsername;
    public string playerPassword;

    [Header("INFORMATION")]
    public int playerId;
    public string playerEmail;
    public int level;
}
