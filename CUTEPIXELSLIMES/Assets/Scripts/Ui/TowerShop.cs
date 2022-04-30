using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerShop : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameObject towerBaseObject;
    [SerializeField] private Transform itemParent;

    public List<BaseTower> towerItems;


    private void OnEnable()
    {
        FillShop();
    }
    private void OnDisable()
    {
        foreach (Transform item in transform)
        {
            item.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    void FillShop()
    {
        foreach (Transform item in itemParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in towerItems)
        {
            GameObject newItem = Instantiate(shopItemPrefab, itemParent);
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            //maybe plaatje?
            newItem.GetComponent<Button>().onClick.AddListener(SetButton);
        }
    }
    void SetButton()
    {
        //check for currency


        GameObject newTowerObject = Instantiate(towerBaseObject);
        PlaceItemBase.instance.SetObject(newTowerObject);
        GameManager.instance.ToggleTowerShop(false, true);
    }
}
