using kart_action;
using kartstates;
using System.Collections;
using UnityEngine;

public class GreenShellMovement : MonoBehaviour
{
    private SphereCollider shellCollider;
    private Rigidbody shellBody;
    private GameObject particles;
    private GroundInfo groundInfo;
    private MeshRenderer model;
    private int maxHits = 10;
    private float timer = 0f;
    private float maxSecondsAlive = 15f;
    private readonly float STUNDURATION = 2f;

    private void Start()
    {
        shellCollider = GetComponent<SphereCollider>();
        shellBody = GetComponent<Rigidbody>();
        model = GetComponent<MeshRenderer>();
        particles = Resources.Load<GameObject>("BrokenShellParticle");
        groundInfo = new GroundInfo(1f, shellCollider.bounds.extents.y, true);

        shellBody.AddForce(transform.forward * 2400f);
    }

    private void Update()
    {
        Quaternion wantedRotation = Quaternion.FromToRotation(model.transform.up, groundInfo.floorNormal) * model.transform.rotation;
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, wantedRotation.normalized, 1);
        model.transform.Rotate(model.transform.up, 600f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (timer >= maxSecondsAlive)
            Break();
        else
            timer += Time.fixedDeltaTime;

        groundInfo.CheckGroundShell(transform, shellCollider, -model.transform.up);
        if (!groundInfo.IsOnFloor())
            shellBody.AddForce(Vector3.down * 20f);
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

        if (collider.tag.Equals("Projectile"))
            Break();
               
        
        Vector3 collisionNormal = collision.GetContact(0).normal;
        if (Vector3.Angle(groundInfo.floorNormal, collisionNormal) >= 60f)
        {
            if (maxHits < 2)
                Break();
            else
            {
                maxHits--;
                shellBody.velocity = Vector3.Reflect(-collision.relativeVelocity, collisionNormal);
            }
        }
    }

    private void Break()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(shellBody.gameObject);
    }
}
