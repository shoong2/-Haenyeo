using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : Slot
{
    public int needItemCount;

    public override void AddItem(Item _item, int _count = 1)
    {
        // base.AddItem(_item, _count);
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        text_Count.text = needItemCount+"/"+ itemCount.ToString();

        SetColor(1);
    }

    public void AddRecipe(Cooking item_)
    {
        itemImage.sprite = item_.cookingImage;
        SetColor(1);
    }

    public void Test(Item item_)
    {
        itemImage.sprite = item_.itemImage;
        SetColor(1);
    }
}
