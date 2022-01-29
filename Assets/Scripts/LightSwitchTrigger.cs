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
    }

    // Update is called once per frame
    void Update()
    {
        bool enabled = Mathf.Round(SwitchObject.transform.localRotation.eulerAngles.x) == 87; 

	if (enabled == isOn)
		return;

	if (enabled)
		toggledOn.Invoke();
	else
		toggledOff.Invoke();
    }


}
