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

    void CanChange()//判断是否可以进行时空穿越
    {
        int Electricity = GetComponent<PlayerProperty>().Electricity;
        if (Electricity > 0)//电池大于0的时候才可以
        {
            ChangePos();
        }
    }

    void ChangePos()
    {
        if (Input.GetMouseButtonDown(1))//获取鼠标右键点击事件
        {
            if (flag == 0)//flag==0代表在上层
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
