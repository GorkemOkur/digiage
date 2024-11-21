using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class menu_script : MonoBehaviour
{

    public GameObject setting_panel;
    public GameObject sound_panel;
    public Button sound_button;

    public Animator animator;
    public TextMeshProUGUI money;

    public Sprite sound_on;
    public Sprite sound_off;

    bool opened;
    bool sound_change;

    // Start is called before the first frame update
    void Start()
    {
        animator = sound_panel.transform.GetComponent<Animator>();
        opened = false;
        sound_change = true;
    }


    public void setting_button(){
        if (opened=!opened)
            animator.Play("menu_open");
        else
            animator.Play("menu_close");
    }

    public void change_sound()
    {
        Debug.Log("" + sound_off.name);
        if (sound_change = !sound_change)
            sound_button.GetComponent<Image>().sprite = sound_on;
        else
            sound_button.GetComponent<Image>().sprite = sound_off;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("points"))
            money.text = "" + PlayerPrefs.GetInt("points");
    }
}
