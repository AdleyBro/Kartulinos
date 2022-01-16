using kart_action;
using kartstates;
using System.Collections;
using UnityEngine;

public class BananaPeelAction : MonoBehaviour
{
    private GameObject particles;
    private Rigidbody rigidBody;
    private SphereCollider bananaCollider;
    private GroundInfo groundInfo;
    private MeshRenderer model;
    private readonly float STUNDURATION = 2f;

    private void Start()
    {
        bananaCollider = GetComponent<SphereCollider>();
        rigidBody = GetComponent<Rigidbody>();
        particles = Resources.Load<GameObject>("BrokenShellParticle");
        model = GetComponent<MeshRenderer>();
        groundInfo = new GroundInfo(1f, bananaCollider.bounds.extents.y, true);
    }

    private void Update()
    {
        Quaternion wantedRotation = Quaternion.FromToRotation(model.transform.up, groundInfo.floorNormal) * model.transform.rotation;
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, wantedRotation.normalized, 1);
    }

    private void FixedUpdate()
    {
        groundInfo.CheckGroundShell(transform, bananaCollider, -model.transform.up);
        if (!groundInfo.IsOnFloor())
            rigidBody.AddForce(Vector3.down * 20f); // mass is 1 so no need to multiply by it
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.tag.Equals("Player"))
        {
            KartAction k = collider.GetComponent<KartAction>();
            k.stateManager.ChangeState(new Wounded(k, STUNDURATION));
            Break();
        }
        else if (collider.tag.Equals("Projectile"))
            Break();

        Vector3 collisionNormal = collision.GetContact(0).normal;
        if (Vector3.Angle(groundInfo.floorNormal, collisionNormal) >= 60f)
        {
            rigidBody.velocity = Vector3.Reflect(-collision.relativeVelocity, collisionNormal) * 0.5f;
        }
    }

    private void Break()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(bananaCollider.gameObject);
    }
}
