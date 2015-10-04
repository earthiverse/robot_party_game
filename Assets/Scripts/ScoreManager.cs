using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public static int Score;

    private Text _text;

	void Awake () {
        // Set up the reference
        _text = GetComponent<Text>();

        // Reset the score
        Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        _text.text = "Score: " + Score;
	}
}
