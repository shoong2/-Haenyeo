using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //���� �θ�
    [SerializeField]
    GameObject go_SlotsParent;

    //���Ե�
    Slot[] slots;

    public Slot[] GetSlots() { return slots; }

    [SerializeField] Item[] items;

    public void LoadToInven(int _arrayNum, string _itemName, int _itemNum)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == _itemName)
                slots[_arrayNum].AddItem(items[i], _itemNum);
        }
    }

    private void Awake()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    private void Start()
    {
        //SaveNLoad.instance.LoadData();
        Debug.Log("start");
    }


     
    public void AcquireItem(Item _item, bool quest,int _count =1)
    {
        if (Item.ItemType.Tool != _item.itemType)
        {
           Debug.Log(slots.Length);
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

}
