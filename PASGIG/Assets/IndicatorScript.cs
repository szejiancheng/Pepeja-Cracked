using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{

    public bool isVisible;
    public GameObject Indicator;

    public Transform Target;

    Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Target == null)
        {
            Target = GetComponent<FlyingEnemy>().player;
            if (Target == null)
            {
                Indicator.SetActive(false);
            }
            //Debug.Log(Target);
        } else {
            if(rd.isVisible == false)
        {
            isVisible = false;
            if (Indicator.activeSelf == false)
            {
                Indicator.SetActive(true);
            }
            Vector2 direction = Target.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);


            if (ray.collider!= null)
            {
                Indicator.transform.position = ray.point;
                Indicator.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f)*60, new Vector3(1, 1, 1)*60, 6000/ray.fraction );
                //Debug.Log(ray.fraction);
            }
        }
        else 
        {
            isVisible = true;
            if (Indicator.activeSelf == true)
            {
                Indicator.SetActive(false);
            }
        }
        }

        
    }
}
