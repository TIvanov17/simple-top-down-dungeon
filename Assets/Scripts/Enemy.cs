using UnityEngine;

public class Enemy : Mover
{
    private const int INITIAL_CAPPACITY = 10;

    public int xpValue = 1;

    public float triggerLenght;
    public float chaseLenght;
    private bool isChasing;
    private bool isCollidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hitsCollection = new Collider2D[INITIAL_CAPPACITY];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(IsPlayerCloseToEnemy())
        {
            EnemyChasingZone();
            CheckCollider();
            return;
        }

        UpdateMotor(startPosition - transform.position);
        isChasing = false;
        CheckCollider();
    }

    private void EnemyChasingZone()
    {
        if (IsPlayerTriggerEnemy())
        {
            isChasing = true;
        }

        if (isChasing && !isCollidingWithPlayer)
        {
            UpdateMotor((playerTransform.position - transform.position).normalized);
            return;
        }

        if (!isChasing)
        {
            UpdateMotor(startPosition - transform.position);
            return;
        }
    }

    private void CheckCollider()
    {
        isCollidingWithPlayer = false;
        hitBox.OverlapCollider(filter, hitsCollection);
        TraverseAllHitColliders();
    }

    protected override void Death()
    {
        Destroy(gameObject);

        GameManager.instance.GrandXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta,
            transform.position, Vector3.up * 40, 1.0f);
    }
    protected override Color SetColor()
    {
        return Color.blue;
    }

    private bool IsPlayerCloseToEnemy()
    {
        return Vector3.Distance(playerTransform.position, startPosition) < chaseLenght;
    }

    private bool IsPlayerTriggerEnemy()
    {
        return Vector3.Distance(playerTransform.position, startPosition) < triggerLenght;
    }

    private void TraverseAllHitColliders()
    {
        for (int i = 0; i < hitsCollection.Length; i++)
        {
            if (hitsCollection[i] == null)
            {
                continue;
            }

            if(hitsCollection[i].tag == "Fighter" && hitsCollection[i].name == "Player")
            {
                isCollidingWithPlayer = true;
            }
            
            hitsCollection[i] = null;
        }
    }
}
