using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutBehaviour : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image.CrossFadeAlpha(1.0f, 2f, false);
    }
}
