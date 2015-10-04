using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static HashSet<String> PointsTouching = new HashSet<string>();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PointsTouching.Add(collision.name);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PointsTouching.Remove(collision.name);
    }

    public static bool IsColliding()
    {
        return PointsTouching.Count > 0;
    }
}