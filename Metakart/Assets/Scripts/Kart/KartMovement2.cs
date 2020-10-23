using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class KartMovement2 : MonoBehaviour
{
    private Transform[] raysPosList; //Raycast order: FrontLeft, FrontRight, BackLeft, BackRight
    private Rigidbody kartBody;
    private GroundInfo groundInfo;
    private CapsuleCollider kartCollider;
    private readonly Collider[] colliderBuffer = new Collider[8];
    private Item holdingItem = null;
    private readonly float fMaxAccel = 0.5f; //Default 0.5f
    private readonly float bMaxAccel = -0.2f;
    private readonly float maxSpeed = 24f;
    private readonly float maxSteer = 1.25f;
    private readonly float driftSteer = 1.2f;
    private readonly float accel = 0.075f; //Default 0.08f
    private readonly float mass = 15f;
    private float frontalFrictionCoef;
    private float lateralFrictionCoef;
    private float accelInput;
    private float acceleration = 0f;
    private float steerInput;
    private float steering = 0f;
    private float gravity;
    private float groundDist;
    private int drifting;
    private bool isOnFloor;
    private bool isMovingForward;
    private bool lookBack;
    private bool isBoosting;
    private bool canDrift = true;
    private Vector3 gravityV;
    private Vector3 accelV = Vector3.zero;
    private Vector3 frictionV = Vector3.zero;
    private Vector3 velocityV = Vector3.zero;
    private Vector3 normalV;
    private Vector3 floorNormalV;

    public void Start()
    {
        raysPosList = new Transform[] { transform.Find("FLRayLoc"), transform.Find("FRRayLoc"), 
                                        transform.Find("BLRayLoc"), transform.Find("BRRayLoc") };
        kartCollider = GetComponent<CapsuleCollider>();
        kartBody = GetComponent<Rigidbody>();
        groundDist = kartCollider.bounds.extents.y + 0.05f;
        groundInfo = new GroundInfo(1f);
        groundInfo.SetHugFloor(true);
        normalV = kartBody.transform.up;
        gravity = 0.25f;
        gravityV = Vector3.down * gravity;
        lookBack = false;
    }

    public void Update()
    {
        //PrintRays(); // Debug purpose only
    }

    public void FixedUpdate()
    {
        UpdateIsMovingForward();
        UpdateFrictionCoefs();

        float rotationSpeed;

        Vector3 offset = SolvePenetration(kartBody.rotation);
        groundInfo.CheckGroundKart(raysPosList, kartCollider, -transform.up);

        isOnFloor = groundInfo.floorDistance <= groundDist;
        if (isOnFloor)
        {
            floorNormalV = groundInfo.floorNormal;
            rotationSpeed = 12f;
            gravityV = Vector3.zero;

            if (accelInput != 0f || isBoosting)
            {
                acceleration += accelInput * accel;
                if (acceleration > fMaxAccel || isBoosting)
                    acceleration = fMaxAccel;
                if (acceleration < bMaxAccel)
                    acceleration = bMaxAccel;
            }
            else
                acceleration = 0f;

            frictionV = (isMovingForward? -1f : 1f) * transform.forward * velocityV.magnitude * frontalFrictionCoef * mass;
            if (drifting == 0) {
                float frictDivisor;
                if (velocityV.magnitude > 2f)
                    frictDivisor = 1f;
                else
                    frictDivisor = 5f + velocityV.magnitude;

                frictionV += Vector3.Project(-velocityV.normalized, transform.right) * lateralFrictionCoef / (mass * frictDivisor);
                //frictionV += Vector3.Project(-velocityV.normalized, transform.right) * lateralFrictionCoef * mass; // LATERAL FRICTION
            }

            accelV = transform.forward * acceleration;
            velocityV += accelV + frictionV;
            if (isBoosting) {
                velocityV *= 20f;
                velocityV = Vector3.ClampMagnitude(velocityV, 28f);
            }
            else
                velocityV = Vector3.ClampMagnitude(velocityV, maxSpeed);
            
            velocityV = GetForwardFrom(groundInfo.floorNormal, velocityV);
            if (drifting != 0)
                velocityV = Vector3.Lerp(velocityV, Quaternion.AngleAxis(-drifting * 50f - steerInput * 8f, transform.up) * transform.forward * velocityV.magnitude, 6f * Time.deltaTime);

            if (velocityV.magnitude < 1f && accelInput == 0f)
                velocityV = Vector3.zero;
        }
        else
        {
            floorNormalV = Vector3.up;
            rotationSpeed = 2f;
            gravityV = Vector3.down * gravity;
            velocityV += gravityV;
        }

        kartBody.MovePosition(kartBody.position + offset + velocityV * Time.deltaTime);

        normalV = Vector3.Lerp(normalV, floorNormalV, rotationSpeed * Time.deltaTime);
        Quaternion rotation = Quaternion.FromToRotation(kartBody.transform.up, normalV);

        float sense = isMovingForward ? 1f : -1f;
        steering = maxSteer * steerInput * sense;
        if (drifting != 0)
        {
            rotation *= Quaternion.AngleAxis(drifting * driftSteer + steering * 0.5f, kartBody.transform.up);
        } else if (velocityV.magnitude > 1f)
        {
            rotation *= Quaternion.AngleAxis(steering, kartBody.transform.up);
        }
        
        kartBody.MoveRotation(rotation * kartBody.rotation);
    }

    private void UpdateIsMovingForward()
    {
        float angle = Vector3.Angle(transform.forward, velocityV);

        if (angle > 90f && angle < 270f)
            isMovingForward = false;
        else
            isMovingForward = true;
    }

    private void UpdateFrictionCoefs()
    {
        frontalFrictionCoef = groundInfo.frontalFrictionCoef;
        lateralFrictionCoef = groundInfo.lateralFrictionCoef;
    }

    private Vector3 SolvePenetration(Quaternion rotationStream)
    {
        Vector3 summedOffset = Vector3.zero;
        for (int solveIterations = 0; solveIterations < 3; solveIterations++)
            summedOffset = CalcPentrationOffset(rotationStream, summedOffset);

        return summedOffset;
    }

    private Vector3 CalcPentrationOffset(Quaternion rotationStream, Vector3 summedOffset)
    {
        Vector3 position = kartCollider.transform.position;
        Vector3 capsuleAxis = rotationStream * Vector3.forward * kartCollider.height * 0.5f;
        Vector3 point0 = position + capsuleAxis + summedOffset;
        Vector3 point1 = position - capsuleAxis + summedOffset;
        int kartCapsuleHitCount = Physics.OverlapCapsuleNonAlloc(point0, point1, kartCollider.radius, colliderBuffer, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
        
        for (int i = 0; i < kartCapsuleHitCount; i++)
        {
            Collider hitCollider = colliderBuffer[i];
            if (hitCollider == kartCollider)
                continue;

            Transform hitColliderTransform = hitCollider.transform;
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

        return summedOffset;
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 collisionNormal;
        int length = collision.contactCount;
        for (int i = 0; i < 3; i++)
        {
            if (i > length-1)
                break;
            collisionNormal = collision.GetContact(i).normal;
            float collisionAngle = Vector3.Angle(collisionNormal, transform.up);

            if (collisionAngle > 60f && collisionAngle < 300f)
                velocityV = Vector3.Lerp(velocityV, Vector3.zero, 5f * Time.deltaTime);
        }
        
    }

    private void OnSteering(InputValue value)
    {
        steerInput = value.Get<float>();
    }

    private void OnAccelerating(InputValue value)
    {
        accelInput = value.Get<float>();
    }

    private void OnRegressing(InputValue value)
    {
        accelInput = -value.Get<float>();
    }

    private void OnLookBack(InputValue value)
    {
        lookBack = value.Get<float>() > 0;
    }

    private void OnDrifting(InputValue value)
    {
        if (value.Get<float>() == 0f && drifting != 0)
            StartCoroutine("DriftBoost");

        if (canDrift && value.Get<float>() > 0f && isOnFloor && velocityV.magnitude > 0.2f && steerInput != 0f)
            drifting = steerInput > 0f ? 1 : -1;
        else
            drifting = 0;
    }

    private void OnUsingItem(InputValue value)
    {
        if (holdingItem != null && value.Get<float>() > 0f)
        {
            holdingItem.Utilize(this);
            holdingItem = null;
        }
    }

    private void OnGetItem()
    {
        SetHoldingItem(new GreenShell());
    }

    private IEnumerator DriftBoost()
    {
        canDrift = false;
        isBoosting = true;
        yield return new WaitForSeconds(0.8f);
        canDrift = true;
        isBoosting = false;
    }

    public void SpawnItem(GameObject item)
    {
        Instantiate(item, transform.position + transform.forward * 2f, transform.rotation);
    }

    private Vector3 GetForwardFrom(Vector3 normal, Vector3 velocity)
    {
        Vector3 temp = Vector3.Cross(normal, velocity);
        return Vector3.Cross(temp, normal);
    }

    private void PrintRays()
    {
        foreach (Transform rayTrans in raysPosList)
            Debug.DrawRay(rayTrans.position, Vector3.down * 1f, Color.green);
    }

    public float GetSteerIn()
    {
        return steerInput;
    }

    public Vector3 GetKartRight()
    {
        return transform.right;
    }

    public bool GetIsMovingForward()
    {
        return isMovingForward;
    }

    public bool GetLookBack()
    {
        return lookBack;
    }

    public Vector3 GetVelocity()
    {
        return velocityV;
    }

    public Vector3 GetForward()
    {
        return transform.forward;
    }

    public Vector3 GetUp()
    {
        return transform.up;
    }

    public int GetDrifting()
    {
        return drifting;
    }

    public bool GetIsBoosting()
    {
        return isBoosting;
    }

    public void SetIsBoosting(bool value)
    {
        isBoosting = value;
    }

    public Item GetHoldingItem()
    {
        return holdingItem;
    }

    public void SetHoldingItem(Item item)
    {
        holdingItem = item;
    }

    public bool IsHoldingItem()
    {
        return holdingItem != null;
    }
}