using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour
{
    [SerializeField]
    private string saveFile;
    [SerializeField]
    private GameInfo gameInfo = new GameInfo();
    [SerializeField]
    private LevelManager levelManager;

    private bool saveLoaded = false;

    private void Awake()
    {
        saveFile = Application.dataPath + "/gameInfo.json";

        if (!saveLoaded)
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        if (File.Exists(saveFile))
        {
            string content = File.ReadAllText(saveFile);
            gameInfo = JsonUtility.FromJson<GameInfo>(content);

            levelManager.pumpkins = gameInfo.TotalPumpkins;
        }
        else
        {
            Debug.Log("El Archivo No existe");
        }
        saveLoaded = true;
    }

    public void SaveData()
    {
        if (File.Exists(saveFile))
        {
            string jsonActual = File.ReadAllText(saveFile);
            gameInfo = JsonUtility.FromJson<GameInfo>(jsonActual);
        }
        else
        {
            gameInfo = new GameInfo();
        }

        gameInfo.TotalPumpkins = levelManager.pumpkins;

        string cadenaJSON = JsonUtility.ToJson(gameInfo);
        File.WriteAllText(saveFile, cadenaJSON);

        Debug.Log("Saved");
    }
}
