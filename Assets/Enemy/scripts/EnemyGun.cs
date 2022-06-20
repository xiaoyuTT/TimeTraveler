using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
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
        rb2D.velocity = new Vector2(1, 0) * speed;
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
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("进入碰撞");
        if (collision.gameObject.layer == 12)//12是玩家所在的图层
            Debug.Log("人物受伤");
            collision.gameObject.GetComponent<PlayerProperty>().DealDamage(damage);
        Destroy(gameObject);
    }
}
