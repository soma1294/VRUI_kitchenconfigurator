using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurnitureWidthBehaviour : MonoBehaviour
{
    public ObjectPreviewBehaviour objectPreview;
    public VRUISliderBehaviour sliderBehaviour;

    private int maxIndex;
    private int currentIndex;
    private VRUITextcontainerBehaviour textcontainerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 2;
        maxIndex = Variables.widthsInMM.Length - 1;
        if (sliderBehaviour)
        {
            sliderBehaviour.MaxValue = maxIndex;
            sliderBehaviour.MinValue = 0;
        }
        textcontainerBehaviour = GetComponent<VRUITextcontainerBehaviour>();
        textcontainerBehaviour.ChangeTextTo(Variables.widthsInMM[currentIndex] + " mm");
    }

    public void IncrementIndex()
    {
        if (currentIndex + 1 > maxIndex)
        {
            currentIndex = maxIndex;
        }
        else
        {
            currentIndex++;
        }
        objectPreview.SetWidthIndex(currentIndex);
        textcontainerBehaviour.ChangeTextTo(Variables.widthsInMM[currentIndex] + " mm");
    }

    public void DecrementIndex()
    {
        if (currentIndex - 1 < 0)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex--;
        }
        objectPreview.SetWidthIndex(currentIndex);
        textcontainerBehaviour.ChangeTextTo(Variables.widthsInMM[currentIndex] + " mm");
    }

    public void SetIndex(float index)
    {
        if (index < 0)
        {
            currentIndex = 0;
        } else if (index > maxIndex)
        {
            currentIndex = maxIndex;
        } else
        {
            currentIndex = (int)index;
        }
        objectPreview.SetWidthIndex(currentIndex);
        textcontainerBehaviour.ChangeTextTo(Variables.widthsInMM[currentIndex] + " mm");
    }
}
