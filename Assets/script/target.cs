using UnityEngine;

public class target : MonoBehaviour
{
	public GameObject Player;
	public float speedReq = 75;
	public int damage = 10;
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
		if (Player.GetComponent<PlayerMovementAdvanced>().overallSpeed > speedReq)
		{
			Player.GetComponent<PlayerMovementAdvanced>().score += 1000;
			Destroy(gameObject);
			Debug.Log("destroyed");
		}
		else
		{
			Player.GetComponent<PlayerMovementAdvanced>().health -= damage;
			Destroy(gameObject);
			Debug.Log("destroyed");
		}
	}
}
