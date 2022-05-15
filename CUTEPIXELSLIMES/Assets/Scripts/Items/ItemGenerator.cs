using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    public static ItemGenerator instance;
    public List<MinMaxItemStats> minMaxItemStats;
    public List<Sprite> randomItemIcons;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public TowerItem GenerateItem(int rarity)
    {
        TowerItem newItem = new TowerItem();
        //icon
        newItem.itemIcon = randomItemIcons[Random.Range(0, randomItemIcons.Count)];
        for (int i = 0; i < rarity; i++)
        {
            ItemStat newStat = new ItemStat();
            int rollStatType = Random.Range(0, System.Enum.GetValues(typeof(ItemStats)).Length);
            float rollStatValue = Random.Range(minMaxItemStats[rollStatType].min, minMaxItemStats[rollStatType].max);

            newStat.Setup((ItemStats)rollStatType, rollStatValue);

            newItem.itemStats.Add(newStat);
        }

        if (string.IsNullOrEmpty(newItem.ID))
        {
            Guid guid = Guid.NewGuid();
            newItem.ID = guid.ToString();
        }
        return newItem;
    }
}
[System.Serializable]
public class MinMaxItemStats
{
    public ItemStats name;
    public float min;
    public float max;
}
[System.Serializable]
public class TowerItem
{
    public Sprite itemIcon;
    public List<ItemStat> itemStats = new List<ItemStat>();
    public string ID;
}
[System.Serializable]
public class ItemStat
{
    public ItemStats statType;
    public float amount;

    public void Setup(ItemStats stat, float value)
    {
        statType = stat;
        amount = value;
    }
}
public enum ItemStats
{
    DamageFlat,
    DamagePercentage,
    AttackSpeedFlat,
    AttackSpeedPercentage,//moeten meer stats? ook min-max list aanpassen dan
}