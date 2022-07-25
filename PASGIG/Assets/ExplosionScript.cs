using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitForFinish());
    }

    // Update is called once per frame
    IEnumerator waitForFinish()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
