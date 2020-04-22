using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MaterialConfig", menuName = "Furniture/Material")]
public class MaterialConfig : ScriptableObject {

	public string description;

    public Material material;
}
