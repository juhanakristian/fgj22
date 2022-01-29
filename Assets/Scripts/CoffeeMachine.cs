using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeMachine : MonoBehaviour
{
	public UnityEvent brewingComplete; 

	public void StartBrewing()
	{
		Debug.Log("Brewing...", this);
		Debug.Log("Brewing done", this);
		brewingComplete.Invoke();
	}

	public void StopBrewing()
	{
		Debug.Log("Brewing canceled", this);
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}
}
