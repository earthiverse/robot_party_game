using UnityEngine;
using System.Collections;

public class WallMovement : MonoBehaviour
{
    public bool Animate = true;

    public const float MoveDistance = -30f;

    Vector3 _startPos;
    Vector3 _endPos;

    private GameObject _object;

    protected void Start()
    {
        _object = GetComponent<GameObject>();
        _startPos = transform.position;
        _endPos = transform.position + transform.forward * MoveDistance;
    }

    protected void Update()
    {
        if (!Animate)
            return;

        float perc = (TimeManager.StartTime - TimeManager.TimeLeft) / TimeManager.StartTime;
        transform.position = Vector3.Lerp(_startPos, _endPos, perc);
    }
}
