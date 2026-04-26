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
        if (Input.GetKeyDown(swingKey)) StartSwing();
        if (Input.GetKeyUp(swingKey)) StopSwing();
    }

    private void LateUpdate()
    {
        DrawRope();
    }


    private void StartSwing()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            pm.swinging = true;

            lr.positionCount = 2;

            lr.enabled = true;
        }
    }

    public void StopSwing()
    {

        lr.positionCount = 0;

        Destroy(joint);

        pm.swinging = false;

        lr.enabled = false;
    }
    private void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }
}