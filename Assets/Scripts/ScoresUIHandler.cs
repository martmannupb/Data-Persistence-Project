using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresUIHandler : MonoBehaviour
{
    public TextMeshProUGUI namesText;
    public TextMeshProUGUI scoresText;

    // Start is called before the first frame update
    void Start()
    {
        string names = "";
        string scores = "";
        for (int i = 0; i < DataManager.Instance.playerNames.Count; i++)
        {
            names += DataManager.Instance.playerNames[i] + "\n";
            scores += DataManager.Instance.playerScores[i] + "\n";
        }
        namesText.text = names;
        scoresText.text = scores;
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
