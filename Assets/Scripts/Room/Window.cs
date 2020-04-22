using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Window {
    public int windowRightOffsetinMM;
    public int windowBottomOffsetinMM;

    public int windowHeightInMM;
    public int windowWidthInMM;

    public Window(int rightOffset, int bottomOffset, int height, int width) {
        windowRightOffsetinMM = rightOffset;
        windowBottomOffsetinMM = bottomOffset;
        windowHeightInMM = height;
        windowWidthInMM = width;
    }
}
