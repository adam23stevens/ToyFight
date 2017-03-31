using UnityEngine;
using System.Collections;

public class GravitySource : MonoBehaviour {

    public Vector3 GravitationalCenter = Vector3.zero;
    public float GravitationalConstant = -9.8f;

    public bool DistanceStrengthens = false;

    private float attractorMass;

    void Start()
    {
        attractorMass = this.GetComponentInParent<Rigidbody>().mass;
    }

    Vector3 FindSurface(Rigidbody objectBody)
    {
        float distance = Vector3.Distance(this.transform.position, objectBody.transform.position);
        Vector3 surfaceNorm = Vector3.zero;

        RaycastHit rayHit;
        if (Physics.Raycast(objectBody.transform.position, -objectBody.transform.up, out rayHit, distance))
        {
            surfaceNorm = rayHit.normal;
        }
        return surfaceNorm;
    }

    void OrientBody(Rigidbody objectBody, Vector3 surfaceNorm)
    {
        objectBody.transform.localRotation = Quaternion.FromToRotation(objectBody.transform.up, surfaceNorm) * objectBody.rotation;
    }

    public void Attract(Rigidbody objectBody)
    {
        Debug.Log("plaent Mass = " + attractorMass);

        Vector3 pullVec = FindSurface(objectBody);
        OrientBody(objectBody, pullVec);

        float pullForce = 0.0f;
        if (!DistanceStrengthens)
        {
            pullForce = GravitationalConstant * ((attractorMass * objectBody.mass) /
                                                Mathf.Pow(Vector3.Distance(this.transform.position + GravitationalCenter, objectBody.transform.position), 2));
        }
        else
        {
            pullForce = GravitationalConstant * (attractorMass * objectBody.mass) *
                                                Vector3.Distance(this.transform.position + GravitationalCenter, objectBody.transform.position);
        }
        pullVec = objectBody.transform.position - GravitationalCenter;

        Debug.Log("pull force = " + pullVec);
        objectBody.AddForce(pullVec.normalized * pullForce * Time.deltaTime);
    }
}
