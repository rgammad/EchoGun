using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
	bool isPause = false;
	public Text text;
	public Button resume;
	public Button quit;
	public Image image;
	// Use this for initialization
	void Start () {
		resume.onClick.AddListener(proc);
		quit.onClick.AddListener(fini);
	}

	public void fini () {
		Time.timeScale = 1;
		SceneManager.LoadScene (0);
	}

	public void proc () {

        isPause = false;
        Time.timeScale = 1;
		text.gameObject.SetActive (false);
		resume.gameObject.SetActive (false);
		quit.gameObject.SetActive (false);
		image.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!isPause) {
                isPause = true;
                Time.timeScale = 0;
				text.gameObject.SetActive (true);
				resume.gameObject.SetActive (true);
				quit.gameObject.SetActive (true);
				image.gameObject.SetActive (true);
			} else {
                proc();
			}
		}

	}

}
