using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoImageWhenNone : MonoBehaviour
{
    public Image thisImage;

    void Awake()
    {
        thisImage = GetComponent<Image>();
        StartCoroutine(IfNoImageTranslucent());
    }

    IEnumerator IfNoImageTranslucent()
    {
        yield return new WaitUntil(() => thisImage.sprite == null);
        thisImage.color = new Color(255, 255, 225, 0);
        yield return new WaitUntil(() => thisImage.sprite != null);
        thisImage.color = new Color(255, 255, 225, 100);
        StartCoroutine(IfNoImageTranslucent());
    }
}
