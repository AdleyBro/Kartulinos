using System.Collections;
using UnityEngine;

public class GreenShellMovement : MonoBehaviour
{
    private Vector3 directionV;
    private Vector3 gravityV;
    private SphereCollider shellCollider;
    private Rigidbody shellBody;
    private GameObject particles;
    private GroundInfo groundInfo;
    private readonly Collider[] colliderBuffer = new Collider[8];
    private bool isOnFloor;
    private bool canCollide = true;
    private float speed = 30f;
    private float groundDist;
    private int maxHits = 3;

    private void Start()
    {
        directionV = transform.forward;
        shellCollider = GetComponent<SphereCollider>();
        shellBody = GetComponent<Rigidbody>();
        particles = Resources.Load<GameObject>("BrokenShellParticle");
        groundDist = shellCollider.bounds.extents.y + 0.05f;
        groundInfo = new GroundInfo(1f);
        groundInfo.SetHugFloor(true);
        //StartCoroutine(AutoDestroy());
    }

    private void FixedUpdate()
    {
        groundInfo.CheckGroundShell(transform, shellCollider, -transform.up);

        isOnFloor = groundInfo.floorDistance <= groundDist;
        if (isOnFloor) {
            gravityV = Vector3.zero;
            directionV = Vector3.ProjectOnPlane(directionV, groundInfo.floorNormal).normalized;
        }
        else
            gravityV = Vector3.down * 0.01f;

        Quaternion rotation = Quaternion.FromToRotation(shellBody.transform.up, groundInfo.floorNormal);
        shellBody.MoveRotation(rotation * shellBody.rotation);

        directionV += gravityV;
        shellBody.MovePosition(shellBody.position + directionV * speed * Time.deltaTime);
        shellBody.transform.position += SolvePenetration(shellBody.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canCollide)
            return;
        
        Vector3 collisionNormal = collision.GetContact(0).normal;
        print(Vector3.Angle(groundInfo.floorNormal, collisionNormal));
        if (Vector3.Angle(groundInfo.floorNormal, collisionNormal) >= 70f)
        {
            StartCoroutine(CollideCooldown());
            if (maxHits == 1)
                Break();
            maxHits--;
            directionV = Vector3.Reflect(directionV, collisionNormal);
        }
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(15f);
        Break();
    }

    IEnumerator CollideCooldown()
    {
        canCollide = false;
        yield return new WaitForSeconds(0.25f);
        canCollide = true;
    }

    private void Break()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(shellBody.gameObject);
    }

    private Vector3 SolvePenetration(Quaternion rotationStream)
    {
        Vector3 summedOffset = Vector3.zero;
        for (int solveIterations = 0; solveIterations < 6; solveIterations++)
            summedOffset = CalcPentrationOffset(rotationStream, summedOffset);

        return summedOffset;
    }

    private Vector3 CalcPentrationOffset(Quaternion rotationStream, Vector3 summedOffset)
    {
        Vector3 position = shellCollider.transform.position;
        int kartCapsuleHitCount = Physics.OverlapSphereNonAlloc(position, shellCollider.radius, colliderBuffer, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);

        for (int i = 0; i < kartCapsuleHitCount; i++)
        {
            Collider hitCollider = colliderBuffer[i];
            if (hitCollider == shellCollider)
                continue;

            Transform hitColliderTransform = hitCollider.transform;
            if (Physics.ComputePenetration(shellCollider, position + summedOffset, rotationStream, hitCollider, hitColliderTransform.position, hitColliderTransform.rotation, out Vector3 separationDirection, out float separationDistance))
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
}
