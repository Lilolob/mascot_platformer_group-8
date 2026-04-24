using UnityEngine;
using UnityEngine.SceneManagement;

public class deathplane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void Update()
    {

	}

	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		SceneManager.LoadScene("MAINMENU");
	}
}
