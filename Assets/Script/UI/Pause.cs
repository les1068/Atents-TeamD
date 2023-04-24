using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public static bool isPause = false;

    public GameObject canvas_Pause;
    Button button_ReStart;
    Button button_MainMenu;
    
    private void Awake()
    {
        button_ReStart = GameObject.Find("ReStart Button").GetComponent<Button>();
        button_MainMenu = GameObject.Find("MainMenu Button").GetComponent<Button>();        
        
        button_ReStart.onClick.AddListener(OnReStart);
        button_MainMenu.onClick.AddListener(OnMainMenu);        
    }

    private void Start()
    {
        canvas_Pause.SetActive(false);
    }

    public void OnPause()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            //Debug.Log($"{isPause}");
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvas_Pause.SetActive(true);
            //Debug.Log($"{isPause}");
        }
    }

    private void OnReStart()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("OnGameStart()");
    }

    private void OnMainMenu()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainLobby");
        //Debug.Log("OnCredits()");
    }
}
