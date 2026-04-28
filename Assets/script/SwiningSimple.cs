using UnityEngine;

public class SwingingSimple : MonoBehaviour
{
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;
    public PlayerMovementAdvanced pm;

    [Header("Swinging")]
    public float maxSwingDistance = 100f;
    private Vector3 swingPoint;
    private SpringJoint joint;

    public float spring = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;


    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse1;

    private void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwing(); // swing key input
        if (Input.GetKeyUp(swingKey)) StopSwing(); // stops swinging when the swing key is released
    }

    private void LateUpdate()
    {
        DrawRope(); // draws a rope when swinging is enabled
    }

    private void StartSwing()
    {
        GetComponent<Grappling>().StopGrapple(); // Stops the grapple function if the player is currently grappling, so that swinging and grappling don't interfere
    
        RaycastHit hit; // creates a straight line in the direction the camera is facing
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = hit.point;

            joint = player.gameObject.AddComponent<SpringJoint>(); // adds a spring joint to the player
            joint.autoConfigureConnectedAnchor = false; // stops the anchor point from being  automatically set
            joint.connectedAnchor = swingPoint; // sets the anchor point of the joint to the raycast hit point

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint); // calculates the distance between the player and the hit point

            joint.maxDistance = distanceFromPoint * 0.85f; // sets the maximum distance the join can extend to 85% of the distance between the player and the hit point
            joint.minDistance = distanceFromPoint * 0.65f; 

            joint.spring = spring; // sets the strength of the spring to keep the player attached to the swing point
            joint.damper = damper; // sets the damping of the spring to reduce the strenght of the spring force
            joint.massScale = massScale; // not fully sure what this value does but it makes the swing feel more weighty

            pm.swinging = true; // puts the player into swining mode
            lr.positionCount = 2; // sets the number of positions of the line renderer to two
            lr.enabled = true; // enables the line renderer to make 
        }
    }

    public void StopSwing()
    {
        lr.positionCount = 0;

        Destroy(joint); // destroys the joint component, so that the player is no longer attached to the swing point

        pm.swinging = false;

        lr.enabled = false;
    }
    private void DrawRope()
    {
        if (!joint) return; // if no joint was created, this function will stop as we don't want to draw a rope 

        lr.SetPosition(0, gunTip.position); // sets the first position of the line renderer to the position of the grapple gun tip
        lr.SetPosition(1, swingPoint); // sets the second position of the line renderer to the position of the previously established swing point
    }
}