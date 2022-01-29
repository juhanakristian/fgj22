using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchTrigger : MonoBehaviour
{
    public bool isOn = false;
    public GameObject SwitchObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isOn = Mathf.Round(SwitchObject.transform.localRotation.eulerAngles.x) == 87; 
    }


}
