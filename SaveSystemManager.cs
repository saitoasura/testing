using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveSystemManager : MonoBehaviour
{
  //////////////////JSON SAVE///////////////////////
    public string saveName = "SaveData";

    public void SaveData(string dataToSave)
    {
        if (WriteToFile(saveName, dataToSave))
        {
            Debug.Log("Successfully saved data");
        }
    }

    public string LoadData()
    {
        string data = "";
        if (ReadFromFile(saveName, out data))
        {
            Debug.Log("Successfully loaded data");
        }
        return data;
    }

    private bool WriteToFile(string name, string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath + Path.DirectorySeparatorChar + name+".json");

        try
        {
            File.WriteAllText(fullPath, content);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving to a file " + e.Message);
        }
        return false;
    }

    private bool ReadFromFile(string name, out string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath + Path.DirectorySeparatorChar + name+".json");
        try
        {
            content = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when loading the file " + e.Message);
            content = "";
        }
        return false;
    }
        
    
}
