using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericAnimationEvent : MonoBehaviour
{
    //This script is intended to cover a lot of generic animation events that pop up, especially with UI. If it becomes too cumbersome I'll scrap it.

    public Image[] allChildImages; //All child images. Note that this includes images on the parent object.
    public Text[] allChildText; //All child texts. Note that this includes text on the parent object.
    public bool ignoreZeroImage; //This should be true if you want to ignore the first image with appear and disappear image calls.
    public bool childObjectsStartInactive; //This should be true if child objects should start inactive - they need to be active in the inspector on awake.

    void Awake()
    {
        allChildImages = GetComponentsInChildren<Image>();
        allChildText = GetComponentsInChildren<Text>();
        if (childObjectsStartInactive == true)
        {
            ChildImagesDisappear();
            ChildTextDisappear();
        }
    }

    public void ChildObjectsAppear ()
    {
        ChildImagesAppear();
        ChildTextAppear();
    }

    public void ChildObjectsDisappear()
    {
        ChildImagesDisappear();
        ChildTextDisappear();
    }

    public void ChildImagesAppear()
    {
        for (int i = 0; i < allChildImages.Length; i++)
        {
            if ((ignoreZeroImage == true & i == 0) == false)
            {
                allChildImages[i].enabled = true;
            }
        }
    }

    public void ChildImagesDisappear()
    {
        for (int i = 0; i < allChildImages.Length; i++)
        {
            if ((ignoreZeroImage == true & i == 0) == false)
            {
                allChildImages[i].enabled = false;
            }
        }
    }

    public void ZeroImageDisappear ()
    {
        allChildImages[0].enabled = false;
    }

    public void ChildTextAppear()
    {
        for (int i = 0; i < allChildText.Length; i++)
        {
            allChildText[i].enabled = true;
        }
    }

    public void ChildTextDisappear()
    {
        for (int i = 0; i < allChildText.Length; i++)
        {
            allChildText[i].enabled = false;
        }
    }
}
