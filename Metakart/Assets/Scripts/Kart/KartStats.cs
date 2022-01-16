using UnityEngine;

[CreateAssetMenu(fileName = "New KartStats", menuName = "Kart Stats")]
public class KartStats : ScriptableObject
{
    public float accelForce;
    public int maxAccelForce;
    public int maxBwdSpeed;        // backward speed
    public float handling;         // more handling = tighter curves
    public int mass;               // more mass = bigger knockback on collisions to others
}
