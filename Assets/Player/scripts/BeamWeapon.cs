using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : MonoBehaviour
{ public float BeamDamage;
    public float MaxDamageDistance;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
           hit= Physics2D.Raycast(transform.position, Vector2.right, MaxDamageDistance);
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(0, transform.position);
            transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, hit.point);
           // hit.collider.gameObject.GetComponent<EnemyControl>().EnemyGetDamage(BeamDamage*Time.deltaTime);
        }
    }
}
