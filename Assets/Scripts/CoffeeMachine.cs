using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeMachine : MonoBehaviour
{
	public UnityEvent brewingComplete; 
	public float brewingDuration = 5.0f;
	public float brewingTimer = 0.0f;
	public bool brewing = false;

	private IEnumerator coro;

	public void StartBrewing()
	{
		StopBrewing();

		// Already brewed?
		if (brewingTimer >= brewingDuration)
			return;

		Debug.Log("Brewing...", this);
		coro = Brew();
		brewing = true;
		StartCoroutine(coro);
	}

	public void StopBrewing()
	{
		if (!brewing)
			return;

		brewing = false;
		Debug.Log("Brewing canceled", this);

		StopCoroutine(coro);
	}

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	private IEnumerator Brew()
	{
		while (brewingTimer < brewingDuration) {
			yield return new WaitForSeconds(0.1f);
			brewingTimer += 0.1f;
		}

		Debug.Log("Brewing done", this);
		brewingComplete.Invoke();
	}
}
