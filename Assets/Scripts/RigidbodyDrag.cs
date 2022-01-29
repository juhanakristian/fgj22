using UnityEngine;
using UnityEngine.Events;

public class RigidbodyDrag : MonoBehaviour
{
	/// Invoked whenever dragging starts.
	public UnityEvent dragStart;

	/// Invoked whenever dragging stops.
	public UnityEvent dragEnd;

	//// Layers to pick draggable objects from.
	public LayerMask layers;

	/// How strong force is applied to dragging
	public float force = 600;

	/// Spring damping effect.
	public float damping = 6;

	/// Current drag distance.
	public float distance = 0;

	/// Currently dragged object.
	private Rigidbody target;

	/// Joint used for dragging.
	private Transform joint;

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			DragByRaycast(Input.mousePosition);
		}

		if (Input.GetMouseButtonUp(0)) {
			Detach();
		}

		if (joint) {
			distance += Input.mouseScrollDelta.y * 0.5f;
		}
	}

	void FixedUpdate()
	{
		if (joint) {
			OnMouseDrag();
		}
	}

	void OnMouseDrag()
	{
		if (!joint)
			return;

		Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
		joint.position = worldPos;
	}

	public void DragByRaycast(Vector3 screenPos, float maxDistance=Mathf.Infinity)
	{
		RaycastHit hit;

		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (!Physics.Raycast(ray, out hit, maxDistance, layers))
			return;

		if (!hit.rigidbody)
			return;

		Attach(hit.rigidbody, hit.point);
	}

	/// Drag point is specified in world coordinates.
	public void Attach(Rigidbody tgt, Vector3 dragPoint)
	{
		distance = Vector3.Distance(dragPoint, transform.position);

		GameObject drago = new GameObject("DragObject");
		drago.transform.position = dragPoint;

		Rigidbody dragrb = drago.AddComponent<Rigidbody>();
		dragrb.isKinematic = true;

		ConfigurableJoint dragj = drago.AddComponent<ConfigurableJoint>();
		dragj.connectedBody = tgt;
		dragj.configuredInWorldSpace = true;
		dragj.xDrive = NewJointDrive();
		dragj.yDrive = NewJointDrive();
		dragj.zDrive = NewJointDrive();
		dragj.slerpDrive = NewJointDrive();
		dragj.rotationDriveMode = RotationDriveMode.Slerp;

		joint = dragj.transform;
	}

	public void Detach()
	{
		if (!joint)
			return;

		Destroy(joint.gameObject);
		joint = null;
	}

	public JointDrive NewJointDrive()
	{
		JointDrive ret = new JointDrive();
		// ret.mode = JointDriveMode.Position; // Obsolete?
		ret.positionSpring = force;
		ret.positionDamper = damping;
		ret.maximumForce = Mathf.Infinity;
	
		return ret;
	}
}
