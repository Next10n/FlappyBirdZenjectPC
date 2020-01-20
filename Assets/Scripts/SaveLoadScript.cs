using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Zenject;

public class SaveLoadScript
{
    [Inject]
    private ScoreController _scoreController;

    [Inject]
    private SoundController _soundController;

    string _filePath;



    public void SaveGame()
    {
        _filePath = GetPath();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate);
        Save save = new Save(_scoreController.GetMaxScore(), _soundController.SoundCheck());

        bf.Serialize(fs, save);

        fs.Close();
    }

    public void LoadGame()
    {
        _filePath = Application.persistentDataPath + "/save.gamesave";
        if (!File.Exists(_filePath))
        {
            Debug.Log("Файл с сохранением не найден");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        LoadData(save);

        fs.Close();
    }

    private void LoadData(Save save)
    {
        _scoreController.SetMaxScore(save.MaxScore);
        _soundController.SoundSetActive(save.SoundCheck);
    }

    public string GetPath()
    {
        return Application.persistentDataPath + "/save.gamesave";
    }

}



[System.Serializable]
public class Save
{

    public int MaxScore;
    public bool SoundCheck;

    public Save(int max, bool sound)
    {
        MaxScore = max;
        SoundCheck = sound;
    }


}

