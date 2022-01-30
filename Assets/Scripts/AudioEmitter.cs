using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    var a = GetComponent<AudioSource>();
	    if (!a)
		    Destroy(this.gameObject);

	    if (!a.isPlaying)
		    Destroy(this.gameObject);
    }
}
