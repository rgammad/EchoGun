using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

     public void OptionsButton()
    {
        SceneManager.LoadScene(2);
    }

	public void ControlsButton()
	{
		SceneManager.LoadScene (3);
	}

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
