using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DitzelGames.FastIK;

public class BrainController : MonoBehaviour
{
	public Rigidbody root;

	public GameObject armEnd1;
	public GameObject armEnd2;
	public GameObject armEnd3;
	public GameObject armEnd4;

	private GameObject armIK1;
	private GameObject armIK2;
	private GameObject armIK3;
	private GameObject armIK4;

	private Vector3 armTarget1;
	private Vector3 armTarget2;
	private Vector3 armTarget3;
	private Vector3 armTarget4;

	public RigidbodyDrag dragger;

	public GameObject dragArm;

	// Start is called before the first frame update
	void Start()
	{
		dragger.dragStart.AddListener(OnDragStart);
		dragger.dragEnd.AddListener(OnDragEnd);

		armIK1 = new GameObject("IK1");
		armIK2 = new GameObject("IK2");
		armIK3 = new GameObject("IK3");
		armIK4 = new GameObject("IK4");

		armIK1.transform.position = armEnd1.transform.position * 0.5f;
		armIK2.transform.position = armEnd2.transform.position * 0.5f;
		armIK3.transform.position = armEnd3.transform.position * 0.5f;
		armIK4.transform.position = armEnd4.transform.position * 0.5f;

		armIK1.transform.LookAt(new Vector3(0, 0, -1));
		armIK2.transform.LookAt(new Vector3(0, 0, -1));
		armIK3.transform.LookAt(new Vector3(0, 0, -1));
		armIK4.transform.LookAt(new Vector3(0, 0, -1));

		var ik1 = armEnd1.AddComponent<FastIKFabric>();
		var ik2 = armEnd2.AddComponent<FastIKFabric>();
		var ik3 = armEnd3.AddComponent<FastIKFabric>();
		var ik4 = armEnd4.AddComponent<FastIKFabric>();

		ik1.Target = armIK1.transform;
		ik2.Target = armIK2.transform;
		ik3.Target = armIK3.transform;
		ik4.Target = armIK4.transform;

		ik1.ChainLength = 3;
		ik2.ChainLength = 3;
		ik3.ChainLength = 3;
		ik4.ChainLength = 3;

		armIK1.transform.parent = transform;
		armIK2.transform.parent = transform;
		armIK3.transform.parent = transform;
		armIK4.transform.parent = transform;

		armTarget1 = armIK1.transform.localPosition;
		armTarget2 = armIK2.transform.localPosition;
		armTarget3 = armIK3.transform.localPosition;
		armTarget4 = armIK4.transform.localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		if (!dragger.IsDragging()) {
			dragArm = null;
		}

		dragArm = armIK2;

		armIK1.transform.localPosition = Vector3.MoveTowards(armIK1.transform.localPosition, armTarget1, Time.deltaTime);
		armIK2.transform.localPosition = Vector3.MoveTowards(armIK2.transform.localPosition, armTarget2, Time.deltaTime);
		armIK3.transform.localPosition = Vector3.MoveTowards(armIK3.transform.localPosition, armTarget3, Time.deltaTime);
		armIK4.transform.localPosition = Vector3.MoveTowards(armIK4.transform.localPosition, armTarget4, Time.deltaTime);
 
		if (dragger.IsDragging()) {
			dragArm.transform.position = dragger.target.transform.position;
		}


		if (Input.GetMouseButton(1)) {
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * Time.deltaTime * 100.0f);
		}
	}

	void OnDragStart()
	{
	}

	void OnDragEnd()
	{
	}
}
