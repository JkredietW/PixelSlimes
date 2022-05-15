using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    private TowerItem heldItem;
    private TowerItem tempItem;

    public TowerItem HeldItem => heldItem;

    public TowerItem SwapItem(TowerItem newItem = null)
    {
        if(newItem == null)
        {
            tempItem = heldItem;
            heldItem = null;
            return tempItem;
        }
        else
        {
            tempItem = heldItem;
            heldItem = newItem;
            return tempItem;
        }
    }
}
