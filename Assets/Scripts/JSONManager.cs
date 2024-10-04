using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager : MonoBehaviour
{
    public TextAsset JSONFile;
    public PlayerCollection playerCollection = new PlayerCollection();

    public void CreateJSONFile()
    {
        string data = JsonUtility.ToJson(playerCollection);
        File.WriteAllText(Application.dataPath + "/text.txt", data);
    }

    public void LoadJSONFile()
    {
        playerCollection = JsonUtility.FromJson<PlayerCollection>(JSONFile.text);
    }

}
