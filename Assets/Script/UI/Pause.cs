using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject canvas_Pause;
    public GameObject pause_menu;
    public GameObject LevelUp_menu;
    Button button_ReStart;
    Button button_MainMenu;
    Button button_continue;
    Player player;

    public static bool isPause = false;

    private void Awake()
    {
        button_ReStart = GameObject.Find("ReStart Button").GetComponent<Button>();
        button_MainMenu = GameObject.Find("MainMenu Button").GetComponent<Button>();
        button_continue = GameObject.Find("Continue Button").GetComponent<Button>();

        button_ReStart.onClick.AddListener(OnReStart);
        button_MainMenu.onClick.AddListener(OnMainMenu);
        button_continue.onClick.AddListener(OnContinue);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        canvas_Pause.SetActive(false);
        pause_menu.SetActive(false);
        LevelUp_menu.SetActive(false);
    }

    public void OnPause()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            pause_menu.SetActive(false);            
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvas_Pause.SetActive(true);
            pause_menu.SetActive(true);            
        }
    }

    public void OnLeveUp()
    {        
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            LevelUp_menu.SetActive(false);            
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvas_Pause.SetActive(true);
            LevelUp_menu.SetActive(true);            
        }
    }

    private void OnReStart()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        canvas_Pause.SetActive(false);
        pause_menu.SetActive(false);        
    }

    private void OnMainMenu()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainLobby");
        canvas_Pause.SetActive(false);
        pause_menu.SetActive(false);        
    }

    private void OnContinue()
    {
        isPause = false;
        Time.timeScale = 1;
        canvas_Pause.SetActive(false);
        LevelUp_menu.SetActive(false);
    }
}
