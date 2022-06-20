using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunfire : MonoBehaviour
{
    public GameObject gunBulletPrefeb;
    public GameObject gunBulletPrefeb2;
    private Transform childTrs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        childTrs = transform.Find("pos").gameObject.transform;
        Vector3 v1 = transform.localToWorldMatrix.MultiplyPoint(transform.localPosition);
        Vector3 v2 = childTrs.localToWorldMatrix.MultiplyPoint(childTrs.localPosition);
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (v1.x > v2.x)
            {
                ShootOnLeft();
            }
            else
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Instantiate(gunBulletPrefeb, transform.position, transform.rotation);
    }
    void ShootOnLeft()
    {
        Instantiate(gunBulletPrefeb2, transform.position, transform.rotation);
    }
}
