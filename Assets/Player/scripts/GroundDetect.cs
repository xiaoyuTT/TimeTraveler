using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    movementcontrol movescript;
    // Start is called before the first frame update
    void Start()
    {
        movescript = gameObject.transform.parent.GetComponent<movementcontrol>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            movescript.Setfootonground(true);
            movescript.anim.SetBool("jumping", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        movescript.Setfootonground(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
