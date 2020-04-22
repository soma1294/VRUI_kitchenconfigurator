using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMeasurementBehaviour : MonoBehaviour {

    private Prefs prefs;

    public NumberCarouselBehaviour height;
    public NumberCarouselBehaviour width;
    public NumberCarouselBehaviour depth;
    public Transform roomModel;

    // Use this for initialization
    void Start() {
        prefs = new Prefs();
        prefs.Load();
        height.UpdateDigits(prefs.roomHeight);
        width.UpdateDigits(prefs.roomWidth);
        depth.UpdateDigits(prefs.roomDepth);
    }

    void Update() {
        float actualHeight = height.numberValue;
        float actualWidth = width.numberValue;
        float actualDepth = depth.numberValue;
        roomModel.localScale = new Vector3(actualDepth / 10, actualHeight / 10, actualWidth / 10);
    }

    private void OnDisable() {
        prefs.roomHeight = height.numberValue;
        prefs.roomWidth = width.numberValue;
        prefs.roomDepth = depth.numberValue;
        prefs.Save();
        Variables.roomMeasurements = new Vector3(depth.numberValue, height.numberValue, width.numberValue);
    }

    public void startKitchenBuilder() {        
        SceneManager.LoadScene("KitchenBuilder");
    }
}
