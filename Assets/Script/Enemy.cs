using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 2.0f;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 dirVec = target.position- rigid.position;   // 타겟포지션 - 나의 포지션
        Vector2 nextVec = dirVec.normalized * enemySpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        
    }

    /// <summary>
    /// Enemy �ִ� HP
    /// </summary>
    protected float maxHP = 100.0f;

    /// <summary>
    /// Enemy ���� HP
    /// </summary>
    protected float currentHP = 100.0f;

    public float GetEnemyHP()
    {
        return currentHP;
    }

    public TMP_Text EnemyHpText;

    /// <summary>
    /// Enemy ����� player�� ��� �� ����ġ
    /// </summary>
    protected float exp = 30.0f;

    /// <summary>
    /// Enemy ���ݷ�
    /// </summary>
    protected float attackDamage = 10.0f;

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    /// <summary>
    /// Enemy ����
    /// </summary>
    protected float EnemyDefence = 20.0f;

    /// <summary>
    /// Enemy �̵��ӵ�
    /// </summary>
    protected float moveSpeed = 10.0f;

    /// <summary>
    /// Enemy ���� �ӵ�
    /// </summary>
    protected float enemyAttackSpeed = 8.0f;

    /// <summary>
    /// Enemy ������ flase, ����� true
    /// </summary>
    bool isEnemyDead = false;

    /// <summary>
    /// Enemy HP ��������Ʈ
    /// </summary>
    public Action<int> onChangeEnemyHP;

    Player player = FindObjectOfType<Player>();
    PlayerStat playerStat = FindObjectOfType<PlayerStat>();

    protected Transform target;
    
    void Start()
    {
        target = player.transform;
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;      // ��ġ �ʱ�ȭ
    }

    private void Update()
    {
        EnemyAttack();   
    }

    private void EnemyAttack()
    {
        // �÷��̾� �������� �����ϴ� �Լ� �����
        transform.localPosition += Time.deltaTime * enemyAttackSpeed * -transform.right;    // �������� �̵�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentHP != 0)
        {
            // Player �浹�� Enemy HP ����
            if (collision.gameObject.CompareTag("PlayerAttack"))
            {
                onDamageEnemy();
            }
        }
        // Enemy ���̸�, player ����ġ ����
        else if (currentHP < 1)
        {
            isEnemyDead = true;
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        if (!isEnemyDead)
        {
            playerStat.AddExp((int)exp);    // playerStat�� exp�� int. Enemy�� exp�� float. player�� exp �߰�
            gameObject.SetActive(false);    // Enemy ��Ȱ��ȭ
        }
    }

    private void onDamageEnemy()
    {
        currentHP = currentHP - playerStat.EXP;     // player Attack ���� �Ұ�. ���Ƿ� EXP �Է�
        EnemyHpText.text = "HP: " + currentHP.ToString();
    }
}
