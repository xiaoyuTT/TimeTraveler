using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour//����Ԥ�������Ϣ
{
    public Item slotItem;//�����ĸ���Ʒ
    public Image slotImage;//��Ӧͼ��
    public Text slotNum;//��Ŀ
    //prefab�Ǹ�button�����Ե����Ӧ�����¼�
    public void ItemOnClick()
    {
        if (slotItem.itemName == "bullet")
        {

        }
        else
        {
            //InventoryManager.ItemInfoSee(slotItem.iteminfo);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Attack>().Setbullet(slotItem.bullet, slotItem);
        }
    }
}
