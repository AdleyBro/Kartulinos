using kart_action;
using UnityEngine;

public class Steering
{
    private readonly KartAction k;

    public Steering(KartAction kart)
    {
        k = kart;
    }

    public void AlignKartToNormal(Vector3 normal, float speed)
    {
        //floorAngle = k.groundInfo.FloorAngle(k.transform.up);
        //if (floorAngle < 45f && k.groundInfo.floorNormal != Vector3.zero)
        //    k.floorNormalV = k.groundInfo.floorNormal;

        Quaternion wantedRotation = Quaternion.FromToRotation(k.krigidbody.transform.up, normal) * k.krigidbody.rotation;
        k.krigidbody.rotation = Quaternion.Slerp(k.krigidbody.rotation, wantedRotation.normalized, speed * Time.fixedDeltaTime);
    }

    public bool IsMovingForward()
    {
        float angle = Vector3.Angle(k.GetForward(), k.krigidbody.velocity);
        return angle < 90f || angle > 270f;
    }
}
