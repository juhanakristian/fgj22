using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitchTrigger : MonoBehaviour
{
    public bool isOn = false;
    public GameObject SwitchObject;

    public UnityEvent toggledOn;
    public UnityEvent toggledOff;

    // Start is called before the first frame update
    void Start()
    {
	if (isOn) {
	    var r = SwitchObject.transform.localEulerAngles;
	    r.x = 87;
	    SwitchObject.transform.localEulerAngles = r;
	    SwitchObject.GetComponent<Rigidbody>().MoveRotation(SwitchObject.transform.rotation);

		toggledOn.Invoke();
	}
	else {
		toggledOff.Invoke();
	}
    }

    // Update is called once per frame
    void Update()
    {
        bool enabled = Mathf.Round(SwitchObject.transform.localRotation.eulerAngles.x) == 87; 

	if (enabled == isOn)
		return;

	isOn = enabled;

	if (enabled)
		toggledOn.Invoke();
	else
		toggledOff.Invoke();
    }


}
