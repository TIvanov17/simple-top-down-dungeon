using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);


        // rotate Player by x, if he is moving to the right or to the left
        // right -> scale(1,1,1)
        // left -> scale(-1,1,1)
        if (moveDelta.x > 0)
            transform.localScale = originalSize;

        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        // add push if needed
        moveDelta += pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = SetHit(new Vector2(0, moveDelta.y), moveDelta.y * Time.deltaTime);
        CheckIsNotWall(0, moveDelta.y * Time.deltaTime, 0);

        hit = SetHit(new Vector2(moveDelta.x, 0), moveDelta.x * Time.deltaTime);
        CheckIsNotWall(moveDelta.x * Time.deltaTime, 0, 0);
    }

    private RaycastHit2D SetHit(Vector2 vector2, float absArg)
    {
        return Physics2D.BoxCast
                (transform.position, boxCollider.size, 0,
                vector2, Mathf.Abs(absArg),
                LayerMask.GetMask("Actor", "Blocking"));
    }

    private void CheckIsNotWall(float x, float y, float z)
    {
        if (DoNotHitWall())
            transform.Translate(x, y, z);
    }

    private bool DoNotHitWall()
    {
        return hit.collider == null;
    }
}