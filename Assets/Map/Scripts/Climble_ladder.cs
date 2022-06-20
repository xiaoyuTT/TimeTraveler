using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climble_ladder : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("in");
            collision.gameObject.GetComponent<movementcontrol>().canClimb=true ;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if(collision.gameObject.GetComponent<movementcontrol>().currentstate.ToString() =="Climbing" )
            collision.gameObject.GetComponent<movementcontrol>().canClimb = false;
        }
    }

}
