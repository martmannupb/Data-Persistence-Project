using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public string bestPlayerName;
    public int highScore;

    public List<string> playerNames;
    public List<int> playerScores;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerNames = new List<string>();
        playerScores = new List<int>();
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveData
    {
        public List<string> playerNames;
        public List<int> scores;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.playerNames = playerNames;
        data.scores = playerScores;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void Load()
    {
        playerNames.Clear();
        playerScores.Clear();
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Turn arrays into dictionary
            for (int i = 0; i < data.playerNames.Count; i++)
            {
                UpdatePlayerScore(data.playerNames[i], data.scores[i]);
            }
        }
        else
        {
            bestPlayerName = "";
            highScore = 0;
        }
    }

    public void UpdatePlayerScore(string playerName, int score)
    {
        // Remove from list if necessary or abort if score is not higher
        for (int i = 0; i < playerNames.Count; i++)
        {
            if (playerNames[i] == playerName)
            {
                if (playerScores[i] >= score)
                {
                    return;
                }
                playerNames.RemoveAt(i);
                playerScores.RemoveAt(i);
                break;
            }
        }
        // Insert new entry
        int insertAt = 0;
        for (int i = 0; i < playerNames.Count; i++)
        {
            if (score > playerScores[i])
            {
                break;
            }
            insertAt++;
        }
        playerNames.Insert(insertAt, playerName);
        playerScores.Insert(insertAt, score);
        if (insertAt == 0)
        {
            bestPlayerName = playerName;
            highScore = score;
        }
    }
}
