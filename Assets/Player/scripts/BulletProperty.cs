using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    public int Bulletdamage;//Ͷ��������ɵ��˺�

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
        if (collision.gameObject.layer == 9)//9�ǵ������ڵ�ͼ��
            collision.gameObject.GetComponent<EnemyControl>().EnemyGetDamage(Bulletdamage);
        Destroy(gameObject);
    }
}
