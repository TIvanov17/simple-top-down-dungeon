using UnityEngine;

public class NpcText : Collidable
{
    public string message;

    private float cooldown = 4.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }

    protected override void OnCollide(Collider2D currentCollider)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            Vector3 messagePosition = transform.position + new Vector3(0,0.16f,0);
            
            GameManager.instance.ShowText(message, 10, Color.white,
                                          messagePosition, Vector3.zero, 
                                          cooldown
                                          );
        }
    }
}
