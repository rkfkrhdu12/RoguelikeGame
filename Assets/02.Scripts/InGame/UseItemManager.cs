using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemManager : MonoBehaviour
{


    private void Awake()
    {
        UnityGoogleSheet.Load<UseItemSheet.Items>();

        var itemList = UseItemSheet.Items.ItemsList;
        foreach(var line in itemList)
        {

        }
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
    UseItem useItem;

    public string name { get { return useItem.name; } set { useItem.name = value; } }
    public int index { get { return useItem.index; } set { useItem.index = value; } }
    public int price { get { return useItem.price; } set { useItem.price = value; } }
    public string info { get { return useItem.info; } set { useItem.info = value; } }

}