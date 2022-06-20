using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject gunBulletPrefeb;
    public GameObject gunBulletPrefeb2;
    private Transform childTrs;
    private bool seePlayer;
    // Start is called before the first frame update
    void Start()
    {
        seePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        Vector3 v1 = transform.localToWorldMatrix.MultiplyPoint(transform.localPosition);
        Vector3 v2 = childTrs.localToWorldMatrix.MultiplyPoint(childTrs.localPosition);
        if (seePlayer)
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
    void CheckPlayer()
    {
        childTrs = transform.Find("pos1").gameObject.transform;
        Vector3 v2 = childTrs.localToWorldMatrix.MultiplyPoint(childTrs.localPosition);
        seePlayer = Physics2D.Linecast(transform.position, v2, 1 << LayerMask.NameToLayer("Player"));
    }
}
