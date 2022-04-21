using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 5;
    protected override void OnCollect()
    {
        if (!isCollected)
        {
            base.OnCollect(); // изполва импл. от родителя, че предмета е collect-над
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += goldAmount;
            GameManager.instance.ShowText("+ " + goldAmount + " gold!", 25, Color.yellow,
                                transform.position, Vector3.up * 50, 3.0f);
        }
    }
}
