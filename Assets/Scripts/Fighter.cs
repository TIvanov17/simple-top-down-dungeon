using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public float pushRecoverySpeed = 0.2f;

    protected float immuneTime = 1.0f;
    protected float lastImmune;

    protected Vector3 pushDirection;

    protected abstract Color SetColor();

    protected abstract void Death();

    protected virtual void ReceiveDamage(Damage currentDamage)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            health -= currentDamage.hitDamage;
            pushDirection = (transform.position - currentDamage.origin)
                .normalized * currentDamage.pushForce;

            ShowText(currentDamage);
        }

        CheckHealthBeValid();
    }
    protected virtual void ShowText(Damage currentDamage)
    {
        GameManager.instance.ShowText("-" + currentDamage.hitDamage.ToString(), 25, SetColor(),
               transform.position, Vector3.zero, 0.5f);
    }
    private void CheckHealthBeValid()
    {
        if (health <= 0)
        {
            health = 0;
            Death();
        }
    }
}