using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Task : MonoBehaviour
{
	public UnityEvent started;
	public UnityEvent completed;
	public AudioSource source;
	public AudioClip startedClip;
	public AudioClip finishedClip;

	public bool isCompleted = false;

	public List<GameObject> objectsToEnable;

	public List<string> prerequisites;
	public List<string> tasks;

	// Start is called before the first frame update
	void Awake()
	{
		gameObject.SetActive(false);
		CheckPrerequisites();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void StartTask()
	{
		if (isCompleted)
			return;

		if (gameObject.active)
			return;

		gameObject.SetActive(true);

		if (startedClip)
			source.PlayOneShot(startedClip);

		Debug.Log("Started", this);
		started.Invoke();

		foreach (var it in objectsToEnable) {
			it.SetActive(true);
		}

		if (tasks.Count == 0 && prerequisites.Count == 0)
			FinishTask();
	}

	public void FinishTask()
	{
		if (isCompleted)
			return;

		Debug.Log("Finished", this);
		completed.Invoke();
		foreach (var it in objectsToEnable) {
			it.SetActive(false);
		}

		if (finishedClip)
			source.PlayOneShot(finishedClip);

		gameObject.SetActive(false);

		isCompleted = true;
	}

	public void FinishPrerequisite(string name)
	{
		prerequisites.Remove(name);
		CheckPrerequisites();
	}

	public void FinishTask(string name)
	{
		tasks.Remove(name);
		if (tasks.Count == 0 && gameObject.active)
			FinishTask();
	}

	public bool CheckPrerequisites()
	{
		if (prerequisites.Count == 0) {
			StartTask();
			return true;
		}
		else {
			return false;
		}
	}
}
