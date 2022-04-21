
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> collectionOfFloatingText = new List<FloatingText>();
 
    public void Start()
    {
        //DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        foreach (FloatingText floatingText in collectionOfFloatingText) { 
            floatingText.UpdateFloatingText();
        }
    }

    public void Show(string message, int fontSize, 
                     Color color, Vector3 position,
                     Vector3 motion, float duration)
    {

        FloatingText floatingText = GetFloatingText();

        floatingText.textContent.text = message;
        floatingText.textContent.fontSize = fontSize;
        floatingText.textContent.color = color;
        floatingText.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = collectionOfFloatingText.Find(t => !t.active);
        
        return CheckHasNotActiveFloatingText(floatingText);
    }

    private FloatingText CheckHasNotActiveFloatingText(FloatingText floatingText)
    {
        if (floatingText == null)
        {
            return InitFloatingText();
        }

        return floatingText;
    }

    private FloatingText InitFloatingText()
    {
        FloatingText floatingText = new FloatingText();
        floatingText.gameObject = Instantiate(textPrefab);
        floatingText.gameObject.transform.SetParent(textContainer.transform);
        floatingText.textContent = floatingText.gameObject.GetComponent<Text>();

        collectionOfFloatingText.Add(floatingText);
        
        return floatingText;
    }
}
