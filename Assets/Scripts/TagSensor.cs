using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TagSensor : MonoBehaviour
{
    // Start is called before the first frame update
    public string TagToDetect;
    public UnityEvent onDetected;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagToDetect)
        {
            onDetected.Invoke();
        }

    }

}
