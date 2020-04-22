using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowConfigBehaviour : MonoBehaviour {

    public Window window;

    public NumberCarouselBehaviour rightOffset;
    public NumberCarouselBehaviour bottomOffset;
    public NumberCarouselBehaviour width;
    public NumberCarouselBehaviour height;

    public ToggleGroup toggleGroup;

    private void Start() {
        rightOffset.UpdateDigits(window.windowRightOffsetinMM / 1000f);
        bottomOffset.UpdateDigits(window.windowBottomOffsetinMM / 1000f);
        width.UpdateDigits(window.windowWidthInMM / 1000f);
        height.UpdateDigits(window.windowHeightInMM / 1000f);
        if (toggleGroup) {
            rightOffset.SetToggleGroup(toggleGroup);
            bottomOffset.SetToggleGroup(toggleGroup);
            width.SetToggleGroup(toggleGroup);
            height.SetToggleGroup(toggleGroup);
        }
    }


    // Update is called once per frame
    void Update () {
        window.windowRightOffsetinMM = (int)(rightOffset.numberValue * 1000);
        window.windowBottomOffsetinMM = (int)(bottomOffset.numberValue * 1000);
        window.windowHeightInMM = (int)(height.numberValue * 1000);
        window.windowWidthInMM = (int)(width.numberValue * 1000);
    }
}
