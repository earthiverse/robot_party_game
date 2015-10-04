using UnityEngine;
using System.Collections;

public class WallMovement : MonoBehaviour
{
    // Time in seconds it takes for the wall to reach the robot.
    float lerpTime = 10f;
    float currentLerpTime;

    float moveDistance = -30f;

    Vector3 startPos;
    Vector3 endPos;

    protected void Start()
    {
        startPos = transform.position;
        endPos = transform.position + transform.up * moveDistance;
    }

    protected void Update()
    {
        //reset when we press spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLerpTime = 0f;
        }

        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, perc);
    }
}
