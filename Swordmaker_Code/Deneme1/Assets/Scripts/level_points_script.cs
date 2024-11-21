using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Last_points_script : MonoBehaviour
{

    public GameObject canvas;
    public TextMeshProUGUI infoText;
    public GameObject cam;
    public GameObject n1;
    public GameObject n2;

    private Quaternion new_angle;
    private Vector3 new_offset;
    // Start is called before the first frame update
    void Start()
    {
        new_angle = Quaternion.Euler(20, 0, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(transform.name.Equals("Lava_Focus") )
            infoText.text = "spill the melted steel now!";

        if (transform.name.Equals("Water_Focus"))
            infoText.text = "We need to freeze them with water";

        if (transform.name.Equals("Smithing_Focus"))
            infoText.text = "Forge them to shape well";

        if (transform.name.Equals("Sharpening_Focus"))
            infoText.text = "Grind all of'em !";

        if (transform.name.Equals("Last_Focus")){
            infoText.text = "Now, You need to spend your stacks for damage and speed.";
            

            if ( cam.GetComponent<Camera_Script>().trace && other.transform.CompareTag("sword")){
                n1.SetActive(true);
                n2.SetActive(true);

                cam.transform.rotation = new_angle;
                Vector3 new_pos = cam.transform.position;
                new_pos.z = 90f;
                cam.transform.position = new Vector3(0, 4, 90);

                new_offset = cam.GetComponent<Camera_Script>().target_offset;
                new_offset.y = 3f;
                new_offset.z -= 3f;
                cam.GetComponent<Camera_Script>().target_offset = new_offset;

                cam.GetComponent<Camera_Script>().trace = false;
            }
            
        }
            

        if (other.transform.CompareTag("sword"))
            StartCoroutine(pauseGame());
    }

    IEnumerator pauseGame(){
        
        Time.timeScale = 0f;
        canvas.SetActive(true);
        
        yield return new WaitForSecondsRealtime(1f);

        canvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
