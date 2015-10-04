using System;
using UnityEngine;

public class SpawnWalls : MonoBehaviour {

    public GameObject SpawnPointObject;
    public Transform SpawnPoint;
    public GameObject[] WallObjects;
    private static System.Random random = new System.Random();
    private int randomNumber = 0; 
    

	// Use this for initialization
	void Start () {

        randomNumber = random.Next(WallObjects.Length);
        Instantiate(WallObjects[randomNumber], SpawnPoint.localPosition, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {

        if (TimeManager.TimeLeft <= 0)
        {
            DestroyAndSpawnNewWall();
        }            
	}

    private System.Collections.Generic.IEnumerable<WaitForSeconds> DestroyAndSpawnNewWall()
    {
        DestroyObject(WallObjects[randomNumber]);
        yield return new WaitForSeconds(2);
        TimeManager.StartTime -= 1f;
        randomNumber = random.Next(WallObjects.Length);
        Instantiate(WallObjects[randomNumber], SpawnPoint.localPosition, Quaternion.identity);
    }
}