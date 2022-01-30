using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
	public UnityEvent started;
	public UnityEvent completed;
	public AudioClip clips;
	public AudioSource source;

	public List<GameObject> objectsToEnable;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartTask()
	{
		Debug.Log("Started", this);
		started.Invoke();

		foreach (var it in objectsToEnable) {
			it.SetActive(true);
		}
	}

	public void FinishTask()
	{
		Debug.Log("Finished", this);
		completed.Invoke();
		foreach (var it in objectsToEnable) {
			it.SetActive(false);
		}
	}
}
