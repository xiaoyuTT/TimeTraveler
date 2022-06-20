using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int Health = 2;
    public int moveSpeed;
    public int damage = 1;

    public Vector2 minPosition;//�����ƶ��ı߽磬���������߽�
    public Vector2 maxPosition;//��������ұ߽�
    private Vector2 nowPosition;//�����������ڵ�λ��

    private Rigidbody2D enemyRigidbody;
    private CircleCollider2D enemyBody;

    private int flag = -1 ;//flag=-1�����ߣ�flag=1������
    private bool doHurt = false;//����˺���������ԭ�صȴ�duration��
    private float duration = 2.0f;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        nowPosition = transform.position;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyBody = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyMove();
        nowPosition = transform.position;
    }

    void EnemyMove()
    {
        if (doHurt)
        {
            Invoke(nameof(StopSleep), duration);
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed *flag, enemyRigidbody.velocity.y);
            if (nowPosition.x < minPosition.x + 0.1)
            {
                flag = 1;
            }
            else if (nowPosition.x > maxPosition.x - 0.1)
            {
                flag = -1;
            }
            gameObject.transform.localScale = new Vector3(flag*-0.2f, gameObject.transform.localScale.y, 0);
        }
    }

    void StopSleep()
    {
        doHurt = false;
    }
 
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(111);
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyAttack(other.gameObject);
        }
    }

    void EnemyAttack(GameObject other)
    {
        other.GetComponent<PlayerProperty>().DealDamage(damage);
        doHurt = true;
    }

    public void EnemyGetDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
            Death();
    }

    void Death()
    {
        Destroy(gameObject);
    }

}
