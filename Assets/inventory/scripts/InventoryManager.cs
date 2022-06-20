using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public GameObject slotGrid;//获取格，获取的物品加到其子物体中，则可再在背包显示
    public slot slotPrefab;//获取预制体，复制。并且可以设置其属性信息
    public Text itemInfo;//物品描述

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();
        instance.itemInfo.text = "";
    }

    //获得文本信息
    public static void ItemInfoSee(string itemDescripton)
    {
        instance.itemInfo.text = itemDescripton;
    }

    public static void CreateNewItem(Item item)//创建物品，参数为物品类型，使其可以调用相应数据
    {
        //复制slot，其信息通过item设置
        slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);

        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void DeleteItem(Item item)
    {
        int n = instance.slotGrid.transform.childCount;
        for(int i=0;i<n;i++)
        {
            if(instance.slotGrid.transform.GetChild(i).gameObject.GetComponent<slot>().slotItem==item)
            {
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            }
        }
    }

    public static void RefreshItem()//更新物品数
    {
        for(int i =0;i<instance.slotGrid.transform.childCount;i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<ItemInteract.itemList.Count;i++)
        {
            CreateNewItem(ItemInteract.itemList[i]);
        }
    }
}
