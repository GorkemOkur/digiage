using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class level_begin_script : MonoBehaviour
{

    public TextMeshProUGUI money;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("points"))
            money.text = "" + PlayerPrefs.GetInt("points");

        PlayerPrefs.SetInt("level", 2 );

        Debug.Log("____________::::::::::      Points:    " + PlayerPrefs.GetInt("points"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
