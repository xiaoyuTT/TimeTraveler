using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{ public int damage=10;
    public float radios=3;
    private List<Collider2D> collider2Ds=new List<Collider2D>();
    ContactFilter2D contact;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        contact = new ContactFilter2D();
        contact.SetLayerMask(LayerMask.GetMask("enemy"));
        Invoke("boom", 4f);
    }
    void boom()
    {
        int n=Physics2D.OverlapCircle(transform.position,radios,contact,collider2Ds);
        if(n!=0)
        foreach (Collider2D collider in collider2Ds)
        {
            collider.gameObject.GetComponent<EnemyControl>().EnemyGetDamage(damage);
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
