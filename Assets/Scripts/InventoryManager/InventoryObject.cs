using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Inventory")]
public class InventoryObject : ScriptableObject
{

    public List<InventorySlot> Backpack = new List<InventorySlot>();

    // Add Item Method
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;

        // Check if item is already in inventory
        for(int i = 0; i < Backpack.Count; i++)
        {
            if(Backpack[i].item = _item)
            {

                Backpack[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }

        if(!hasItem)
        {
            Backpack.Add(new InventorySlot(_item, _amount));
        }
    }

}


// Inventory Slots Class ==============================================================

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    // Constructor
    public InventorySlot(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }

    // Adding to the stack method
    public void AddAmount(int value)
    {
        amount += value;
    }
}
