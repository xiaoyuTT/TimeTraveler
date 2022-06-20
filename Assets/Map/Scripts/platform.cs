using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public Collider2D Thisupcollider;
    public Vector3 localscale;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Countdown());
    }
    // Start is called before the first frame update
    void Start()
    {
        localscale = gameObject.transform.parent.gameObject.transform.localScale;
    }

    IEnumerator Countdown()
    {
        
            yield return new WaitForSeconds(3);
        //Debug.Log("∆ΩÃ®ÀÈ¡—£°");
        StartCoroutine(Countdown2());
        gameObject.transform.parent.gameObject.transform.localScale=Vector3.zero;
    }
    IEnumerator Countdown2()
    {

        yield return new WaitForSeconds(3);
        gameObject.transform.parent.gameObject.transform.localScale = localscale;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
