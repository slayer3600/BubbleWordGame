using UnityEngine;
using Assets.Scripts;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMasterScript : MonoBehaviour {

    public static GameMasterScript masterScript;
    //public LevelItemList myLevelData;
    public AzureLevelItemList myAzureLevelData;
    public int currentLevel = 0;

	// Use this for initialization
	void Awake () {

        if (!masterScript)
        {
            masterScript = this;
            InitializeLevelData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    
	}

    public void SetLevel(int level)
    {
        currentLevel = level;
    }

    //public LevelItem GetCurrentLevelData()
    //{
    //    return myLevelData.level[currentLevel-1];
    //}

    public AzureLevelItem GetCurrentLevelData()
    {
        return myAzureLevelData[currentLevel - 1];
    }

    public void InitializeLevelData()
    {
        //WebAPI myWebAPI = new WebAPI();
        //string response = myWebAPI.Get("http://azurewebapihuntsman.azurewebsites.net/api/WordGameLevels");
        //myAzureLevelData = JsonConvert.DeserializeObject<AzureLevelItemList>(response);
        //Save();
        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/BubbleLevels.dat");

        bf.Serialize(file, myAzureLevelData);
        file.Close();

    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/BubbleLevels.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/BubbleLevels.dat", FileMode.Open);

            myAzureLevelData = (AzureLevelItemList)bf.Deserialize(file);
            file.Close();
        }
    }
	
}
