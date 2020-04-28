using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIWindowConfigBehaviour : MonoBehaviour
{
    public Window window;

    public VRUITextcontainerBehaviour title;
    public FloatValueContainer rightOffset;
    public FloatValueContainer bottomOffset;
    public FloatValueContainer width;
    public FloatValueContainer height;

    // Start is called before the first frame update
    void Start()
    {
        rightOffset.value = window.windowRightOffsetinMM;
        bottomOffset.value = window.windowBottomOffsetinMM;
        width.value = window.windowWidthInMM;
        height.value = window.windowHeightInMM;
    }

    public void UpdateValues(float amount)
    {
        window.windowRightOffsetinMM = (int)rightOffset.value;
        window.windowBottomOffsetinMM = (int)bottomOffset.value;
        window.windowHeightInMM = (int)height.value;
        window.windowWidthInMM = (int)width.value;
    }
}
