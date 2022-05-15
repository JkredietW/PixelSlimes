using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    [SerializeField] StatUiBox towerStatUi;
    [SerializeField] private GameObject towerItemSlotPrefab;
    [SerializeField] private Transform towerSlotParent;
    [SerializeField] int towerSlotAmount = 3;
    private List<GameObject> towerSlots;
    private List<TowerItem> towerItems;
    public void OnOpenPanel()
    {
        SetTowerSlots();
        SetTowerInfoToPanel();//stats calculation done in basetower, before panel is opened.
        CheckForHeldItems();
    }

    void CheckForHeldItems()
    {
        towerItems = new List<TowerItem>(GameManager.instance.currentSelectedTower.heldItems);
        for (int i = 0; i < 3; i++)
        {
            if(i < towerItems.Count)
            {
                towerSlots[i].GetComponent<Image>().sprite = towerItems[i].itemIcon;
                towerSlots[i].GetComponent<TowerSlot>().SwapItem(towerItems[i]);
            }
            else
            {
                towerSlots[i].GetComponent<Image>().sprite = null;
            }
        }
    }
    void SetTowerSlots()
    {
        towerSlots = new List<GameObject>();
        //remove old
        foreach (Transform item in towerSlotParent)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < towerSlotAmount; i++)
        {
            GameObject newSlot = Instantiate(towerItemSlotPrefab, towerSlotParent);
            towerSlots.Add(newSlot);
        }
        for (int i = 0; i < towerSlots.Count; i++)
        {
            int nummer = new int();
            nummer = i;
            towerSlots[i].GetComponent<Button>().onClick.AddListener(() => TowerSlotClicked(nummer));
        }
    }
    void SetTowerInfoToPanel()
    {
        towerStatUi.statTextObjects[0].text = towerStatUi.statName[0] + GameManager.instance.currentSelectedTower.CurrentDamage.ToString("F2");
        towerStatUi.statTextObjects[1].text = towerStatUi.statName[1] + GameManager.instance.currentSelectedTower.AttackSpeed.ToString("F2");
        towerStatUi.statTextObjects[2].text = towerStatUi.statName[2] + GameManager.instance.currentSelectedTower.RotationSpeed.ToString("F2");
        towerStatUi.statTextObjects[3].text = towerStatUi.statName[3] + GameManager.instance.currentSelectedTower.TowerRange.ToString("F2");
    }
    void TowerSlotClicked(int i)
    {
        TowerSlot selectedSlot = towerSlots[i].GetComponent<TowerSlot>();
        TowerItem swappedItem = null;
        if (selectedSlot.HeldItem != null)
        {
            swappedItem = selectedSlot.SwapItem();
        }
        if(swappedItem != null)
        {
            GameManager.instance.currentSelectedTower.RemoveItem(swappedItem.ID);
        }
        SetTowerSlots();
        CheckForHeldItems();
        GameManager.instance.currentSelectedTower.CalculateDamage();
        SetTowerInfoToPanel();
    }
}
