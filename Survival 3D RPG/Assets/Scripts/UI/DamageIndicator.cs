using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image bloodImage;
    public float flashSpeed;

    private Coroutine fadeAway;

    public void Flash()
    {
        if (fadeAway != null)
        {
            StopCoroutine(fadeAway);
        }

        //alpha value of image convert to 1
        bloodImage.color = Color.white;

        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float alphaImage = 1.0f;

        while (alphaImage > 0)
        {
            alphaImage -= (1.0f / flashSpeed * Time.deltaTime);
            bloodImage.color = new Color(1.0f, 1.0f, 1.0f, alphaImage);

            yield return null;
        }
    }
}
