using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedProbs : MonoBehaviour
{
	[SerializeField]
	private float spawnDistance=1.5f;
	[SerializeField]
	private float spawnOffset = 0.5f;
	[SerializeField]
    private int amountOfBigObject;
	[SerializeField]
	private int amountOfSmallObjects;
	[SerializeField]
	private GameObject minSpawnPoint, maxSpawnPoint;
	[SerializeField]
	private GameObject minCastlePoint, maxCastlePoint;
	[SerializeField]
	private List<GameObject> bigObjects;
	[SerializeField]
	private List<GameObject> smallObjects;
	[HideInInspector]
	public List<Vector3> spawnedPositions; 

    void Start()
    {
        StartGeneratingEnviorment(amountOfBigObject, bigObjects);
		StartGeneratingEnviorment(amountOfSmallObjects, smallObjects);
    }
    private void StartGeneratingEnviorment(int amountOfLoops,List<GameObject> objects)
	{
        int nonSucces = 0;
		for (int i = 0; i < amountOfLoops; i++)
		{
			Vector3 spawnPoint = FunctionManager.GetRandomVector3(minSpawnPoint.transform.position, maxSpawnPoint.transform.position);
			spawnPoint.y += spawnOffset;
			if (!CheckInsideCastle(spawnPoint))
			{
				SpawnRandomProb(spawnPoint,objects);
			}
			else
			{
				nonSucces++;
			}
		}
		if (nonSucces > 0)
		{
			StartGeneratingEnviorment(nonSucces,objects);
		}
	}	
	private void SpawnRandomProb(Vector3 spawnPoint, List<GameObject> objects)
	{
		int rand = Random.Range(0, objects.Count);
		GameObject randomProb = objects[rand];
		float randomRotation = Random.Range(0, 360);
		GameObject newGameObject = Instantiate(randomProb, spawnPoint, Quaternion.Euler(new Vector3(0, randomRotation, 0)), transform);
		spawnedPositions.Add(newGameObject.transform.position);
	}
	private bool CheckInsideCastle(Vector3 spawnpoint)
	{
		Vector3 minCastleVec = minCastlePoint.transform.position;
		Vector3 maxCastleVec = maxCastlePoint.transform.position;
		if (spawnpoint.x > minCastleVec.x && spawnpoint.x < maxCastleVec.x
			&& spawnpoint.z > minCastleVec.z && spawnpoint.z < maxCastleVec.z)
		{
			return true;
		}
		return false;
	}


}
