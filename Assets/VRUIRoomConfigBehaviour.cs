using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRUIRoomConfigBehaviour : MonoBehaviour {

    private Prefs prefs;

    public FloatValueContainer height;
    public FloatValueContainer width;
    public FloatValueContainer depth;
    public Transform roomModel;

    // Use this for initialization
    void Awake () {
        prefs = new Prefs();
        prefs.Load();
        setRoomModelScale();
        height.value = prefs.roomHeight;
        width.value = prefs.roomWidth;
        depth.value = prefs.roomDepth;
    }

    public void SetHeight(float howMuch) {
        prefs.roomHeight = howMuch;
        prefs.Save();
        height.value = prefs.roomHeight;
        setRoomModelScale();
    }

    public void SetWidth(float howMuch) {
        prefs.roomWidth = howMuch;
        prefs.Save();
        width.value = prefs.roomWidth;
        setRoomModelScale();
    }

    public void SetDepth(float howMuch) {
        prefs.roomDepth = howMuch;
        prefs.Save();
        depth.value = prefs.roomDepth;
        setRoomModelScale();
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
        roomModel.localScale = new Vector3(prefs.roomDepth / 5, prefs.roomHeight / 5, prefs.roomWidth / 5);
    }
}
