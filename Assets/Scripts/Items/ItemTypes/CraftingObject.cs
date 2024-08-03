using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Crafting Object", menuName = "Inventory System/Items/Crafting")]

public class CraftingObject : ItemObject
{

    public int damage;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
