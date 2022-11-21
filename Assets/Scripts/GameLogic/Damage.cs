using UnityEngine;

public class Damage
{
    public Vector3 origin;
    public int hitDamage;
    public float pushForce;

    public Damage(Vector3 origin, int hitDamage, float pushForce)
    {
        this.origin = origin;
        this.hitDamage = hitDamage;
        this.pushForce = pushForce;
    }
}
