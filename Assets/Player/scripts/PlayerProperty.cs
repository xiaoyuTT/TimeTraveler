using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    public movementcontrol movementcontroller;
    public bool isTouchEle = false;//�ж��Ƿ������
    public int Hp,Electricity;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void DealDamage(int damage)//����˺������������ܵ��˺��������
    {
        anim.SetBool("beattacked", true);
        Hp -= damage;
        StartCoroutine(WaitSeconds(0.8f));
        anim.SetBool("beattacked", false);
        if (Hp <= 0)
        {
            Death();
        }
    }

    public void GetElectricity(int n)//��غ������õ�����������
    {
            Electricity += n;
    }

    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void DecreaseEle()
    {
        Electricity -= 1;
    }

    void Death() 
    {
        anim.SetBool("dying", true);
        StartCoroutine(WaitSeconds(0.6f));
        anim.SetBool("dying", false);
        movementcontroller.Die();
    }
}
