using System;
using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;

public class TouchingText : MonoBehaviour {

    private Text _text;

    void Awake()
    {
        // Set up the reference
        _text = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        StringBuilder sb = new StringBuilder();
	    foreach(string part in CollisionManager.PointsTouching)
	    {
	        sb.AppendFormat("{0}" + Environment.NewLine, part);
	    }
	    _text.text = sb.ToString();
	}
}
