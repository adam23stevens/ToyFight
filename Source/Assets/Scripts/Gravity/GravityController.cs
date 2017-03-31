using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {

    public GravitySource gravitySource;
    public Rigidbody objectBody;

    void LateUpdate()
    {
        if (gravitySource != null && objectBody != null)
        {
            gravitySource.Attract(objectBody);
        }
    }
}
