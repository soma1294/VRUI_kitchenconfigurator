﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Blocks the transform of it's child objects.
/// </summary>
[RequireComponent(typeof(VRUIPanelBehaviour))]
public class VRUIScrollPanelBehaviour : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    [Tooltip("The maximum amount of displayed elements. This determines how the available space of the panel gets split. " +
             "Can not be set higher than the amount of child objects of this object.")]
    private int maxDisplayedElements = 1;
    [SerializeField]
    [HideInInspector]
    [Tooltip("Defines how the child elements get arranged.")]
    private Layout layout = Layout.LeftToRight;
    //[Header("Scroll Settings")]
    [SerializeField]
    [HideInInspector]
    [Tooltip("Defines how many elements we scroll if StepForward() or StepBackward() gets called. Can't be higher than maxDisplayElements.")]
    private int scrollStepSize = 1;
    [SerializeField]
    [HideInInspector]
    [Tooltip("If this is true calling StepForward() at the end of the list will bring the user back to the start " +
             "and calling StepBackward() at the start of the list will bring the user to the end of the list.")]
    private bool canOverStep = false;
    [SerializeField]
    [HideInInspector]
    [Tooltip("The position of the first displayed element of the children (top child in the hierarchy that is displayed).")]
    private int positionInList;
    [SerializeField]
    [HideInInspector]
    [Tooltip("The local z-position of the child objects.")]
    private float zPositionOfChildren = 0f;
    [SerializeField]
    [HideInInspector]
    public int lastChildCount;
    [SerializeField]
    [HideInInspector]
    public VRUIPanelBehaviour panel;

    public enum Layout
    {
        LeftToRight,
        TopToBottom
    }

    private void OnEnable()
    {
        if (transform.childCount > 0)
        {
            DisplayCorrectChildElements();
            ArrangeElements();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        positionInList = 0;
    }

    private void Update()
    {
        //These values cant be smaller than 1
        if (MaxDisplayedElements < 1)
            MaxDisplayedElements = 1;
        if (scrollStepSize < 1)
            scrollStepSize = 1;
        //If a child was added reposition elements.
        //Debug.Log("if: lastChildCount = " + lastChildCount + ";childCount = " + transform.childCount);
        if (Application.isPlaying)
        {
            if (transform.childCount > 0 && lastChildCount != transform.childCount)
            {
                DisplayCorrectChildElements();
                ArrangeElements();
            }
        }/*
        else if(transform.childCount > 0 && lastChildCount != transform.childCount)
        {
            DisplayCorrectChildElements();
            ArrangeElements();
        }*/
        lastChildCount = transform.childCount;
    }

    public void ArrangeElements()
    {
        float sliceSize;
        Vector3 firstElementPosition;
        if (layout == Layout.LeftToRight)
        {
            sliceSize = SliceSizeVertically();
            //The position of the first element is half the size of the panel plus half the size of one slice from the center of the panel.
            //firstElementPosition = transform.position - new Vector3((PanelSizeX/2) - (sliceSize / 2), 0f, zPositionOfChildren);
            firstElementPosition = transform.position - (transform.right * ((panel.PanelSizeX / 2) - (sliceSize / 2))) - (transform.forward * zPositionOfChildren);
            //Debug.Log("firstElementPosition=" + firstElementPosition);
            int i = 0;
            foreach (Transform child in transform)
            {
                child.position = GetPositionOfChildElement(i, firstElementPosition, sliceSize);
                if(child.GetComponent<VRUIPositioner>())
                    child.GetComponent<VRUIPositioner>().RelativePosition = child.localPosition;
                i++;
            }
        }
        else if (layout == Layout.TopToBottom)
        {
            sliceSize = SliceSizeHorizontally();
            //The position of the first element is half the size of the panel plus half the size of one slice from the center of the panel.
            //firstElementPosition = transform.position + new Vector3(0f, (PanelSizeY / 2) - (sliceSize / 2), zPositionOfChildren);
            firstElementPosition = transform.position + (transform.up * ((panel.PanelSizeY / 2) - (sliceSize / 2))) - (transform.forward * zPositionOfChildren);
            int i = 0;
            foreach (Transform child in transform)
            {
                child.position = GetPositionOfChildElement(i, firstElementPosition, sliceSize);
                if (child.GetComponent<VRUIPositioner>())
                    child.GetComponent<VRUIPositioner>().RelativePosition = child.localPosition;
                i++;
            }
        }
        else
        {
            Debug.LogError("No layout chosen for VRUIScrollPanel! Layout was set to \"LeftToRight\"");
            layout = Layout.LeftToRight;
        }
    }

    private float SliceSizeVertically()
    {
        if (transform.childCount >= MaxDisplayedElements)
        {
            return panel.PanelSizeX / MaxDisplayedElements;
        }
        else
        {
            return panel.PanelSizeX / transform.childCount;
        }
    }

    private float SliceSizeHorizontally()
    {
        if (transform.childCount >= MaxDisplayedElements)
        {
            return panel.PanelSizeY / MaxDisplayedElements;
        }
        else
        {
            return panel.PanelSizeY / transform.childCount;
        }
    }

    private Vector3 GetPositionOfChildElement(int child, Vector3 firstElementPosition, float sliceSize)
    {
        if (layout == Layout.LeftToRight)
            return firstElementPosition + transform.right * (child - positionInList) * sliceSize;
        //return firstElementPosition + new Vector3((child - positionInList) * sliceSize, 0f, 0f);
        if (layout == Layout.TopToBottom)
            return firstElementPosition + -transform.up * (child - positionInList) * sliceSize;
        //return firstElementPosition - new Vector3(0f, (child - positionInList) * sliceSize, 0f);
        Debug.LogError("GetPositionOfChildElement: No Layout chosen! Returned Vector zero...");
        return Vector3.zero;
    }

    public void DisplayCorrectChildElements()
    {
        /*foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }*/
        if (MaxDisplayedElements <= transform.childCount)
        {
            for (int i = positionInList; i < (positionInList + MaxDisplayedElements); i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                    transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Max Displayed Elements is greater than the amount of children of this VRUIScrollPanelBehaviour: " + name + "\nAll children will be displayed.");
            for (int i = 0; i < transform.childCount; i++)
            {
                if (!transform.GetChild(i).gameObject.activeSelf)
                    transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        if (positionInList > 0)
        {
            for (int i = 0; i < transform.childCount && i < positionInList; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (MaxDisplayedElements < transform.childCount)
        {
            for (int i = positionInList + MaxDisplayedElements; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void StepForward()
    {
        int elementListSize = transform.childCount;
        if (positionInList + MaxDisplayedElements >= elementListSize && canOverStep)
        {
            StepToStart();
        }
        else if (positionInList + scrollStepSize >= elementListSize - MaxDisplayedElements)
        {
            positionInList = elementListSize - MaxDisplayedElements;
            StepToEnd();
        }
        else if (MaxDisplayedElements < transform.childCount)
        {
            positionInList += scrollStepSize;
        }
        if (transform.childCount > 0)
        {
            DisplayCorrectChildElements();
            ArrangeElements();
        }
        //Debug.Log("childCount=" + transform.childCount + "; positionInList=" + positionInList);
    }

    public void StepBackward()
    {
        if (positionInList == 0 && canOverStep)
        {
            StepToEnd();
        }
        else if (positionInList - scrollStepSize <= 0)
        {
            positionInList = 0;
            StepToStart();
        }
        else
        {
            positionInList -= scrollStepSize;
        }
        if (transform.childCount > 0)
        {
            DisplayCorrectChildElements();
            ArrangeElements();
        }
    }

    public void StepToStart()
    {
        //Debug.Log("StepToStart");
        positionInList = 0;
        DisplayCorrectChildElements();
        ArrangeElements();
    }

    public void StepToEnd()
    {
        //Debug.Log("StepToEnd");
        positionInList = transform.childCount - MaxDisplayedElements;
        DisplayCorrectChildElements();
        ArrangeElements();
    }

    public bool EndReached()
    {
        return positionInList + scrollStepSize >= transform.childCount - MaxDisplayedElements;
    }

    public bool StartReached()
    {
        return positionInList == 0;
    }

    public int MaxDisplayedElements
    {
        get { return maxDisplayedElements; }
        set { maxDisplayedElements = value; }
    }
}
