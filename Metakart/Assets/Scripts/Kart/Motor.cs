using kart_action;
using UnityEngine;

public class Motor
{
    private readonly KartAction k;
    private Vector3 accelV;

    public Motor(KartAction kart)
    {
        k = kart;
    }

    public void ApplyAcceleration()
    {
        accelV = k.GetForward() * k.accelInput * (k.krigidbody.velocity.magnitude + 0.1f) * k.currentAccelForce;
        accelV = Vector3.ClampMagnitude(k.accelV, k.stats.maxAccelForce);
    }

    public void ApplyFloorFriction()
    {
        float[] friction = k.groundInfo.GetFrictionForces(k.krigidbody.position, Vector3.down);
        Vector3 frontalFriction = -Vector3.Project(k.krigidbody.velocity, k.transform.forward) * friction[0];
        k.krigidbody.AddForce(frontalFriction);

        Vector3 lateralFriction = -Vector3.Project(k.krigidbody.velocity, k.transform.right) * friction[1];
        k.krigidbody.AddForce(lateralFriction);
    }

    public void KeepVelocityAttachedToFloor()
    {
        k.krigidbody.velocity = Vector3.ProjectOnPlane(k.krigidbody.velocity, k.groundInfo.GetFloorNormal()).normalized * k.krigidbody.velocity.magnitude;
    }
}
