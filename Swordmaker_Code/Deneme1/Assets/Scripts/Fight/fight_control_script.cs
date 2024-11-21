using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fight_control_script : MonoBehaviour
{
    public Camera main;
    public Camera cam;
    public GameObject me;

    private Animator me_animator;
    private Animator cam_animator;

    // Start is called before the first frame update
    void Start()
    {
        cam_animator = cam.GetComponent<Animator>();
        me_animator = me.GetComponent<Animator>();

    }

    public void start_fight()
    {
        Debug.Log("Hi from Fight");
        cam.gameObject.SetActive(true);
        main.gameObject.SetActive(false);

        cam_animator.Play("fight_cam_begin");
        me_animator.Play("enter_fight");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
