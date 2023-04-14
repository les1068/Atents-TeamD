using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBase : MonoBehaviour
{
    PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputAction.Test.Enable();
        inputAction.Test.Test1.performed += Test1;
        inputAction.Test.Test2.performed += Test2;
        inputAction.Test.Test3.performed += Test3;
        inputAction.Test.Test4.performed += Test4;
        inputAction.Test.Test5.performed += Test5;
    }

    private void OnDisable()
    {
        inputAction.Test.Test5.performed -= Test5;
        inputAction.Test.Test4.performed -= Test4;
        inputAction.Test.Test3.performed -= Test3;
        inputAction.Test.Test2.performed -= Test2;
        inputAction.Test.Test1.performed -= Test1;
        inputAction.Test.Disable();
        
    }
    protected virtual void Test1(InputAction.CallbackContext _)
    {
       
    }

    protected virtual void Test2(InputAction.CallbackContext _)
    {
       
    }

    protected virtual void Test3(InputAction.CallbackContext _)
    {
       
    }

    protected virtual void Test4(InputAction.CallbackContext _)
    {
       
    }

    protected virtual void Test5(InputAction.CallbackContext _)
    {
       
    }

}
