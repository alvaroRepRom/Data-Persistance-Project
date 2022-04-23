using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;
    public TextMeshProUGUI bestScore;
    public TMP_InputField enterName;
    private string name;
    
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
        Debug.Log(name);
    }

}
