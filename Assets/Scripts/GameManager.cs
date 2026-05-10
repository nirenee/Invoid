using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gemscount = 0;
    public static string sceneToLoad;
    public int gemsRequired;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayerPrefs.DeleteAll();
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        SetGemsRequired();
    }

    public void AddGems(int amount)
    {
        gemscount += amount;
        SaveData();
    }
    public void ResetGems()
    {
        gemscount = 0;
        SaveData();
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("gems",gemscount);
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        gemscount = PlayerPrefs.GetInt("gems", 0);
    }
    private void SetGemsRequired()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "World1")
            gemsRequired = 100;
        else if (scene == "World2")
            gemsRequired = 200;
    }
}
