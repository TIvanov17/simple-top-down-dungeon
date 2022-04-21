using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject gameObject;
    public Text textContent;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;
        gameObject.SetActive(active);
    }
    public void Hide()
    {
        active = false;
        gameObject.SetActive(false);
    }

    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        if(HaveToHide())
        {
            Hide();
        }

        gameObject.transform.position += motion * Time.deltaTime;
    }

    private bool HaveToHide()
    {
        return Time.time - lastShown > duration;
    }
}
