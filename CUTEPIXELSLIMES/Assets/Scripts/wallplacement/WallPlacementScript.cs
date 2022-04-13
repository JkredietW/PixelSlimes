using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacementScript : MonoBehaviour
{
    private Grid grid;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void ClickedOnGroundDing(Vector3 position){
        Vector3 setPosition= grid.GetClossedPoint(position);

    }
}
