using UnityEngine;

public class CamaraChaser : MonoBehaviour
{
    private Vector3 differenceV;
    private Vector3 wantedPosition;
    private float cameraFov = 60f;
    private float maxDistance;
    public KartMovement2 kartMovement;
    public Transform player;

    private void Start()
    {
        differenceV = kartMovement.transform.position - transform.position;
    }

    private void FixedUpdate()
    {
        // FOV ------------------------
        if (kartMovement.GetIsBoosting())
            cameraFov = Mathf.Lerp(cameraFov, 70f, 4f * Time.deltaTime);
        else
            cameraFov = Mathf.Lerp(cameraFov, 60f, 4f * Time.deltaTime);
        GetComponent<Camera>().fieldOfView = cameraFov;
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        /*
        Vector3 velocityDir;
        int drifting = kartMovement.GetDrifting();
        if (kartMovement.GetVelocity().magnitude < 0.1f)
            velocityDir = kartMovement.GetForward();
        else if (drifting == 0)
            velocityDir = Vector3.ProjectOnPlane(kartMovement.GetVelocity(), kartMovement.GetUp()).normalized;
        else
            velocityDir = Quaternion.AngleAxis(-30f * drifting, kartMovement.GetUp()) * kartMovement.GetForward();

        wantedDir = Vector3.Lerp(wantedDir, velocityDir, 10f * Time.deltaTime);
        Vector3 height = Vector3.zero;
        lookAt = player.position + kartMovement.GetUp() / 2f;

        if (kartMovement.GetLookBack()) {
            lookAt -= wantedDir;
        }
        */

        Quaternion rotation = Quaternion.FromToRotation(differenceV, kartMovement.GetForward());
        differenceV = rotation * differenceV;
        Vector3 position = kartMovement.transform.position + differenceV;
        wantedPosition = Vector3.Lerp(wantedPosition, position, Time.deltaTime * 7f);
        transform.position = rotation * wantedPosition;
    }
}
