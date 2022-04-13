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
        //hier in panel zetten
        towerPanel.SetActive(true);
    }
    void SetInfoToPanel()
    {
        
    }
    public float CheckDistanceNotSquared(Vector3 a,Vector3 b)
	{
        float abX = a.x - b.x;
        float abY = a.y - b.y;
        float abZ = a.z - b.z;
        return (abX * abX + abY * abY + abZ * abZ);
    }
    public Vector3 GetRandomVector3(Vector3 a,Vector3 b)
	{
        float x = Random.Range(a.x, b.x);
        float y = Random.Range(a.y, b.y);
        float z = Random.Range(a.z, b.z);
        return new Vector3(x, z, y);
	}
}
