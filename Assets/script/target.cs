using UnityEngine;

public class target : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("faggot");
		if (other.GetComponent<PlayerMovementAdvanced>().overallSpeed > 35)
		{
			Destroy(gameObject);
			Debug.Log("destroyed");
		}
		else
		{
			other.GetComponent<playerhealth>().health -= 10;
			Destroy(gameObject);
			Debug.Log("destroyed");
		}
	}
}
