using UnityEngine;

public class Collectable : Collidable
{
    protected bool isCollected;

    protected override void OnCollide(Collider2D currentCollider)
    {
        if(currentCollider.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        SetIsCollected(true);
    }

    private void SetIsCollected(bool isCollected)
    {
        this.isCollected = isCollected;
    }
}