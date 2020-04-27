using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowConfigMenuBehaviour : MonoBehaviour
{
    public CompassDirections wallIndex = CompassDirections.NORTH;

    public List<Window> windows = new List<Window>();

    public GameObject windowConfigPrefab;

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
        WindowConfigBehaviour currentWindowConfig;
        foreach (Window window in windows)
        {
            //currentWindow = Instantiate(windowConfigPrefab, transform);
            //currentWindowConfig = currentWindow?.GetComponent<WindowConfigBehaviour>();
            //currentWindowConfig.window = window;
            //currentWindowConfig.toggleGroup = toggleGroup;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
