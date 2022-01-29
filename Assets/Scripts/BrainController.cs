using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DitzelGames.FastIK;

public class BrainController : MonoBehaviour
{
	public GameObject armEnd1;
	public GameObject armEnd2;
	public GameObject armEnd3;
	public GameObject armEnd4;

	private GameObject armIK1;
	private GameObject armIK2;
	private GameObject armIK3;
	private GameObject armIK4;

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

		armIK1.transform.position = armEnd1.transform.position;
		armIK2.transform.position = armEnd2.transform.position;
		armIK3.transform.position = armEnd3.transform.position;
		armIK4.transform.position = armEnd4.transform.position;

		armIK1.transform.LookAt(-transform.position);
		armIK2.transform.LookAt(-transform.position);
		armIK3.transform.LookAt(-transform.position);
		armIK4.transform.LookAt(-transform.position);

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
	}

	// Update is called once per frame
	void Update()
	{
		if (!dragger.IsDragging()) {
			dragArm = null;
		}

		dragArm = armIK1;
 
		if (dragger.IsDragging()) {
			dragArm.transform.position = dragger.target.transform.position;
		}
	}

	void OnDragStart()
	{
	}

	void OnDragEnd()
	{
	}
}
