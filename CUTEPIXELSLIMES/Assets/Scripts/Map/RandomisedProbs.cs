using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedProbs : MonoBehaviour
{
	[SerializeField]
	private float spawnDistance=1.5f;
	[SerializeField]
    private int amountOfObjects;
	[SerializeField]
	private GameObject minSpawnPoint, maxSpawnPoint;
	[SerializeField]
	private List<GameObject> randomProbs;
	[HideInInspector]
	public List<Vector3> spawnedPositions; 
    void Start()
    {
        StartGeneratingEnviorment(amountOfObjects);
    }
    private void StartGeneratingEnviorment(int amountOfLoops)
	{
        int nonSucces = 0;
		for (int i = 0; i < amountOfLoops; i++)
		{
			Vector3 spawnPoint = FunctionManager.GetRandomVector3(minSpawnPoint.transform.position, maxSpawnPoint.transform.position);

			SpawnRandomProb(spawnPoint);
		}
		if (nonSucces > 0)
		{
			StartGeneratingEnviorment(nonSucces);
		}
	}	
	private void SpawnRandomProb(Vector3 spawnPoint)
	{
		int rand = Random.Range(0, randomProbs.Count);
		GameObject randomProb = randomProbs[rand];
		GameObject newGameObject = Instantiate(randomProb, spawnPoint,Quaternion.identity,transform);

		spawnedPositions.Add(newGameObject.transform.position);
	}


}
