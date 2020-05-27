using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private GameData gameData = null;
    public static GameData GameData
    {
        get
        {
            if(DataManager.Instance == null) return null;
            if(DataManager.Instance.gameData == null)
            {
                DataManager.Instance.gameData = new GameData();
            }
            return DataManager.Instance.gameData;
        }

    }
    /*public GameData GetData()
    {
        return null;
    }

    private static DataManager instance;
    private static object mutex = new object();
    private DataManager() { 
        //Contruct
    }

    public static DataManager Instance
    {
        get
        {
            lock (DataManager.mutex)
            {
                if (DataManager.instance == null)
                {
                    DataManager.instance = new DataManager();
                }
            }
            return DataManager.instance;
        }
    }
    */
}
