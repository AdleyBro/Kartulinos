using UnityEngine;

public class CamaraChaser : MonoBehaviour
{
    private Vector3 wantedDir;
    private Vector3 lookAt;
    private float cameraFov = 60f;
    public KartMovement2 kartMovement;
    public Transform player;

    private void FixedUpdate()
    {
        if (kartMovement.GetIsBoosting())
            cameraFov = Mathf.Lerp(cameraFov, 70f, 4f * Time.deltaTime);
        else
            cameraFov = Mathf.Lerp(cameraFov, 60f, 4f * Time.deltaTime);
        GetComponent<Camera>().fieldOfView = cameraFov;

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

        height = Vector3.Lerp(height, kartMovement.GetUp() * 8f, 8f * Time.deltaTime);
        transform.LookAt(lookAt);
    }
}
