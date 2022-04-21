using UnityEngine;

public class Crate : Fighter
{
    protected override void ShowText(Damage currentDamage)
    {
        
    }
    protected override Color SetColor()
    {
        return Color.red;
    }
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
