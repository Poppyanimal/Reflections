using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiroLib : MonoBehaviour
{
    public static Vector2 rotateVector2(float angle, Vector2 startV)
    {
        return rotateVector2eul(angle, startV);
    }
    public static Vector2 rotateVector2eul(float angle, Vector2 startV)
    {
        float angleR = angle * Mathf.Deg2Rad;
        return rotateVector2rad(angleR, startV);
    }

    public static Vector2 rotateVector2rad(float angleR, Vector2 startV)
    {
        return new Vector2(
            Mathf.Cos(angleR)*startV.x - Mathf.Sin(angleR)*startV.y,
            Mathf.Sin(angleR)*startV.x + Mathf.Cos(angleR)*startV.y);
    }

    public static float angleToTarget(Vector2 origin, Vector2 target)
    {
        return Mathf.Atan2(target.y - origin.y, target.x - origin.x) * 180 / Mathf.PI + 90;
    }

    public static float normalizeEuler(float e)
    {
        if(e<0)
            e += 360f;
        e %= 360f;
        return e;
    }
    
    public static ContactFilter2D getBulletFilter() { return getFilter("EnemyBullet"); }
    public static ContactFilter2D getPBulletFilter() { return getFilter("PlayerBullet"); }
    public static ContactFilter2D getFakePBulletFilter() { return getFilter("PlayerBulletFake"); }
    public static ContactFilter2D getAllBulletsToClearFilter() { return getFilter(new string[] {"EnemyBullet", "PlayerBullet", "PlayerBulletFake"}); }
    
    public static ContactFilter2D getFilter(string layer) { return getFilter(new string[] {layer}); }
    public static ContactFilter2D getFilter(string[] layers)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.useLayerMask = true;
        filter.layerMask = LayerMask.GetMask(layers);
        return filter;
    }
}