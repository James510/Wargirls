using UnityEngine;
using System.Collections;

public class FadeDestroy : MonoBehaviour
{
    Color startColor;
    Color currentColor;
    Color endColor;
    bool shouldFade = false;
    float startTime;
    float endTime;
    public float seconds = 5.0f;
    float t;

    // Use this for initialization
    void Start()
    {
        Debug.Log("FadeTest used by " + gameObject.name);
        startColor = gameObject.GetComponent<Renderer>().material.color;
        endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        int childCount = transform.childCount;
        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            if (child.gameObject.GetComponent<Renderer>())
            {
                child.gameObject.AddComponent<FadeDestroy>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            shouldFade = true;
            startTime = Time.time;
            endTime = startTime + seconds;
        }

        Fade();
    }
    void Fade()
    {
        if (shouldFade)
        {
            t = Time.time / endTime;
            currentColor = Color.Lerp(startColor, endColor, t);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", currentColor);

            if (currentColor == endColor)
            {
                shouldFade = false;
                startTime = 0.0f;
                endTime = 0.0f;
                t = 0.0f;
            }
        }
    }
}