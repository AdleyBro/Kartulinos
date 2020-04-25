using UnityEngine;

// This method takes a list of raycast transforms and gathers information about the floor beneath the kart.
public class GroundInfo
{
    private float rayDistance;
    public bool oneRayHits; //Like isOnFloor
    public float floorDistance; //Is the smallest distance from one of the four raycasts to the floor
    public Vector3 floorNormal;
    public float floorHeight;
    public RaycastHit[] hitsList;
    public int numOfHits;

    public GroundInfo(float rayDistance)
    {
        this.rayDistance = rayDistance;
        oneRayHits = false;
        hitsList = new RaycastHit[4];
    }

    public void CheckGround(Transform[] raysList, Vector3 rayDirection, Collider kartcollider)
    {
        numOfHits = 0;
        float smallestDist = 32f;
        bool rayHits = false;
        Vector3 normal = Vector3.zero;
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(raysList[i].position, rayDirection, out RaycastHit hit, rayDistance))
            {
                if (hit.collider == kartcollider)
                    continue;

                numOfHits += 1;
                hitsList[i] = hit;
                rayHits = true;
                normal += hit.normal;
                if (hit.distance < smallestDist)
                    smallestDist = hit.distance;
            }
        }
        
        oneRayHits = rayHits;
        floorDistance = smallestDist;
        if (rayHits)
            floorNormal = normal.normalized;
        else
            floorNormal = Vector3.up;
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
