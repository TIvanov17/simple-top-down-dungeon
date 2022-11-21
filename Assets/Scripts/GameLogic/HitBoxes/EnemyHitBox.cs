using UnityEngine;

public class EnemyHitBox : Collidable
{
    public int hitDamage;
    public float pushForce;

    protected override void OnCollide(Collider2D currentCollider)
    {
        if (ShouldAttack(currentCollider))
        {
            Damage damage = new Damage(transform.position, hitDamage, pushForce);
            currentCollider.SendMessage("ReceiveDamage", damage);
        }
    }

    private bool ShouldAttack(Collider2D currentCollider)
    {
        return currentCollider.name == "Player";
    }
}
