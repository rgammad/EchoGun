using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	bool isPause = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;
				if(isPause)
					Time.timeScale = 0;
				else
					Time.timeScale = 1;
		}
	}

	void onGUI() {
		if (isPause) {
			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			GUI.Label (new Rect (Screen.width/2-50, Screen.height/2-25, 100, 50), "BLAH", centeredStyle);
		}
	}
}
