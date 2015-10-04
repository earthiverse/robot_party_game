using UnityEngine;
using System.Collections;

public class WallMovement : MonoBehaviour
{
    public static bool Animate = false;

    // Time in seconds it takes for the wall to reach the robot.
    public const float MoveDistance = -30f;

    Vector3 _startPos;
    Vector3 _endPos;

    protected void Start()
    {
        _startPos = transform.position;
        _endPos = transform.position + transform.forward * MoveDistance;
    }

    protected void Update()
    {
        //lerp!
        float perc = (TimeManager.StartTime - TimeManager.TimeLeft) / TimeManager.StartTime;
        transform.position = Vector3.Lerp(_startPos, _endPos, perc);
    }
}
