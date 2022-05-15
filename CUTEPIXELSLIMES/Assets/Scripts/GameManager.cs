using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject towerPanel;
    [SerializeField] GameObject towerShopPanel;
    [HideInInspector] public BaseTower currentSelectedTower;
    private GameObject playerObject;

    public GameObject PlayerObject => playerObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            playerObject = PlayerController.instance.gameObject;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ToggleTowerShop(bool value, bool placingTower = false)
    {
        towerShopPanel.SetActive(value);
        if(placingTower)
        {
            PlayerController.instance.SetState(SelectionState.buildMode);
        }
        else if(value)
        {
            PlayerController.instance.SetState(SelectionState.InUi);
        }
        else
        {
            PlayerController.instance.SetState(SelectionState.Default);
        }
    }
    public void OpenTowerPanel(BaseTower currentTower)
    {
        currentSelectedTower = currentTower;
        CameraController.instance.SetFocus(currentSelectedTower.gameObject, true);
        towerPanel.SetActive(true);
        GetComponent<TowerInfoPanel>().OnOpenPanel();
    }
    public void CloseTowerPanel()
    {
        CameraController.instance.SetFocus();
        towerPanel.SetActive(false);
    }
    private void Update()
    {
        //print(PlayerController.instance.SelectionState);
    }
}
[System.Serializable]
public class StatUiBox
{
    public List<TextMeshProUGUI> statTextObjects;
    public List<string> statName;
}
