using UnityEngine;
using UnityEngine.Events;

public class Rotator : MonoBehaviour
{
	public Vector3 torque;
	public float counter = 0;

    void FixedUpdate()
    {
	    GetComponent<Rigidbody>().AddTorque(Time.fixedDeltaTime * torque * counter);

	    counter += Time.fixedDeltaTime;

	    if (counter > 5.0f) {
		    GetComponent<Rigidbody>().AddForce(Vector3.up * 100);
	    }
    }
}
