using UnityEngine;

public static class Utils
{
    public static bool ColliderIsPlayer(Collider iCollider)
    {
         return iCollider.gameObject.GetComponent<PlayerController>();
    }
}