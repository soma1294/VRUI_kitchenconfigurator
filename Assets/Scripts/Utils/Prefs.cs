using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefs {

    public float roomHeight;
    public float roomWidth;
    public float roomDepth;



    public void Load() {
        roomHeight = PlayerPrefs.GetFloat("roomHeight", 1f);
        roomWidth = PlayerPrefs.GetFloat("roomWidth", 1f);
        roomDepth = PlayerPrefs.GetFloat("roomDepth", 1f);
    }

    public void Save() {
        PlayerPrefs.SetFloat("roomHeight", roomHeight);
        PlayerPrefs.SetFloat("roomWidth", roomWidth);
        PlayerPrefs.SetFloat("roomDepth", roomDepth);
    }
}
