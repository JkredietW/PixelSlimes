using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    public static Grid instance;
    [Range(0.1f, 10.0f)]
    [SerializeField]
    private float sizeDefault = 0.1f;
    [SerializeField]
    private Vector2 gridSize;
    private List<Vector3> gridPoints;

    public float size { get { return sizeDefault; } }
	private void Start()
	{
        gridPoints = new List<Vector3>();
        Gizmos.color = Color.magenta;
        for (float x = -gridSize.x; x < gridSize.x; x += sizeDefault)
        {
            for (float z = -gridSize.y; z < gridSize.y; z += sizeDefault)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, transform.position.y, z));
                gridPoints.Add(point);
            }
        }
    }
    public Vector3 GetClossedPoint(Vector3 raycastPos)
	{
		if (gridPoints.Count < 0)
		{
            Debug.LogError("grid points list is empty (line 34 Grid.cs)");
		}
        Vector3 clossedPos = gridPoints[0];

        for (int i = 0; i < gridPoints.Count; i++)
		{
            if (GameManager.instance.CheckDistanceNotSquared(raycastPos,clossedPos)>
                GameManager.instance.CheckDistanceNotSquared(raycastPos, gridPoints[i]))
			{
                clossedPos = gridPoints[i];
            }
		}
        return clossedPos;
	}
    private Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position += transform.position;

        int xCount = Mathf.RoundToInt(position.x / sizeDefault);
        int yCount = Mathf.RoundToInt(position.y / sizeDefault);
        int zCount = Mathf.RoundToInt(position.z / sizeDefault);
        Vector3 result = new Vector3(
            (float)xCount * sizeDefault,
            (float)yCount * sizeDefault,
            (float)zCount * sizeDefault);
        result += transform.position;
        return result;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        for (float x = -gridSize.x; x < gridSize.x; x += sizeDefault)
        {
            for (float z = -gridSize.y; z < gridSize.y; z += sizeDefault)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, transform.position.y, z));
                Gizmos.DrawSphere(point, 0.15f);
            }
        }
    }
}