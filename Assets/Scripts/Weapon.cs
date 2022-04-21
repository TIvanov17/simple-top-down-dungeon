using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Хар. на оръжието, за да може да бъде променяно
    public int weaponLevel = 0;
    private SpriteRenderer weaponSprite;

    // Завъртане на оръжието (анимация)
    private Animator animator;
    private float coolDown = 0.5f;
    private float lastSwing;

    // Полета за това колко damage и колко
    // ще отблъсне enemy-то конкретното оръжие
    public int[] hitDamageCollection = {1, 2, 3};
    public float[] pushForceCollection = {2.0f, 2.5f, 3.5f };

    protected override void Start()
    {
        base.Start();
        weaponSprite = GetComponent<SpriteRenderer>(); // инициализира сегашното оръжие
        animator = GetComponent<Animator>();
    }
    
    protected override void Update()
    {
        base.Update();
        if (ShouldSwing())
        {
            lastSwing = Time.time;
            Swing();
        }
    }

    protected override void OnCollide(Collider2D currentCollider)
    {
        if(ShouldAttack(currentCollider))
        {
            Damage weaponDamage = new Damage
                                  ( transform.position, 
                                    hitDamageCollection[weaponLevel], 
                                    pushForceCollection[weaponLevel]
                                   );

            currentCollider.SendMessage("ReceiveDamage", weaponDamage);
        }
    }

    private bool ShouldAttack(Collider2D currentCollider)
    {
        return currentCollider.tag == "Fighter" && currentCollider.name != "Player";
    }

    private bool ShouldSwing()
    {
        return Input.GetKeyDown(KeyCode.Space) && Time.time - lastSwing > coolDown;
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        if (IsWeaponLevelInvalid()){
            return;
        }
        weaponSprite.sprite = GameManager.instance.weaponSprites[++weaponLevel];
    }

    private bool IsWeaponLevelInvalid()
    {
        return (weaponLevel >= GameManager.instance.weaponSprites.Count - 1);
    }

}
