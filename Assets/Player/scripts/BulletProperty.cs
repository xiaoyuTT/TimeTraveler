using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    public int Bulletdamage;//投掷物能造成的伤害

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetBulletdamage(int d) 
    {
        Bulletdamage = d;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)//9是敌人所在的图层
            collision.gameObject.GetComponent<EnemyControl>().EnemyGetDamage(Bulletdamage);
        Destroy(gameObject);
    }
}
