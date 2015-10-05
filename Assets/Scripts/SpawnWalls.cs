using System;
using UnityEngine;
using Random = System.Random;

public class SpawnWalls : MonoBehaviour
{
    private static readonly Random random = new Random();
    private static GameObject _currentGameObject;
    private int _randomNumber;
    public AudioSource Bgm;
    public AudioSource DefeatSound;
    public bool Spawning = true;
    public Transform SpawnPoint;
    public GameObject SpawnPointObject;
    public GameObject[] WallObjects;
    // Use this for initialization
    private void Start()
    {
        _randomNumber = random.Next(WallObjects.Length);
        _currentGameObject =
            Instantiate(WallObjects[_randomNumber], SpawnPoint.localPosition, Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset time
            TimeManager.TimeLeft = 10f;
            TimeManager.StartTime = 10f;

            // Clear Score
            ScoreManager.Score = 0;

            // Clear Parts List
            CollisionManager.PointsTouching.Clear();
            RobotKinectMovement.Animate = true;

            Bgm.Play();
            DestroyAndSpawnNewWall();
            Spawning = true;
        }

        if (Spawning)
        {
            if (TimeManager.TimeLeft <= 0)
            {
                if (CollisionManager.IsColliding())
                {
                    // You lost :(
                    Debug.Log("You lost :(");
                    RobotKinectMovement.Animate = false;

                    // Stop the audio!
                    Bgm.Stop();
                    DefeatSound.Play();
                    Spawning = false;
                }
                else
                {
                    // Points!
                    ScoreManager.Score += 10;
                    Debug.Log("Destroying & Spawning!");
                    TimeManager.StartTime -= 0.5f;
                    TimeManager.StartTime = Math.Max(5, TimeManager.StartTime);
                    TimeManager.TimeLeft = TimeManager.StartTime;
                    DestroyAndSpawnNewWall();
                }
            }
        }
    }

    private void DestroyAndSpawnNewWall()
    {
        Destroy(_currentGameObject);

        // Get a new random number
        _randomNumber = random.Next(WallObjects.Length);

        _currentGameObject =
            Instantiate(WallObjects[_randomNumber], SpawnPoint.localPosition, Quaternion.identity) as GameObject;
    }
}