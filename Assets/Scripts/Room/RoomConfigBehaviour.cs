using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomConfigBehaviour : MonoBehaviour {

    private Prefs prefs;

    public Text height;
    public Text width;
    public Text depth;
    public Transform roomModel;

    // Use this for initialization
    void Start () {
        prefs = new Prefs();
        prefs.Load();
        setRoomModelScale();
        height.text = prefs.roomHeight.ToString("0.00") + "m";
        width.text = prefs.roomWidth.ToString("0.00") + "m";
        depth.text = prefs.roomDepth.ToString("0.00") + "m";
    }

    public void addToHeight(float howMuch) {
        prefs.roomHeight += howMuch;
        prefs.Save();
        height.text = prefs.roomHeight.ToString("0.00") + "m";
        setRoomModelScale();
    }

    public void subtractFromHeight(float howMuch) {
        if (howMuch < prefs.roomHeight) {
            prefs.roomHeight -= howMuch;
            prefs.Save();
            height.text = prefs.roomHeight.ToString("0.00") + "m";
            setRoomModelScale();
        }
    }

    public void addToWidth(float howMuch) {
        prefs.roomWidth += howMuch;
        prefs.Save();
        width.text = prefs.roomWidth.ToString("0.00") + "m";
        setRoomModelScale();
    }

    public void subtractFromWidth(float howMuch) {
        if(howMuch < prefs.roomWidth) {
            prefs.roomWidth -= howMuch;
            prefs.Save();
            width.text = prefs.roomWidth.ToString("0.00") + "m";
            setRoomModelScale();
        }
    }

    public void addToDepth(float howMuch) {
        prefs.roomDepth += howMuch;
        prefs.Save();
        depth.text = prefs.roomDepth.ToString("0.00") + "m";
        setRoomModelScale();
    }

    public void subtractFromDepth(float howMuch) {
        if (howMuch < prefs.roomDepth) {
            prefs.roomDepth -= howMuch;
            prefs.Save();
            depth.text = prefs.roomDepth.ToString("0.00") + "m";
            setRoomModelScale();
        }
    }

    public void startKitchenBuilder() {
        prefs.Save();
        SceneManager.LoadScene("KitchenBuilder");
    }

    public void startButtonDemo()
    {
        prefs.Save();
        SceneManager.LoadScene("ButtonDemo");
    }

    private void setRoomModelScale() {
        roomModel.localScale = new Vector3(prefs.roomDepth / 10, prefs.roomHeight / 10, prefs.roomWidth / 10);
    }
}
