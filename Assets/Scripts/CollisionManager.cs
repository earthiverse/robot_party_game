using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static uint PointsTouching;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PointsTouching += 1;
        Debug.LogFormat("OnTriggerEnter2D - {0} ({1}", collision.name, PointsTouching);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        PointsTouching -= 1;
        Debug.LogFormat("OnTriggerExit2D - {0} ({1}", collision.name, PointsTouching);
    }

    public static bool IsColliding()
    {
        return PointsTouching > 0;
    }
}