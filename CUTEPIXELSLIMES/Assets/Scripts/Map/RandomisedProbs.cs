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
	private List<Vector3> spawnedPositions; 
    void Start()
    {
        StartGeneratingEnviorment(amountOfObjects);
		spawnedPositions = new List<Vector3>();
    }
    private void StartGeneratingEnviorment(int amountOfLoops)
	{
        int nonSucces = 0;
		for (int i = 0; i < amountOfLoops; i++)
		{
			Vector3 spawnPoint = GameManager.instance.GetRandomVector3(minSpawnPoint.transform.position, maxSpawnPoint.transform.position);

			for (int x = 0; x < spawnedPositions.Count; x++)
			{

				if (GameManager.instance.CheckDistanceNotSquared(spawnPoint, spawnedPositions[i]) < spawnDistance)
				{
					nonSucces++;
					return;
				}
				else
				{
					SpawnRandomProb(spawnPoint);
				}
			}
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
