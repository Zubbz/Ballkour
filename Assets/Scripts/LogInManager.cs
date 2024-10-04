using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogInManager : MonoBehaviour
{
    [Header("Data Source")]
    private PlayerCollection database = new PlayerCollection();
    public TextAsset JSONFile;

    [Header("Input Fields")]
    public TMP_InputField playerUsername;
    public TMP_InputField playerPassword;

    [Header("User Interface")]
    public GameObject currentPanel;
    public GameObject selectionPanel;
    public GameObject logInPanel;

    public void Awake()
    {
        database = JsonUtility.FromJson<PlayerCollection>(JSONFile.text);
        if(currentPanel == null)
        {
            currentPanel = logInPanel;
        }
    }

    public bool AuthenticateLogin(string username, string password)
    {
        for (int i = 0; i < database.players.Length; i++) 
        {
            if (database.players[i].playerUsername == username && database.players[i].playerPassword == password)
            {
                GameManager.instance.currentUser = database.players[i];
                Debug.Log("Current User: " + GameManager.instance.currentUser);
                return true;
            }
        }
        return false;
    }

    public void LogIn()
    {
        if (AuthenticateLogin(playerUsername.text, playerPassword.text))
        {
            SwitchPanel(selectionPanel);
        }
    }

    public void SwitchPanel(GameObject nextPanel)
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
        currentPanel = nextPanel;
    }
}
