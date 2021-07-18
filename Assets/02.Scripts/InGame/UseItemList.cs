using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemList : MonoBehaviour
{
    Dictionary<string, UIUseItem> _useItemList;
    Dictionary<int, UIUseItem> _useItemListCode;

    private void Awake()
    {
        _useItemList = new Dictionary<string, UIUseItem>();
        _useItemListCode = new Dictionary<int, UIUseItem>();

        UnityGoogleSheet.Load<UseItemSheet.Items>();

        var itemList = UseItemSheet.Items.ItemsList;
        foreach(var line in itemList)
        {
            UIUseItem newData = new UIUseItem()
            {
                useItem = new UseItem(),

                index = line.index,
                info = line.Info,
                name = line.Name,
                price = line.price,
            };

            AddItem(newData);
        }
    }

    void AddItem(UIUseItem item)
    {
        _useItemList.Add(item.name, item);
        _useItemListCode.Add(item.index, item);
    }
}

public class UseItem
{
    public int index;
    public string name;
    public string info;
    public int price;
}

public class UIUseItem
{
    public UseItem useItem;

    public string name { get { return useItem.name; } set { useItem.name = value; } }
    public int index { get { return useItem.index; } set { useItem.index = value; } }
    public int price { get { return useItem.price; } set { useItem.price = value; } }
    public string info { get { return useItem.info; } set { useItem.info = value; } }

}