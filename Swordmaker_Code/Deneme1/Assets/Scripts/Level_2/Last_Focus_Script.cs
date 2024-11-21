using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Last_Focus_Script : MonoBehaviour
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
        if (transform.name.Equals("Last_Focus"))
        {
            if (cam.GetComponent<Camera_Script>().trace && other.transform.CompareTag("sword"))
            {
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
