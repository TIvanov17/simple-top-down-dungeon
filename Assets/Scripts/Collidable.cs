using UnityEngine;

public class Collidable : MonoBehaviour
{
    private const int INITIAL_CAPACITY = 10;

    public ContactFilter2D filter;
    private BoxCollider2D currentBoxCollider;
    private Collider2D[] hitsCollection;

    public Collidable()
    {
        hitsCollection = new Collider2D[INITIAL_CAPACITY];
    }

    protected virtual void Start()
    {
        currentBoxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        currentBoxCollider.OverlapCollider(filter, hitsCollection);
        
        TraverseAllHitColliders();
    }

    private void TraverseAllHitColliders()
    {
        for (int i = 0; i < hitsCollection.Length; i++)
        {
            if (hitsCollection[i] == null)
            {
                continue;
            }

            OnCollide(hitsCollection[i]);
            hitsCollection[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D currentCollider)
    {
        Debug.Log(currentCollider.name);
    }

}
