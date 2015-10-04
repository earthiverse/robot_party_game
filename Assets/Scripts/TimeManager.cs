using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    // Time in seconds before wall hits
    public static float StartTime = 10f;
    public static bool CountDown = true;

    public static float TimeLeft;

    private Text _text;

    void Awake()
    {
        // Set up the reference
        _text = GetComponent<Text>();
    }

	void Start () {
        TimeLeft = StartTime;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Reset time
            TimeLeft = StartTime;

            // Re-enable animation
            RobotKinectMovement.Animate = true;
            WallMovement.Animate = true;
            CountDown = true;
        }

        if (TimeLeft <= 0)
        {
            // Check
            if (!CollisionManager.IsColliding() && CountDown)
                ScoreManager.Score += 10;

            TimeLeft = 0f;
            RobotKinectMovement.Animate = false;
            WallMovement.Animate = false;
            CountDown = false;

        }

	    if (CountDown)
	        TimeLeft -= Time.deltaTime;

        // Update Timer
	    _text.text = TimeLeft > 0 ? String.Format("TIME: {0:#.##}", TimeLeft) : "TIME: UP!";
	}
}
