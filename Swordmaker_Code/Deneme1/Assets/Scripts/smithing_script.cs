using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class smithing_script : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.transform.CompareTag("sword_part_smithing")  ){
            anim.Play("smithing");
            other.transform.tag = "sword_part_sharpening";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.transform.CompareTag("sword_part_sharpening")  ){}
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
