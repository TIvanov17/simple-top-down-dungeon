using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
        { 
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void ChangeSprite(int index)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[index];
    }

    public void OnLevelUp()
    {
        maxHealth++;    
    }

    public void Heal(int healingAmount)
    {
        if (health == maxHealth)
        {
            return;
        }

        health += healingAmount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        
        GameManager.instance.ShowText
            ("+" + healingAmount.ToString(), 25, Color.green,
             transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.ChangeHealthBar();
    }

    public void Respawn()
    {
        Heal(maxHealth);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

    protected override void ReceiveDamage(Damage currentDamage)
    {
        if (!isAlive) {
            return;
        }
        base.ReceiveDamage(currentDamage);
        GameManager.instance.ChangeHealthBar();
    }

    protected override Color SetColor()
    {
        return Color.red;
    }

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }
}