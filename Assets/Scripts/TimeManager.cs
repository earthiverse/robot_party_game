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
	    if (CountDown)
	        TimeLeft -= Time.deltaTime;

        // Update Timer
	    _text.text = TimeLeft > 0 ? String.Format("TIME: {0:#.##}", TimeLeft) : "TIME: UP!";
	}
}
