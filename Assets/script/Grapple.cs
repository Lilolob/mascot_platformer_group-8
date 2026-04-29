using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovementAdvanced pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    public float grapplingCdTimer;
    public bool grapplingCdBool;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovementAdvanced>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple(); // grapple key input

        if (grapplingCdTimer > 0) // grapple cooldown timer, checks if cooldown is active, if so sets grapplingCooldownBool to false
        {
            grapplingCdTimer -= Time.deltaTime; 
            pm.playerGpCdTimer = grapplingCdTimer; // updated for UI reasons
            grapplingCdBool = false;
            pm.playerGpCd = grapplingCdBool; // updated for UI reasons
        }
        else // if grapple coodlwon timer is not active, sets grapplingCooldownBool to true
        {
            grapplingCdBool = true;
            pm.playerGpCd = grapplingCdBool; // updated for UI reasons
        }
    }

    private void LateUpdate()
    {
        if (grappling)
            DrawRope();
    }

    private void StartGrapple()
    {
        if (grapplingCdBool == false) return;

        GetComponent<SwingingSimple>().StopSwing(); // deactivates swinging

        grappling = true;

        RaycastHit hit; // creates a straight line in the direction the camera is facing
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable)) // checks if the raycasts hits a valid grapple point
        {
            grapplePoint = hit.point; 

            Invoke(nameof(ExecuteGrapple), grappleDelayTime); // calls the execute grapple function with a delay
            lr.enabled = true; // enables the line renderer 
            grapplingCdTimer = grapplingCd; // starts the grapple cooldown timer
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance; // if the hit point isn't valid, grapple point is set to max grapple distance

            Invoke(nameof(StopGrapple), grappleDelayTime); // stops grapple with a delay
        }

        
    }

    private void ExecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z); // finds the vector of the lowest point of the player

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y; //  calculates the relative y position of the grapple point to the lowest point of the player
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis; // calculates the highest point on the arc of the grapple

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis; // if the grapple point is below the lowest point of the player, the apex of the arc is set to the overshootYAxis value

        pm.JumpToPosition(grapplePoint, highestPointOnArc); // calls the jump to position function from the player movement script, passing in the grapple point and the highest point on the arc

        Invoke(nameof(StopGrapple), 1f); // stops the grapple function after 1 second
    }
    private void DrawRope()
    {
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
    public void StopGrapple()
    {

        grappling = false;
        lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}