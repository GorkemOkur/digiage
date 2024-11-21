using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_point_script : MonoBehaviour
{
    public List<GameObject> speeds;
    public List<GameObject> damages;

    public Material enable_mat;

    public TextMesh speed_name;
    public TextMesh damage_name;

    public GameObject fight_object;

    private int speed_cnt;
    private int damage_cnt;

    private string speed_key = "local_speed";
    private string damage_key = "local_damage";

    private string global_speed_key = "speed";
    private string global_damage_key = "damage";

    private List<string> speed_part_sword;
    private List<string> damage_part_sword;

    // Start is called before the first frame update
    void Start()
    {
        level1();

        speed_part_sword = new List<string> { "Slow", "Fast", "Quick", "Great", "Ex", "Mex", "Tex", "Cex" };
        damage_part_sword = new List<string> { "Edge", "Razor", "Blade", "Slicer", "Sword", "Calibur", "Blow", "Lancelot" };

        // setting variables for speed
        speed_name.text = speed_part_sword[ (PlayerPrefs.GetInt(global_speed_key) - 1) % 8];
        speed_cnt = PlayerPrefs.GetInt(global_speed_key);
        PlayerPrefs.SetInt(speed_key, speed_cnt);
        
        // setting variables for damage
        damage_name.text = damage_part_sword[ (PlayerPrefs.GetInt(global_damage_key) - 1) % 8];
        damage_cnt = PlayerPrefs.GetInt(global_damage_key);
        PlayerPrefs.SetInt(damage_key, damage_cnt);

        handle_points();
    }

    void level1()
    {
        PlayerPrefs.SetInt(global_speed_key, 1);
        PlayerPrefs.SetInt(global_damage_key, 1);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("sword_part_ready"))
        {
            // if the x compenent of positon is lower than Zero and speed counter lower than 8,
            // then it counted as point for speed. 
            if ( other.transform.position.x < 0 && speed_cnt < 8 )
            {
                handleSpeed(other);
            }
            // If x position is equal or bigger than Zero and damage counter lower than 8,
            // then it counted as point for damage.
            else if( other.transform.position.x > -1 && damage_cnt < 8)
            {
                handleDamage(other);
            }
        }

        // If a not complated sword part reaches the end, then just destroy it.
        if (!(other.transform.CompareTag("sword")))
            Destroy(other.gameObject);


        // If the sword reached the end, then pause the game.
        if (other.transform.CompareTag("sword"))
            StartCoroutine(start_fight(other));
    }

    // handle speed related compomnets.
    void handleSpeed(Collider other)
    {
        // Changing material of coefficient of sword power.
        speeds[speed_cnt-1].GetComponent<Renderer>().material = enable_mat;

        // changing the name of the upgraded sword.
        speed_name.text = speed_part_sword[ (speed_cnt-1) % 8];

        // Set speed for fight
        PlayerPrefs.SetInt(speed_key, speed_cnt);

        // increment the volatile variable.
        speed_cnt++;

        // Destroy collected sword part.
        Destroy(other.gameObject);
    }

    // handle damage related compomnets.
    void handleDamage(Collider other)
    {
        // Changing material of coefficient of sword power.
        damages[damage_cnt-1].GetComponent<Renderer>().material = enable_mat;

        // changing the name of the upgraded sword.
        damage_name.text = damage_part_sword[ (damage_cnt-1) % 8];

        // Set damage for fight
        PlayerPrefs.SetInt(damage_key, damage_cnt);

        // increment the volatile variable.
        damage_cnt++;

        // Destroy collected sword part.
        Destroy(other.gameObject);
    }

    void handle_points(){
        for(int i = PlayerPrefs.GetInt(global_speed_key), j = 0; i < (PlayerPrefs.GetInt(global_speed_key) + 8); i++, j++)
            speeds[j].GetComponentInChildren<TextMesh>().text = "x" + i;

        for (int i = PlayerPrefs.GetInt(global_damage_key), j = 0; i < (PlayerPrefs.GetInt(global_damage_key) + 8); i++, j++)
            damages[j].GetComponentInChildren<TextMesh>().text = "" + i*10;
    }

    IEnumerator start_fight(Collider other)
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Destroy(other.gameObject);
        fight_object.GetComponent<fight_control_script>().start_fight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
