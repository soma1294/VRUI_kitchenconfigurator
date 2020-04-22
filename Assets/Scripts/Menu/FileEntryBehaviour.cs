using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileEntryBehaviour : MonoBehaviour {

	public string filename;

	public Text text;

	public FileListBehaviour fileList;

	public Button loadButton;

	public Button overwriteButton;

	public bool saveEnabled;


	// Use this for initialization
	void Start()
	{
		if (filename != null) {
			text.text = filename;
			loadButton.onClick.AddListener(delegate {
				fileList.LoadFile(filename);
			});
			overwriteButton.onClick.AddListener(delegate {
				fileList.SaveFile(filename);
			});
			overwriteButton.gameObject.SetActive(saveEnabled);
		}
	}
}
