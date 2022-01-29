using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectFall : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent onFall;
    private bool hasFallen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasFallen) return;

        if (transform.up.y < 0.6f) {
            onFall.Invoke();
            hasFallen = true;
        }
    }
}
