using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml; 
using System.Xml.Serialization; 

public static class Extensions
{
    //Taken from ButtonController for use in Hub
    public static IEnumerator FadeIn(this Graphic gUIText, float fadeTimer)
    {
        float speed = 1.0f / fadeTimer;
        for (float t = 0.0f; t < 1.0; t += Time.deltaTime*speed)
        {
            float a = Mathf.Lerp(0.0f, 1.0f, t);
            Color faded = gUIText.color;
            faded.a = a;
            gUIText.color = faded;
            yield return 0;
        }
    }

    //Taken from ButtonController for use in Hub
    public static IEnumerator FadeOut(this Graphic gUIText, float fadeTimer)
    {
        float speed = 1.0f / fadeTimer;
        for (float t = 0.0f; t < 1.0; t += Time.deltaTime * speed)
        {
            float a = Mathf.Lerp(1.0f, 0.0f, t);
            Color faded = gUIText.color;
            faded.a = a;
            gUIText.color = faded;
            yield return 0;
        }
    }
}
