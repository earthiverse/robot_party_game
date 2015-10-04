using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static HashSet<String> PointsTouching = new HashSet<string>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PointsTouching.Add(collision.name);
        Debug.LogFormat("OnTriggerEnter2D - {0} ({1}", collision.name, PointsTouching);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PointsTouching.Remove(collision.name);
        Debug.LogFormat("OnTriggerExit2D - {0} ({1}", collision.name, PointsTouching);
    }

    public static bool IsColliding()
    {
        return PointsTouching.Count > 0;
    }
}