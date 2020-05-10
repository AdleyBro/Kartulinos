using UnityEngine;

public class KartMovement2 : MonoBehaviour
{
    private Transform[] raysPosList; //Raycast order: FrontLeft, FrontRight, BackLeft, BackRight
    private Rigidbody kartBody;
    private GroundInfo groundInfo;
    private CapsuleCollider kartCollider;
    private Collider[] colliderBuffer = new Collider[8];
    private float maxSpeed;
    private float maxSteer;
    private float accel_val;
    private float accel_in;
    private float acceleration;
    private float friction;
    private float steer_in;
    private readonly float gravFactor = 0.2f;
    private float gravity;
    private float groundDist;
    private bool lowOffset;
    private Vector3 accel_v;
    private Vector3 velocity_v;
    private Vector3 inertia_v;
    private Vector3 normal_v;

    public void Start()
    {
        raysPosList = new Transform[] { transform.Find("FLRayLoc"), transform.Find("FRRayLoc"), 
                                        transform.Find("BLRayLoc"), transform.Find("BRRayLoc") };
        maxSpeed = 0.3f;
        maxSteer = 2f;
        kartCollider = GetComponent<CapsuleCollider>();
        kartBody = GetComponent<Rigidbody>();
        groundDist = kartCollider.bounds.extents.y + 0.2f;
        groundInfo = new GroundInfo(1f);
        velocity_v = Vector3.zero;
        accel_v = Vector3.zero;
        inertia_v = Vector3.zero;
        gravity = 0f;
        accel_val = 0.2f;
        acceleration = 0f;
        normal_v = kartBody.transform.up;
    }

    public void Update()
    {
        PlayerInput();
        PrintRays(); // Debug purpose only
    }

    public void FixedUpdate()
    {
        Vector3 friction_v = Vector3.zero;
        Vector3 direction_v = kartBody.transform.forward;
        Vector3 kartPosition = kartBody.position;
        Vector3 offset = SolvePenetration(kartBody.rotation);

        groundInfo.CheckGround(raysPosList, Vector3.down, kartCollider);
        if (lowOffset || groundInfo.floorDistance <= groundDist) // If kart on floor
        {
            gravity = 0f;
            acceleration += accel_in * accel_val * Time.deltaTime;
            if (accel_in == 0f) {
                friction = -acceleration * 0.6f * Time.deltaTime;
                acceleration += friction;
            }
                
            accel_v = direction_v * acceleration;
            inertia_v = accel_v;    // Furrula bien pero cuando aterriza en el suelo la inercia desaparece
        }
        else
        {
            if (offset == Vector3.zero)
                gravity += gravFactor * Time.deltaTime;
            accel_v = inertia_v;
        }

        if (acceleration > maxSpeed)
            acceleration = maxSpeed;

        velocity_v = accel_v + friction_v;
        velocity_v.y -= gravity;

        //if (groundInfo.floorDistance <= groundDist)
        //    offset += -kartBody.transform.up * 0.01f;
        
        kartBody.MovePosition(kartPosition + velocity_v + offset);

        normal_v = Vector3.Lerp(normal_v, groundInfo.floorNormal, 10f * Time.deltaTime);
        Quaternion rotation = Quaternion.FromToRotation(kartBody.transform.up, normal_v);
        rotation *= Quaternion.Euler(0, maxSteer * steer_in, 0);
        kartBody.MoveRotation(rotation * kartBody.rotation);
    }

    private Vector3 SolvePenetration(Quaternion rotationStream)
    {
        Vector3 summedOffset = Vector3.zero;
        for (var solveIterations = 0; solveIterations < 3; solveIterations++)
            summedOffset = CalcPentrationOffset(rotationStream, summedOffset);

        return summedOffset;
    }

    private Vector3 CalcPentrationOffset(Quaternion rotationStream, Vector3 summedOffset)
    {
        Vector3 position = kartCollider.transform.position;
        var capsuleAxis = rotationStream * Vector3.forward * kartCollider.height * 0.5f;
        var point0 = position + capsuleAxis + summedOffset;
        var point1 = position - capsuleAxis + summedOffset;
        int kartCapsuleHitCount = Physics.OverlapCapsuleNonAlloc(point0, point1, kartCollider.radius, colliderBuffer, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
        
        for (int i = 0; i < kartCapsuleHitCount; i++)
        {
            var hitCollider = colliderBuffer[i];
            if (hitCollider == kartCollider)
                continue;

            var hitColliderTransform = hitCollider.transform;
            if (Physics.ComputePenetration(kartCollider, position + summedOffset, rotationStream, hitCollider, hitColliderTransform.position, hitColliderTransform.rotation, out Vector3 separationDirection, out float separationDistance))
            {
                Vector3 offset = separationDirection * (separationDistance + Physics.defaultContactOffset);
                if (Mathf.Abs(offset.x) > Mathf.Abs(summedOffset.x))
                    summedOffset.x = offset.x;
                if (Mathf.Abs(offset.y) > Mathf.Abs(summedOffset.y))
                    summedOffset.y = offset.y;
                if (Mathf.Abs(offset.z) > Mathf.Abs(summedOffset.z))
                    summedOffset.z = offset.z;
            }
        }

        float offsetMagn = summedOffset.sqrMagnitude;
        if (offsetMagn > 0f && offsetMagn <= 0.01f)
            lowOffset = true;
        else
            lowOffset = false;

        return summedOffset;
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            accel_in = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            accel_in = -1f;
        else
            accel_in = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            steer_in = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            steer_in = -1f;
        else
            steer_in = 0;
    }

    private void PrintRays()
    {
        foreach (Transform rayTrans in raysPosList)
            Debug.DrawRay(rayTrans.position, Vector3.down * 1f, Color.green);
    }
}