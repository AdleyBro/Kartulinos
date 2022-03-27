using kart_action;

public interface OnFloorInterface
{
    void ApplyKartSteering(KartAction k);

    void RotateKartToFloorNormal(KartAction k);
    
    void KeepVelocityAttachedToFloor(KartAction k);

    void ApplyAcceleration(KartAction k);

    void ApplyFloorFriction(KartAction k);
}
