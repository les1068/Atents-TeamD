using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Singleton<T> : MonoBehaviour where T : Component
{
    private bool initialized = false;
    private const int NOT_SET = -1;
    private int mainSceneIndex = NOT_SET;
    private static bool isShutDown = false;
    private static T instance;
    public static T Inst
    {
        get
        {
            if (isShutDown)
            {
                Debug.LogWarning($"{typeof(T)} 싱글톤은 이미 삭제되었다.");
                return null;
            }
            if (instance == null)
            {
                T obj = FindObjectOfType<T>();
                if (obj == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = typeof(T).Name;
                    obj = gameObj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }

    }
    private void OnApplicationQuit()
    {
        isShutDown = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PreInitialize();
        Initialize();
    }
    protected virtual void PreInitialize()
    {
        if(!initialized)
        { 
            initialized = true;
            Scene active = SceneManager.GetActiveScene();
            mainSceneIndex = active.buildIndex;
        }
    }
    protected virtual void Initialize()
    {

    }

    protected virtual void ResetData()
    { 
    }
}
