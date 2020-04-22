using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Valve.VR;
//using Valve.VR.InteractionSystem;

//[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Toggle))]
public class DigitCarouselBehaviour : MonoBehaviour
{

    public int value = 0;

    public Text text;

    private bool selected = false;
    private Vector2 startTouch = Vector2.zero;
    private int startValue;
    private Toggle toggle;

    private bool axisIsInUse = false;
    // Use this for initialization
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = value.ToString();

        if (toggle.isOn)
        {
            //Vector2 delta = Vector2.zero;
            float yAxis = Input.GetAxis("Oculus_GearVR_LThumbstickY");
            if (yAxis != 0)
            {
                if (!axisIsInUse)
                {
                    axisIsInUse = true;
                    if (yAxis > 0)
                    {
                        value++;
                    } else if (yAxis < 0)
                    {
                        value--;
                    }
                    /*
                    delta = startTouch - new Vector2(Input.GetAxis("Oculus_GearVR_LThumbstickX"), Input.GetAxis("Oculus_GearVR_LThumbstickY"));
                    value = startValue - Mathf.RoundToInt(delta.y / 0.2f);
                    */
                    value %= 10;
                    if (value < 0)
                    {
                        value += 10;
                    }
                    
                }   
            } else
            {
                axisIsInUse = false;
            }
        }
    }
}
