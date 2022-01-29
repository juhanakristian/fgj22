using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision with " + other.gameObject.name);
        if (other.gameObject.tag != "Water")
            StartCoroutine(DeSpawn());
    }

    IEnumerator DeSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
