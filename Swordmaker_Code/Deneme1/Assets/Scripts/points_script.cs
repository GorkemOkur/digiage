using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points_script : MonoBehaviour
{

    private float destroy_time=1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroy_time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
