using System;
using UnityEngine;

public struct DamageInfo
{
    public float amount;
    public Vector2 hitPoint;
    public Vector2 direction;
    public object source; 

    public DamageInfo(float amount, Vector2 hitPoint, Vector2 dir, object source)
    {
        this.amount = amount;
        this.hitPoint = hitPoint;
        this.direction = dir;
        this.source = source;
    }
}
