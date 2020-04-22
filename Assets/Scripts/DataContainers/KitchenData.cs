using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KitchenData {
	public VariablesData variables;

	public FurnitureData furniture;

	public KitchenData(VariablesData variables, FurnitureData furniture) {
		this.variables = variables;
		this.furniture = furniture;
	}
}
