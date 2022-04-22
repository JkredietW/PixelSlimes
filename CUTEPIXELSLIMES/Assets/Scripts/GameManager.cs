using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject towerPanel;
    private BaseTower currentSelectedTower;
    private GameObject playerObject;

    public GameObject PlayerObject => playerObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            playerObject = FindObjectOfType<PlayerControllerNameSpace.PlayerController>().gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OpenTowerPanel(BaseTower currentTower)
    {
        currentSelectedTower = currentTower;
        CameraController.instance.SetFocus(currentSelectedTower.gameObject, true);
        towerPanel.SetActive(true);
    }
    public void CloseTowerPanel()
    {
        CameraController.instance.SetFocus();
        towerPanel.SetActive(false);
    }
    void SetInfoToPanel()//moet nog
    {
        
    }
}
