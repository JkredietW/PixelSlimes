using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItemBase : MonoBehaviour
{
    public static PlaceItemBase instance;

    private GameObject itemToPlace;
    [SerializeField] Transform towerParent;
    [SerializeField] private LayerMask placeLayer;
    [SerializeField] private Vector3 LocationOffset;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        StayOnMouse();
    }
    void StayOnMouse()
    {
        if(itemToPlace == null)
        {
            return;
        }
        if(Physics.Raycast(PlayerController.instance.RayFromMouse(), out RaycastHit _hit, Mathf.Infinity, placeLayer))
        {
            itemToPlace.transform.position = _hit.point + LocationOffset;
        }
    }
    public void SetObject(GameObject _thisObject)
    {
        itemToPlace = _thisObject;
        itemToPlace.GetComponent<BaseTower>().PlaceTower(true);
    }
    public void PlaceTower()
    {
        itemToPlace.GetComponent<BaseTower>().PlaceTower();
        itemToPlace.transform.SetParent(towerParent);
        itemToPlace = null;
    }
}