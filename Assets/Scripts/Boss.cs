using UnityEngine;

public class Boss : Enemy
{
    public float[] fireballSpeedsColl = { 2.5f, -2.5f };
    public float distance = 0.25f;

    public Transform[] fireballTransormColl;

    private void Update()
    {
        for(int i = 0; i < fireballSpeedsColl.Length; i++)
        {
            float argumentValue = Time.time * fireballSpeedsColl[i];

            float xValue = -Mathf.Cos(argumentValue) * distance;
            float yValue = Mathf.Sin(argumentValue) * distance;

            Vector3 addedPotionVector = new Vector3(xValue, yValue, 0);

            fireballTransormColl[i].position = transform.position + addedPotionVector;
        }
       
    }
}
