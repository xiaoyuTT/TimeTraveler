using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBulletControl : MonoBehaviour
{
    public float speed;
    public int damage;
    public float destoryDistance;

    private Rigidbody2D rb2D;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(1,0) * speed;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destoryDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)//9是敌人所在的图层
            collision.gameObject.GetComponent<EnemyControl>().EnemyGetDamage(damage);
        Destroy(gameObject);
    }
}
