using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        if (DataManager.Instance != null)
        {
            nameInput.text = DataManager.Instance.playerName;
            if (DataManager.Instance.bestPlayerName != "")
            {
                highScoreText.text = "Best Score : " + DataManager.Instance.highScore + " by " + DataManager.Instance.bestPlayerName;
            }
            else
            {
                highScoreText.text = "No High Score Yet";
            }
        }
    }

    public void ButtonStart()
    {
        if (nameInput.text == "")
        {
            EventSystem.current.SetSelectedGameObject(nameInput.gameObject);
        }
        else
        {
            DataManager.Instance.playerName = nameInput.text;
            SceneManager.LoadScene(1);
        }
    }

    public void ButtonScores()
    {
        SceneManager.LoadScene(2);
    }

    public void ButtonExit()
    {
        DataManager.Instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
