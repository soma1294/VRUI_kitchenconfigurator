using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFurnitureWidthBehaviour : MonoBehaviour
{
    public ObjectPreviewBehaviour objectPreview;

    private int maxIndex;
    private int currentIndex;
    private VRUITextcontainerBehaviour textcontainerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 2;
        maxIndex = Variables.widthsInMM.Length - 1;
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
}
