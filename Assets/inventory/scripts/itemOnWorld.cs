using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public Item thisItem;//属于哪种类型物品

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }
    public void AddNewItem()
    {
        if (!ItemInteract.itemList.Contains(thisItem))
        {
            ItemInteract.itemList.Add(thisItem);
            //InventoryManager.CreateNewItem(thisItem);
            thisItem.itemHeld += 1;
        }
        else
        {
            thisItem.itemHeld += 1;
        }
        InventoryManager.RefreshItem();
    }
    static public void DecreaseItem(Item attackItem)
    {
        if (attackItem.itemHeld==1)
        {
            ItemInteract.itemList.Remove(attackItem);
            InventoryManager.DeleteItem(attackItem);
            attackItem.itemHeld -= 1;
        }
        else
        {
            attackItem.itemHeld -= 1;
        }
        InventoryManager.RefreshItem();
    }
}
