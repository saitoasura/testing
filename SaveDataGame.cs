using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataGame : MonoBehaviour
{
     //SAVE SYSTEM
    //SAVE GAME FITURE

    public Costumization player;

    public SaveSystemManager savemanager;


    private void FixedUpdate()
    {
        player = FindObjectOfType<Costumization>();
        savemanager = FindObjectOfType<SaveSystemManager>();
    }

    [Serializable]
    public class PlayerDataSerialization
    {
        public Costumization data = GameObject.FindObjectOfType<Costumization>();
        public bool Glasses, Boots;

        public Color glassesColor, bootsColor;


        public PlayerDataSerialization(bool Glasses, bool Boots, Color glassesColor, Color bootsColor)
        {
            this.Glasses = data.Glasses;
            this.Boots = data.Boots;
            this.glassesColor = data.galassesObject.material.color;
            this.bootsColor = data.bootsObject.material.color;
            
        }
    }

    [Serializable]
    public class SaveDataSerialization
    {
        public List<PlayerDataSerialization> playerData = new List<PlayerDataSerialization>();

        public void AddPlayerData(bool Glasses, bool Boots, Color glassesColor, Color bootsColor)
        {
            playerData.Add(new PlayerDataSerialization(Glasses, Boots, glassesColor, bootsColor));
        }

    }


    public void SaveGame()
    {

        SaveDataSerialization saveData = new SaveDataSerialization();

        saveData.AddPlayerData(player.Glasses, player.Boots, player.galassesObject.material.color, player.bootsObject.material.color);
        
        var jsonFormat = JsonUtility.ToJson(saveData, true);
        Debug.Log(jsonFormat);
        savemanager.SaveData(jsonFormat);


    }

    public void LoadGame()
    {
        var jsonFormatData = savemanager.LoadData();
        if (String.IsNullOrEmpty(jsonFormatData))
            return;
        SaveDataSerialization saveData = JsonUtility.FromJson<SaveDataSerialization>(jsonFormatData);

        foreach (var playerData in saveData.playerData)
        {
            player.Glasses = playerData.Glasses;
            player.Boots = playerData.Boots;
            player.galassesObject.material.color = playerData.glassesColor;
            player.bootsObject.material.color = playerData.bootsColor;

        }
        


    }
}
