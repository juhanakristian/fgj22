using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public GameObject brokenVersion;
    public float breakForce = 3.0f;
    public float collisionForce = 0.5f;
    public AudioClip collisionClip;
    public AudioClip breakClip;
    public UnityEvent onBroken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
	    if (brokenVersion && collision.impulse.magnitude > breakForce) {
		    Break();

		    GameObject go = new GameObject("AudioEmitter");
		    go.AddComponent<AudioEmitter>();
		    var a = go.AddComponent<AudioSource>();
		    a.clip = breakClip;
		    a.Play();
	    }
	    else if (collision.impulse.magnitude > collisionForce) {
		    GameObject go = new GameObject("AudioEmitter");
		    go.AddComponent<AudioEmitter>();
		    var a = go.AddComponent<AudioSource>();
		    a.clip = collisionClip;
		    a.Play();
	    }
    }

    void Break()
    {
	    GameObject broken = Instantiate(brokenVersion);
	    broken.transform.position = transform.position;
	    broken.transform.rotation = transform.rotation;

	    onBroken.Invoke();

	    Destroy(this.gameObject);
    }
}
