using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Skill3 : PoolObject
{
    PlayerInputAction inputActions;
    Player player;
    Transform transSkill;
    Animator animSkill;
    Vector3 inputDir = Vector3.zero;

    /// <summary>
    /// 발사할 총알 프리팹
    /// </summary>
    public GameObject bullet;

    /// <summary>
    /// 총알 발사 시간 간격
    /// </summary>
    public float skillInterval;

    /// <summary>
    /// 총알을 계속 발사하는 코루틴
    /// </summary>
    IEnumerator skillCoroutine;

    /// <summary>
    /// 총알 발사 시간 간격만큼 기다리는 WaitForSeconds
    /// </summary>
    WaitForSeconds waitSkillInterval;
        
    public bool isLeft = false;

    /// <summary>
    /// 스킬 데미지 계산용 변수
    /// </summary>
    public float skillValue = 1.0f;

    /// <summary>
    /// 스킬 데미지 계산 후 변수 
    /// </summary>
    public float skillPower
    {
        get => skillPower;
        set
        {
            skillPower = value * skillValue * player.attackPoint;
        }
    }

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        animSkill = GetComponent<Animator>();
        transSkill = GetComponent<Transform>();
        skillCoroutine = skillLoop();
    }

    void Start()
    {
        waitSkillInterval = new WaitForSeconds(skillInterval);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMoveInput;
        inputActions.Player.Move.canceled += OnMoveInput;
        inputActions.Player.Attack3.performed += OnSkill3;
    }

    protected override void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMoveInput;
        inputActions.Player.Move.canceled -= OnMoveInput;
        inputActions.Player.Disable();
        inputActions.Player.Attack3.performed -= OnSkill3;
        base.OnDisable();

    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        inputDir = dir;

        if (dir.x > 0)                                            // 마지막 이동 위치 확인용 
        {
            isLeft = false;
        
        }
        if (dir.x < 0)
        {
            isLeft = true;
        
        }
    }

    public void OnSkill3(InputAction.CallbackContext context)   // 키보드 A키
    {
        GameObject obj = Factory.Inst.GetObject(PoolObjectType.Bullet); //풀에서 Bullet빼서쓰는걸로 변경함
        float posX = transSkill.position.x;
        float posY = transSkill.position.y;
        if(isLeft)
        {
            obj.transform.position = new Vector2(posX - 1, posY);
        }
        else
        {
            obj.transform.position = new Vector2(posX + 1, posY);
        }
        
    }

    /// <summary>
    /// 주기적으로 총알을 발사하는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator skillLoop()
    {
        while (true)
        {
            OnFire();
            yield return waitSkillInterval;
        }
    }

    /// <summary>
    /// 총알을 한발 발사하는 함수.
    /// </summary>
    protected virtual void OnFire()
    {
    }
}