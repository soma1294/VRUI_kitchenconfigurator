using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HandlesConfig", menuName = "Furniture/Handle")]
public class HandlesConfig : ScriptableObject {

    public string description;

    public GameObject handlesPrefab;
}
