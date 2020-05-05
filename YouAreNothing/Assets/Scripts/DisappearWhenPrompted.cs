using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearWhenPrompted : MonoBehaviour
{
    public Image targetImage;
    public Image[] thisImage;
    public Text[] thisText;
    public int dontEnabledImagesAfter; //If zero, enables all images. Otherwise, doesn't enable images after the set int.
    int deia; // temp int

    void Awake()
    {
        if (dontEnabledImagesAfter != 0)
        {
            deia = dontEnabledImagesAfter;
        }
        else
        {
            deia = thisImage.Length;
        }
        thisImage = GetComponentsInChildren<Image>();
        thisText = GetComponentsInChildren<Text>();
        StartCoroutine(LinkActivationToTargetObject());
    }

    IEnumerator LinkActivationToTargetObject()
    {
        yield return new WaitUntil(() => targetImage.enabled == false);
        for (int i = 0; i < thisImage.Length; i++)
        {
            thisImage[i].enabled = false;
        }
        for (int i = 0; i < thisText.Length; i++)
        {
            thisText[i].enabled = false;
        }
        yield return new WaitUntil(() => targetImage.enabled == true);
        for (int i = 0; i < dontEnabledImagesAfter; i++)
        {
            thisImage[i].enabled = true;
        }
        for (int i = 0; i < thisText.Length; i++)
        {
            thisText[i].enabled = true;
        }
        StartCoroutine(LinkActivationToTargetObject());
    }
}
