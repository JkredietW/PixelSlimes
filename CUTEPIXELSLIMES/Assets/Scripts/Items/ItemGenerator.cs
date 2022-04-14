using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public static ItemGenerator instance;
    public List<MinMaxItemStats> minMaxItemStats;

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
        for (int i = 0; i < rarity; i++)
        {
            ItemStat newStat = new ItemStat();
            int rollStatType = Random.Range(0, System.Enum.GetValues(typeof(ItemStats)).Length);
            float rollStatValue = Random.Range(minMaxItemStats[rollStatType].min, minMaxItemStats[rollStatType].max);

            newStat.Setup((ItemStats)rollStatType, rollStatValue);

            newItem.itemStats.Add(newStat);
            print($"type: {newItem.itemStats[i].statType}; value: {newItem.itemStats[i].amount}");
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
    public List<ItemStat> itemStats = new List<ItemStat>();
}
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