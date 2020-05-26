using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIVibrationSliderOVR : VRUIVibration
{
    [SerializeField]
    [Tooltip("If this is true the controller will vibrate.")]
    private bool vibrate = true;

    public override void Vibrate()
    {
        if (vibrate)
        {
            VRUIGestureController gestureController = GetComponent<VRUISliderBehaviour>().LastRegisteredGestureController;
            if (!gestureController)
                return;
            if (gestureController.Hand == VRUIGestureController.HandSide.left)
            {
                StartCoroutine(OVRVibration(VibrationFrequency, VibrationAmplitude, VibrationDuration, OVRInput.Controller.LTouch));
            }
            else
            {
                StartCoroutine(OVRVibration(VibrationFrequency, VibrationAmplitude, VibrationDuration, OVRInput.Controller.RTouch));
            }
        }
    }

    private IEnumerator OVRVibration(float freq, float amp, float duration, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(freq, amp, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0.0f, 0.0f, controller);
    }
}
