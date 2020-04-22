using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class WindowListBehaviour : MonoBehaviour {

    public CompassDirections wallIndex = CompassDirections.NORTH;

    public List<Window> windows = new List<Window>();

    public GameObject windowConfigPrefab;

    private ToggleGroup toggleGroup;
	// Use this for initialization
	void Start () {
        toggleGroup = GetComponent<ToggleGroup>();
        // Activate when windowConfigs are persisted between starts
        switch (wallIndex) {
            case CompassDirections.NORTH:
                windows = Variables.northWindows;
                break;
            case CompassDirections.EAST:
                windows = Variables.eastWindows;
                break;
            case CompassDirections.SOUTH:
                windows = Variables.southWindows;
                break;
            case CompassDirections.WEST:
                windows = Variables.westWindows;
                break;
        }
        GameObject currentWindow;
        WindowConfigBehaviour currentWindowConfig;
        foreach (Window window in windows) {
            currentWindow = Instantiate(windowConfigPrefab, transform);
            currentWindowConfig = currentWindow?.GetComponent<WindowConfigBehaviour>();
            currentWindowConfig.window = window;
            currentWindowConfig.toggleGroup = toggleGroup;
        }
    }

    public void AddWindow() {
        GameObject currentWindow = Instantiate(windowConfigPrefab, transform);
        WindowConfigBehaviour currentWindowConfig = currentWindow?.GetComponent<WindowConfigBehaviour>();
        currentWindowConfig.window = new Window(1000, 1000, 1000, 600);
        currentWindowConfig.toggleGroup = toggleGroup;
    }

    private void OnDisable() {
        WindowConfigBehaviour[] windowConfigs = gameObject.GetComponentsInChildren<WindowConfigBehaviour>();
        windows.Clear();
        for(int i = 0; i < windowConfigs.Length; i++) {
            windows.Add(windowConfigs[i].window);
        }

        switch (wallIndex) {
            case CompassDirections.NORTH:
                Variables.northWindows = windows;
                break;
            case CompassDirections.EAST:
                Variables.eastWindows = windows;
                break;
            case CompassDirections.SOUTH:
                Variables.southWindows = windows;
                break;
            case CompassDirections.WEST:
                Variables.westWindows = windows;
                break;
        }
            

    }
}
