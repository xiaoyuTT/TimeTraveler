using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Attack : MonoBehaviour
{
    public Camera Playercamera;//������
    Vector3 mousevector;
    public GameObject theplayer;//�������
    GameObject bullet;//Ͷ������Ʒ
    Item bulletType;
    public float Time_count_CD;//Ͷ��CD����λ��
    private float Time_count;//��ʱ������ʣ��CDʱ�䣩
    bool CDflag = false;
    private GameObject bullets;//��ʱ����
    private Rigidbody2D rigid;//��ʱ����
    private Animator anim;
    public Transform leftShootPos, rightShootPos;
    public Item gunBullet;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!CDflag)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                mousevector = Input.mousePosition;
                //Debug.Log(mousevector.x);
                //Debug.Log(mousevector.y);
                if(mousevector.x>300||mousevector.y<310)
                {
                    mousevector = Playercamera.ScreenToWorldPoint(mousevector);
                    mousevector -= theplayer.transform.position;
                    vectortransform(mousevector);
                    if (bullet != null&& bulletType.itemHeld != 0)
                    {
                        anim.SetBool("attacking", true);                      
                        //�����ӵ�
                        bullets = GameObject.Instantiate(bullet, leftShootPos.position, Quaternion.identity, GameObject.Find("background").transform);
                        //�ı��ӵ��ٶ�
                        rigid = bullets.GetComponent<Rigidbody2D>();
                        rigid.velocity = mousevector;
                        if (bulletType.itemName == "buqiang")
                        {
                            StartCoroutine(WaitSeconds(0.5f));
                            bullets = GameObject.Instantiate(bullet, leftShootPos.position, Quaternion.identity, GameObject.Find("background").transform);
                            //�ı��ӵ��ٶ�
                            rigid = bullets.GetComponent<Rigidbody2D>();
                            rigid.velocity = mousevector;
                            itemOnWorld.DecreaseItem(gunBullet);
                        }
                        //��CD
                        StartCoroutine(wait());
                        CDflag = true;
                        Time_count = Time.time;
                        if(bulletType.itemName == "shouqiang")
                        {
                            itemOnWorld.DecreaseItem(gunBullet);
                        }
                        else
                        {
                            itemOnWorld.DecreaseItem(bulletType);
                        }
                    }
                } 
            }
        }
                //CD
        else
            if (Time.time >= Time_count + Time_count_CD)
        {
            CDflag = false;
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("attacking", false);

    }

    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public static void vectortransform(Vector3 vector3) 
    {
        vector3.Set(vector3.x*10, vector3.y*10, vector3.z*10);//�����任��ûд��Ӧ���޶��ٶ���������
        
    }
    public void Setbullet(GameObject b,Item thisitemType)
    {
        bullet = b;
        bulletType = thisitemType;
    }

}
