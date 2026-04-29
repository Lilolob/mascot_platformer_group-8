using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour

{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the main menu is active
	}
    
    public void Update()
    {
		Cursor.lockState = CursorLockMode.None;
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayGame();
		}
	}

	public string sceneName;
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
