using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour//复制预制体的信息
{
    public Item slotItem;//属于哪个物品
    public Image slotImage;//对应图像
    public Text slotNum;//数目
    //prefab是个button，可以点击响应以下事件
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
