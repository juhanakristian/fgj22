using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterJug : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WaterObject;
    public GameObject Drop;

    private float timer = 0f;
    private float timerMax = 0.1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y < -0.6f && timer > timerMax)
        {
            timer = 0f;
            StartCoroutine(PuourWater());
        }

        timer += Time.deltaTime;
    }

    IEnumerator PuourWater()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject water = GameObject.Instantiate(WaterObject);

        water.transform.position = Drop.transform.position;
        // water.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        // water.AddComponent<Rigidbody>();
    }
}
