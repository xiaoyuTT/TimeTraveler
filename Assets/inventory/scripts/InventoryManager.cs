using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public GameObject slotGrid;//��ȡ�񣬻�ȡ����Ʒ�ӵ����������У�������ڱ�����ʾ
    public slot slotPrefab;//��ȡԤ���壬���ơ����ҿ���������������Ϣ
    public Text itemInfo;//��Ʒ����

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

    //����ı���Ϣ
    public static void ItemInfoSee(string itemDescripton)
    {
        instance.itemInfo.text = itemDescripton;
    }

    public static void CreateNewItem(Item item)//������Ʒ������Ϊ��Ʒ���ͣ�ʹ����Ե�����Ӧ����
    {
        //����slot������Ϣͨ��item����
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

    public static void RefreshItem()//������Ʒ��
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
