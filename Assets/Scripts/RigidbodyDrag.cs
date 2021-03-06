using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

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

    /// Minimum distance to the drag object.
    public float minDistance = 1.0f;

    /// Maximum distance to pick and drag objects to.
    public float maxDistance = 2.5f;

    /// Material to use for highlight object
    public Material highlightMaterial;

    /// Currently dragged object.
    public GameObject target;

    private Camera cam;

    public BrainSharer brainSharer;

    public Texture2D Crosshair;

    /// Joint used for dragging.
    private Transform joint;

    private GameObject highlightObject;
    private GameObject highlightMeshObject;

    void Start()
    {
        if (!highlightMaterial)
            Debug.LogWarning("No highlight material assigned", this);


        try {
            Vector2 cursorOffset = new Vector2(Crosshair.width / 2, Crosshair.height / 2);
            Cursor.SetCursor(Crosshair, cursorOffset, CursorMode.ForceSoftware);
        } catch (UnassignedReferenceException){
            Debug.LogWarning("No Cursor was set");
        }


        cam = GetComponent<Camera>();
 
        if (cam == null){
            Debug.Log("Using CinemaBrain");
            cam = brainSharer.GetBrain().OutputCamera;
        }

     
    }



    void Update()
    {
        if (!IsDragging())
        {
            CheckPick(Input.mousePosition, Input.GetMouseButtonDown(0));
        }

        if (Input.GetMouseButtonUp(0))
        {
            Detach();
        }

        if (joint)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                joint.Rotate(cam.transform.forward, Input.mouseScrollDelta.y * 10f);
            }
            else
            {
                distance += Input.mouseScrollDelta.y * 0.1f;
                distance = Mathf.Max(distance, minDistance);
                distance = Mathf.Min(distance, maxDistance);
            }
        }

        Cursor.visible = !IsDragging();
    }

    

    void FixedUpdate()
    {
        if (joint)
        {
            OnMouseDrag();
        }
    }



    void OnMouseDrag()
    {
        if (!joint)
            return;

        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        joint.position = worldPos;
    }

    public void CheckPick(Vector3 screenPos, bool startDragging)
    {
        // Stop dragging previous object
        Detach();

        RaycastHit hit;
        var ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, maxDistance, layers))
        {
            Highlight(null);
            return;
        }

        // Hit only a collider? Cant drag that.
        if (!hit.rigidbody)
        {
            Highlight(null);
            return;
        }

        Highlight(hit.rigidbody.gameObject);

        if (startDragging)
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

        dragStart.Invoke();
    }

    public void Detach()
    {
        if (!joint)
            return;

        Destroy(joint.gameObject);
        joint = null;

        dragEnd.Invoke();
    }

    public bool IsDragging()
    {
        return joint != null && target != null;
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

    public void Highlight(GameObject target)
    {
        // Nothing to do?
        if (highlightObject == target)
            return;

        // Remove old highlight
        Destroy(highlightMeshObject);
        highlightMeshObject = null;

        highlightObject = target;

        if (!target)
            return;

        if (!highlightMaterial)
            return;

        var srcf = target.GetComponent<MeshFilter>();
        if (!srcf)
        {
            // No mesh filters in the object to be highlighted?
            return;
        }

        var srcr = target.GetComponent<MeshRenderer>();

        highlightMeshObject = new GameObject();
        highlightMeshObject.transform.SetParent(target.transform, false);
        var mr = highlightMeshObject.AddComponent<MeshRenderer>();
        var mf = highlightMeshObject.AddComponent<MeshFilter>();

        mf.sharedMesh = srcf.sharedMesh;

        var mats = srcr.materials;
        for (int i = 0; i < mats.Length; ++i)
        {
            mats[i] = highlightMaterial;
        }

        mr.materials = mats;
        this.target = target.gameObject;
    }
}
