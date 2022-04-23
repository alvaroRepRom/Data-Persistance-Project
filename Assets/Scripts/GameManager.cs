using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI bestScoreText;
    public TMP_InputField enterName;
    public Button startButton;
    public Button quitButton;

    public static string name;

    public static string bestName;
    public static int bestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScore();
        ChangeBestScoreText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void EnterName()
    {
        name = enterName.text;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.score = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestName = data.name;
            bestScore = data.score;
        }
    }

    public string GetBestName()
    {
        return bestName;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int points)
    {
        if (bestScore > points) return;
        bestScore = points;
        SaveScore();
    }

    public void ChangeBestScoreText()
    {
        if (bestScore > 0)
        {
            bestScoreText.text = "Best Score: " + bestName + ": " + bestScore;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string name;
    public int score;
}
