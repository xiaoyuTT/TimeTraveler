using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sting : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            movementcontrol con = collision.gameObject.GetComponent<movementcontrol>();
            con.Die();

        
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
