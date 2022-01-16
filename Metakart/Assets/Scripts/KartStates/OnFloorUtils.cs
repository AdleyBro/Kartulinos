
using kart_action;
using UnityEngine;

public static class OnFloorUtils
{
    public static void UpdateIsMovingForward(KartAction k)
    {
        float angle = Vector3.Angle(k.kartBody.transform.forward, k.kartBody.velocity);

        k.isMovingForward = angle < 90f || angle > 270f;
    }

    public static void RotateKartToFloorNormal(KartAction k)
    {
        //floorAngle = k.groundInfo.FloorAngle(k.transform.up);
        //if (floorAngle < 45f && k.groundInfo.floorNormal != Vector3.zero)
        //    k.floorNormalV = k.groundInfo.floorNormal;

        k.floorNormalV = k.groundInfo.floorNormal;
        Quaternion wantedRotation = Quaternion.FromToRotation(k.kartBody.transform.up, k.floorNormalV) * k.kartBody.rotation;
        k.kartBody.rotation = Quaternion.Slerp(k.kartBody.rotation, wantedRotation.normalized, 12f * Time.fixedDeltaTime);
    }

    // This function makes sure the Kart's box collider is floating in the air, simulating
    // it has real wheels
    public static void KeepWheelsOffset(KartAction k)
    {
        k.offset = (0.25f - k.groundInfo.floorDistance) * 8f * k.groundInfo.floorNormal;
        k.kartBody.AddForce(k.offset, ForceMode.VelocityChange);
    }

    public static void KeepVelocityAttachedToFloor(KartAction k)
    {
        k.kartBody.velocity = Vector3.ProjectOnPlane(k.kartBody.velocity, k.floorNormalV).normalized * k.kartBody.velocity.magnitude;
    }

    public static void ApplyAcceleration(KartAction k)
    {
        k.accelV = k.transform.forward * k.accelInput * (k.kartBody.velocity.magnitude + 0.1f) * k.currentAccelForce;
        k.accelV = Vector3.ClampMagnitude(k.accelV, k.stats.maxAccelForce);
    }

    public static void ApplyFloorFriction(KartAction k)
    {
        Vector3 frontalFriction = -Vector3.Project(k.kartBody.velocity, k.transform.forward) * k.groundInfo.frontalFricForce;
        k.kartBody.AddForce(frontalFriction);

        Vector3 lateralFriction = -Vector3.Project(k.kartBody.velocity, k.transform.right) * k.groundInfo.lateralFricForce;
        k.kartBody.AddForce(lateralFriction);
    }
}
