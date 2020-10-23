using UnityEngine;

// This method takes a list of raycast transforms and gathers information about the floor beneath the kart.
public class GroundInfo
{
    private bool hugFloor;
    private readonly float rayDistance;
    private float smallestDist;
    public float frontalFrictionCoef;
    public float lateralFrictionCoef;
    public float floorDistance; //Is the smallest distance from one of the four raycasts to the floor
    public Vector3 floorNormal;
    public RaycastHit[] hitsList;
    public int numOfHits;

    public GroundInfo(float rayDistance)
    {
        this.rayDistance = rayDistance;
        hitsList = new RaycastHit[4];
        hugFloor = false;
    }

    public void CheckGroundKart(Transform[] raysList, CapsuleCollider kartCollider, Vector3 kartDown)
    {
        numOfHits = 0;
        bool rayHits = false;
        Vector3 normal = Vector3.zero;
        smallestDist = 2f;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(raysList[i].position, hugFloor ? kartDown : Vector3.down, out RaycastHit hit, rayDistance))
            {
                if (hit.collider == kartCollider)
                    continue;

                Vector3 hitNormal = hit.normal;
                if (Vector3.Angle(-kartDown, hitNormal) < 50f) {
                    numOfHits += 1;
                    hitsList[i] = hit;
                    rayHits = true;
                    normal += hit.normal;

                    if (hit.distance < smallestDist)
                        smallestDist = hit.distance;
                }            
            }
        }

        if (Physics.Raycast(kartCollider.transform.position, kartDown, out RaycastHit hit1, rayDistance))
        {
            if (hit1.distance < smallestDist)
                smallestDist = hit1.distance;

            string floorType = hit1.transform.tag;
            if (floorType.Equals("Offroad")) {
                frontalFrictionCoef = 0.004f;
                lateralFrictionCoef = 60f;
            }
            else if (floorType.Equals("Dirt"))
            {
                frontalFrictionCoef = 0.001f;
                lateralFrictionCoef = 15f;
            }
            else if (floorType.Equals("Road")) {
                frontalFrictionCoef = 0.001f;
                lateralFrictionCoef = 60f;
            }
        }

        floorDistance = smallestDist;
        if (rayHits)
            floorNormal = normal.normalized;
        else
            floorNormal = Vector3.up;
    }

    public void CheckGroundShell(Transform rayPos, SphereCollider shellCollider, Vector3 shellDown)
    {
        smallestDist = 4f;
        if (Physics.Raycast(rayPos.position, hugFloor ? shellDown : Vector3.down, out RaycastHit hit, rayDistance))
        {
            Vector3 hitNormal = hit.normal;
            if (hit.distance < smallestDist)
                floorDistance = hit.distance;
            floorNormal = hitNormal.normalized;
        } else
        {
            floorNormal = Vector3.up;
            floorDistance = smallestDist;
        }
    }

    public void SetHugFloor(bool hugFloor)
    {
        this.hugFloor = hugFloor;
    }

    public void PrintHits()
    {
        string mensaje = "FL: " + hitsList[0].normal + "\n" +
                  "FR:  " + hitsList[1].normal + "\n" +
                  "BL:  " + hitsList[2].normal + "\n" +
                  "BR: " + hitsList[3].normal + "\n";

        Debug.Log(mensaje);
    }

    public void PrintBools()
    {
        string mensaje = "";
        for (int i = 0; i < 4; i++)
        {
            if (hitsList[i].GetType() != null)
                mensaje += i;
        }
        Debug.Log(mensaje);
    }
}
