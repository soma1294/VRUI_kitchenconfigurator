using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Variables {
    public static string measuringSystem = "SMS";

    public static int[] heightsInMM = { 127, 254, 381, 508, 635, 762, 889, 1016, 1143, 1270, 1397, 1524, 1651, 1778, 1905, 2032 };
    public static int[] widthsInMM = { 275, 400, 450, 500, 550, 600, 900 };
    public static int[] depthsInMM = { 330, 600 };
    public static int[] offFloorHeightsInMM = { 0, 127, 254, 381, 508, 635, 762, 889, 1016, 1143, 1270, 1397, 1524, 1651, 1778, 1905, 2032 };

    public static int baseHeightInMM = 110;
    public static int baseInsetInMM = 50;
    public static int wallThicknessInMM = 16;
    public static int workPlateThicknessInMM = 42;
    public static int workPlateOverhangInMM = 40;

    public static Material workPlateMaterial;
    public static Material furnitureMaterial;

    public static Material floorMaterial;
    public static Material wallMaterial;
    public static Material ceilingMaterial;

    public static Material handlesMaterial;
    public static GameObject handlesPrefab;

    public static Vector3 roomMeasurements;
    public static List<Window> northWindows = new List<Window>();
    public static List<Window> eastWindows = new List<Window>();
    public static List<Window> southWindows = new List<Window>();
    public static List<Window> westWindows = new List<Window>();

	public static KitchenData loadedData;
	public static string loadedFile;
}
