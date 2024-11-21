using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Transform target;
    public Vector3 target_offset;

    public bool trace;

    // Start is called before the first frame update
    void Start()
    {
        target_offset = transform.position - target.position;
        trace = true;
    }

    private void LateUpdate()
    {
        if (trace)
            transform.position = Vector3.Lerp(transform.position, target.position + target_offset, .125f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
