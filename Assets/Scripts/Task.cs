using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
	public UnityEvent started;
	public UnityEvent completed;

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
	}

	public void FinishTask()
	{
		Debug.Log("Finished", this);
		completed.Invoke();
	}
}
