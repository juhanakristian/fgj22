using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receptacle : MonoBehaviour
{
	public UnityEvent onInsert;
	public GameObject allowedObject;

	public bool inserted = false;

	public GameObject insertedObject;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Enter");

		Rigidbody orb = collider.GetComponent<Rigidbody>();

		// We hit something strange?
		if (!orb) {
			return;
		}

		// Trying to insert something else?
		if (orb.gameObject != allowedObject)
			return;

		orb.transform.SetParent(transform, true);
		orb.transform.position = transform.position;
		orb.transform.rotation = transform.rotation;
		orb.isKinematic = true;

		insertedObject = orb.gameObject;

		inserted = true;
		onInsert.Invoke();
	}
}
