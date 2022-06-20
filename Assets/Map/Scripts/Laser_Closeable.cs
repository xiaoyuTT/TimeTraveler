using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Closeable : MonoBehaviour
{
    public float closetime=2;
    public float opentime=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator  Close()
    {
        yield return new WaitForSeconds(closetime);
        gameObject.GetComponent<Collider2D>().enabled = false;
        //激光关闭图片
        StartCoroutine(Open());
    }
    IEnumerator Open()
    {
        yield return new WaitForSeconds(opentime);
        gameObject.GetComponent<Collider2D>().enabled = true;
        //激光打开图片
        StartCoroutine(Close());
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<movementcontrol>().Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
