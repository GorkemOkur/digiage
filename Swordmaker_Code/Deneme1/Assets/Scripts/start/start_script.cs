using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_script : MonoBehaviour
{
    private string level = "level";
    private string global_speed_key = "speed";
    private string global_damage_key = "damage";

    // Start is called before the first frame update
    void Start()
    {
        instance_variables();
        PlayerPrefs.Save();
        go_level();
    }

    void instance_variables()
    {
        if ( !PlayerPrefs.HasKey(global_speed_key) )
            PlayerPrefs.SetInt(global_speed_key, 1);

        if ( !PlayerPrefs.HasKey(global_damage_key) )
            PlayerPrefs.SetInt(global_damage_key, 1);
    }

    void go_level()
    {
        if ( PlayerPrefs.HasKey("level") )
        {
            level += PlayerPrefs.GetInt("level");

            SceneManager.LoadScene(level);
        }
        else
        {
            PlayerPrefs.SetInt(level, 1);

            level += "1";

            SceneManager.LoadScene(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
