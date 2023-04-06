using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLobby : MonoBehaviour
{    
    Button button_GameStart;
    Button button_Credits;
    Button button_Exit;

    private void Awake()
    {
        button_GameStart = GameObject.Find("GameStart Button").GetComponent<Button>();
        button_Credits = GameObject.Find("Credits Button").GetComponent<Button>();
        button_Exit = GameObject.Find("Exit Button").GetComponent<Button>();
        button_GameStart.onClick.AddListener(OnGameStart);
        button_Credits.onClick.AddListener(OnCredits);
        button_Exit.onClick.AddListener(OnExit);
    }

    private void OnGameStart()
    {
        SceneManager.LoadScene("TEST_ALL(Battle)");
        //Debug.Log("OnGameStart()");
    }

    private void OnCredits()
    {
        SceneManager.LoadScene("Credits");
        //Debug.Log("OnCredits()");
    }

    private void OnExit()
    {
        Application.Quit();
        //Debug.Log("OnExit()");
    }
}
