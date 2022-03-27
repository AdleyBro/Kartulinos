using UnityEngine;
using kart_action;
using kartstates;

public class CameraChaser : MonoBehaviour
{
    private readonly int DEFAULT_FOV = 60;
    private readonly int BOOST_FOV = 70;
    private readonly float DISTANCE = 3.1f;
    private readonly float HEIGHT = 1.5f;
    private float currentFov;
    private Camera cameraChaser;
    private Vector3 cameraPoint;            // Point where the camera focus
    private Vector3 wantedPosition;         // Position for the camera
    private KartAction k;               // All info of the kart to chase

    private void Start()
    {
        cameraChaser = GetComponent<Camera>();
        k = GetComponentInParent<KartAction>();
        wantedPosition = k.transform.position - k.GetForward() * DISTANCE;
        cameraPoint = Vector3.up * 1.1f + k.transform.position;
    }

    private void FixedUpdate()
    {
        // FOV ------------------------
        if (k.stateManager.IsPowerUpActive(typeof(BoostState)))
            currentFov = Mathf.Lerp(currentFov, BOOST_FOV, 6f * Time.deltaTime); // Boost fov
        else
            currentFov = Mathf.Lerp(currentFov, DEFAULT_FOV, 6f * Time.deltaTime);      // Normal fov
        cameraChaser.fieldOfView = currentFov;                        // Sets fov to camera
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if (k.stateManager.IsInState(typeof(Wounded)))
        {
            Vector3 dir = Vector3.ProjectOnPlane(wantedPosition - k.transform.position, k.GetUp()).normalized;
            wantedPosition = Vector3.Lerp(wantedPosition,  dir * DISTANCE + k.transform.position + Vector3.up * HEIGHT, 6f * Time.fixedDeltaTime);
            return;
        }

        Vector3 forward = Vector3.ProjectOnPlane(k.GetForward(), Vector3.up).normalized;
        Vector3 futurePosition = k.transform.position - forward * DISTANCE + Vector3.up * HEIGHT;
        if (k.isMovingForward)
            wantedPosition = Vector3.Lerp(wantedPosition, futurePosition, 12f * Time.fixedDeltaTime);
        else
            wantedPosition = Vector3.Lerp(wantedPosition, futurePosition, 12f * Time.fixedDeltaTime);

        if (k.lookBack)
            wantedPosition = k.transform.position + k.GetForward() * DISTANCE + k.GetUp() * HEIGHT;
        else if (Vector3.Distance(wantedPosition, futurePosition) > 2f)
            wantedPosition = futurePosition;
    }

    private void LateUpdate()
    {
        wantedPosition = CylinderPositionClamp(wantedPosition, k.transform.position, DISTANCE);
        transform.position = wantedPosition;

        cameraPoint = Vector3.up * 1.1f + k.transform.position;
        transform.LookAt(cameraPoint, Vector3.up);
    }

    private Vector3 CylinderPositionClamp(Vector3 vector, Vector3 centre, float radius)
    {
        Vector3 wantedPositionD = Vector3.Project(vector - centre, k.GetUp()) + centre;
        Vector3 dir = wantedPositionD - vector;
        Vector3 fixedDir = dir.normalized * radius;
        return vector + dir - fixedDir;
    }

    private void LimitCameraAngle()
    {
        float angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(cameraPoint + k.transform.position - wantedPosition, k.GetUp()), k.GetForward(), k.GetUp());
        Debug.Log(angle);
        if (angle > 17f)
            wantedPosition = Quaternion.AngleAxis(angle - 17f, k.GetUp()) * (wantedPosition - k.transform.position) + k.transform.position;
        else if (angle < -17f)
            wantedPosition = Quaternion.AngleAxis(angle + 17f, k.GetUp()) * (wantedPosition - k.transform.position) + k.transform.position;
    }
}
