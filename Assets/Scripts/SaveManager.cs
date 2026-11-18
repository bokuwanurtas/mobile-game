using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Сам создаётся при первом обращении, класть в сцену не нужно.
public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;

    public static SaveManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SaveManager");
                _instance = go.AddComponent<SaveManager>();
            }
            return _instance;
        }
    }

    public SaveData data;
    public int runScore = 0;   // очки текущего прохождения (в файл не пишутся)

    private string filePath;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(Application.persistentDataPath, "save.json");
        Load();
        AudioListener.volume = data.musicVol;
    }

    // ---------- Загрузка и сохранение ----------

    public void Load()
    {
        // Файла нет - значения по умолчанию
        if (File.Exists(filePath) == false)
        {
            data = CreateDefaultData();
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(json);

            // Файл пустой или странный
            if (data == null)
            {
                data = CreateDefaultData();
            }

            if (data.unlocked == null)
            {
                data.unlocked = new List<int>();
            }

            // Первый уровень всегда открыт
            if (data.unlocked.Contains(1) == false)
            {
                data.unlocked.Add(1);
            }

            // Громкость должна быть от 0 до 1
            if (data.musicVol < 0f || data.musicVol > 1f)
            {
                data.musicVol = 0.8f;
            }
        }
        catch (Exception e)
        {
            // Файл сломан - игра НЕ падает, берём значения по умолчанию
            Debug.LogWarning("Save file is broken, using defaults: " + e.Message);
            data = CreateDefaultData();
        }
    }

    public void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not save: " + e.Message);
        }
    }

    private SaveData CreateDefaultData()
    {
        SaveData newData = new SaveData();
        newData.version = 1;
        newData.bestScore = 0;
        newData.musicVol = 0.8f;
        newData.unlocked.Add(1);
        return newData;
    }

    // ---------- Удобные методы ----------

    public bool IsUnlocked(int levelNumber)
    {
        return data.unlocked.Contains(levelNumber);
    }

    public void UnlockLevel(int levelNumber)
    {
        if (data.unlocked.Contains(levelNumber) == false)
        {
            data.unlocked.Add(levelNumber);
        }
    }

    public void SubmitScore(int score)
    {
        if (score > data.bestScore)
        {
            data.bestScore = score;
        }
    }

    public void SetMusicVolume(float value)
    {
        data.musicVol = value;
        AudioListener.volume = value;
        Save();   // настройки сохраняем сразу
    }

    public void ResetProgress()
    {
        float oldVolume = data.musicVol;   // громкость оставляем
        data = CreateDefaultData();
        data.musicVol = oldVolume;
        runScore = 0;
        Save();
    }

    // ---------- Сохранение на телефоне ----------

    // Приложение свернули
    void OnApplicationPause(bool pause)
    {
        if (pause == true)
        {
            Save();
        }
    }

    // Приложение закрыли
    void OnApplicationQuit()
    {
        Save();
    }
}
