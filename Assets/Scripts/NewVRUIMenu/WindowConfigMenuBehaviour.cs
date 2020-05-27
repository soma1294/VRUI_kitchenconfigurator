using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowConfigMenuBehaviour : MonoBehaviour
{
    public CompassDirections wallIndex = CompassDirections.NORTH;

    public List<Window> windows = new List<Window>();

    public GameObject windowConfigPrefab;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Activate when windowConfigs are persisted between starts
        switch (wallIndex)
        {
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
        VRUIWindowConfigBehaviour currentWindowConfig;
        foreach (Window window in windows)
        {
            currentWindow = Instantiate(windowConfigPrefab, transform);
            currentWindowConfig = currentWindow?.GetComponent<VRUIWindowConfigBehaviour>();
            currentWindowConfig.window = window;
            currentWindowConfig.title.ChangeTextTo("Window " + (i + 1));
            currentWindowConfig.rightOffset.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
            currentWindowConfig.bottomOffset.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
            currentWindowConfig.width.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
            currentWindowConfig.height.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
            i++;
        }
    }

    public void AddWindow()
    {
        GameObject currentWindow = Instantiate(windowConfigPrefab, transform);
        VRUIWindowConfigBehaviour currentWindowConfig = currentWindow?.GetComponent<VRUIWindowConfigBehaviour>();
        currentWindowConfig.window = new Window(1000, 1000, 1000, 600);
        currentWindowConfig.title.ChangeTextTo("Window " + (i + 1));
        currentWindowConfig.rightOffset.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
        currentWindowConfig.bottomOffset.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
        currentWindowConfig.width.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
        currentWindowConfig.height.OnValueChanged.AddListener(currentWindowConfig.UpdateValues);
        i++;
    }
}
