using System;
using UnityEngine;

public class SpawnWalls : MonoBehaviour {

    public GameObject SpawnPointObject;
    public Transform SpawnPoint;
    public GameObject[] WallObjects;
    private static System.Random random = new System.Random();
    private int _randomNumber;

    private static GameObject _currentGameObject;

	// Use this for initialization
	void Start () {
        _randomNumber = random.Next(WallObjects.Length);
        _currentGameObject = Instantiate(WallObjects[_randomNumber], SpawnPoint.localPosition, Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (TimeManager.TimeLeft <= 0)
        {
            TimeManager.StartTime = Math.Min(5, TimeManager.StartTime--); ;
            DestroyAndSpawnNewWall();
        }            
	}

    private void DestroyAndSpawnNewWall()
    {
        Destroy(_currentGameObject);
        _currentGameObject = Instantiate(WallObjects[_randomNumber], SpawnPoint.localPosition, Quaternion.identity) as GameObject;
    }
}