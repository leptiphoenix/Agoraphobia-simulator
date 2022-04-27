using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handPhysicsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position)/Time.fixedDeltaTime;

        Quaternion rotationDif = target.rotation*Quaternion.Inverse(transform.rotation);
        rotationDif.ToAngleAxis(out float angleDegree, out Vector3 rotationAxis);

        Vector3 rotatDiffInDegree = angleDegree*rotationAxis;
        rb.angularVelocity = (rotatDiffInDegree*Mathf.Deg2Rad/Time.fixedDeltaTime);
    }
}
