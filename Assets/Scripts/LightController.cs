using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
	public AnimationCurve startCurve;
	public AnimationCurve stopCurve;

	public float duration = 2.0f;

	public Light target;

	private AnimationCurve currentCurve;
	private float startTime = 0;
	private float origIntensity = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		if (!target)
			target = GetComponent<Light>();

		startTime = Time.time;
		origIntensity = target.intensity;

		ToggleOn();
	}

	// Update is called once per frame
	void Update()
	{
		float reltime = (Time.time - startTime) / duration;
		if (reltime > 1.0f)
			reltime = 1.0f;

		target.intensity = currentCurve.Evaluate(reltime) * origIntensity;
	}

	public void ToggleOn()
	{
		if (currentCurve == startCurve)
			return;

		startTime = Time.time;
		currentCurve = startCurve;
	}

	public void ToggleOff()
	{
		if (currentCurve == stopCurve)
			return;

		startTime = Time.time;
		currentCurve = stopCurve;
	}
}
