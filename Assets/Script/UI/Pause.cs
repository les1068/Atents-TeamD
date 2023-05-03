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
    public GameObject Stage1End_menu;
    public GameObject Stage2End_menu;
    public GameObject Stage3End_menu;
    Button button_ReStart;
    Button button_MainMenu;
    Button button_continue;
    Button button_NextStage1;
    Button button_NextStage2;
    Button button_NextStage3;
    Player player;
        
    public static bool isPause = false;

    private void Awake()
    {
        button_ReStart = GameObject.Find("ReStart Button").GetComponent<Button>();
        button_MainMenu = GameObject.Find("MainMenu Button").GetComponent<Button>();
        button_continue = GameObject.Find("Continue Button").GetComponent<Button>();
        button_NextStage1 = GameObject.Find("Next Stage1 Button").GetComponent<Button>();
        button_NextStage2 = GameObject.Find("Next Stage2 Button").GetComponent<Button>();
        button_NextStage3 = GameObject.Find("Next Stage3 Button").GetComponent<Button>();

        button_ReStart.onClick.AddListener(OnReStart);
        button_MainMenu.onClick.AddListener(OnMainMenu);
        button_continue.onClick.AddListener(OnContinue);
        button_NextStage1.onClick.AddListener(OnNextStage1);
        button_NextStage2.onClick.AddListener(OnNextStage2);
        button_NextStage3.onClick.AddListener(OnNextStage3);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        canvas_Pause.SetActive(false);
        pause_menu.SetActive(false);
        LevelUp_menu.SetActive(false);
        Stage1End_menu.SetActive(false);
        Stage2End_menu.SetActive(false);
        Stage3End_menu.SetActive(false);
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

    public void Stage1End()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            Stage1End_menu.SetActive(false);
        }
        else
        {
            isPause = true;
            canvas_Pause.SetActive(true);
            Stage1End_menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Stage2End()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            Stage2End_menu.SetActive(false);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvas_Pause.SetActive(true);
            Stage2End_menu.SetActive(true);
        }
    }

    public void Stage3End()
    {
        if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvas_Pause.SetActive(false);
            Stage3End_menu.SetActive(false);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvas_Pause.SetActive(true);
            Stage3End_menu.SetActive(true);
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
        SceneManager.LoadScene(0);
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

    private void OnNextStage1()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
        canvas_Pause.SetActive(false);
        Stage1End_menu.SetActive(false);        
    }

    private void OnNextStage2()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
        canvas_Pause.SetActive(false);
        Stage2End_menu.SetActive(false);
    }

    private void OnNextStage3()
    {
        isPause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(7);
        canvas_Pause.SetActive(false);
        Stage3End_menu.SetActive(false);
    }
}
