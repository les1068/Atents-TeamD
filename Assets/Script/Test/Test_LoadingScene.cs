using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test_LoadingScene : TestBase
{
    AsyncOperation async;
    protected override void Test1(InputAction.CallbackContext _)
    {
        //SceneManager.LoadScene(1);//동기방식(Synchronous)
        StartCoroutine(LoadScene());
       
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        async.allowSceneActivation = true;
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(3);
        async.allowSceneActivation = false; // 씬전환 즉시 하지 않고 대기

        while (!async.isDone)
        {
            Debug.Log($"Progress : {async.progress}");
            yield return null;
        }

        Debug.Log("Loading Complete");

    }
}
