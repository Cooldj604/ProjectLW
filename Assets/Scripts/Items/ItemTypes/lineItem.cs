using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Crafting Object", menuName = "Inventory System/Items/Line Item")]

public class lineItem : ItemObject
{

    public int length;
    public GameObject sprite;

    public void Awake()
    {
        type = ItemType.Line;
    }

}
