using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNamesCollection; 
    protected override void OnCollide(Collider2D currentCollider)
    {
        if(currentCollider.name == "Player")
        {
            GameManager.instance.SaveState();
            string currentSceneName = sceneNamesCollection[Random.Range(0, sceneNamesCollection.Length)];
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
