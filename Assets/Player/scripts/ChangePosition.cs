using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    private int flag = 0;
    public float distance = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanChange();
    }

    void CanChange()//�ж��Ƿ���Խ���ʱ�մ�Խ
    {
        int Electricity = GetComponent<PlayerProperty>().Electricity;
        if (Electricity > 0)//��ش���0��ʱ��ſ���
        {
            ChangePos();
        }
    }

    void ChangePos()
    {
        if (Input.GetMouseButtonDown(1))//��ȡ����Ҽ�����¼�
        {
            if (flag == 0)//flag==0�������ϲ�
            {
                Vector3 target_pos = new Vector3(transform.position.x, transform.position.y - distance,transform.position.z);
                transform.position = target_pos;
                flag = 1;
            }
            else if (flag == 1)
            {
                Vector3 target_pos = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
                transform.position = target_pos;
                flag = 0;
            }
            GetComponent<PlayerProperty>().DecreaseEle();
        }
    }
}
