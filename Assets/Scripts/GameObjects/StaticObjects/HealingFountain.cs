using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;
    private float healCoolDown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D currentCollider)
    {
        if(currentCollider.name != "Player")
        {
            return;
        }

        float currentTime = Time.time;

        if(currentTime - lastHeal > healCoolDown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}