using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class me_script : MonoBehaviour
{
    public GameObject position;
    public GameObject me_canvas;
    public GameObject en_canvas;
    public GameObject win_canvas;

    public Slider me_slider;
    public Slider en_slider;

    public TextMeshProUGUI me_speed_text;
    public TextMeshProUGUI me_damge_text;
    public TextMeshProUGUI me_health_text;

    public TextMeshProUGUI en_speed_text;
    public TextMeshProUGUI en_damge_text;
    public TextMeshProUGUI en_health_text;

    private Animator animator;
    private Vector3 pos;

    private string speed_key = "local_speed";
    private string damage_key = "local_damage";
    private short healt = 1000;
    private short speed;
    private short damage;

    private short enemy_healt_level1 = 250;
    private short enemy_speed_level1 = 1;
    private short enemy_damage_level1 = 50;


    private bool in_position = false;
    private bool end = false;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pos = position.transform.position;
        int lvl = PlayerPrefs.GetInt("points");
        float co = 0.024f;

        if (lvl == 1)
            enemy_healt_level1 = 250;
        else
            enemy_healt_level1 = (short)( 250 + (250*lvl*co) );
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!in_position && other.transform.CompareTag("position")) {
            Debug.Log("POSITION COLLISION");
            load_infos();

            Destroy(position.gameObject);
            in_position = true;
        }
        Debug.Log("____________::::::::::      Points:    " + PlayerPrefs.GetInt("points"));
        PlayerPrefs.SetInt("points", PlayerPrefs.GetInt("points"));
        PlayerPrefs.Save();
    }

    private void load_infos()
    {
        transform.position = pos;
        me_canvas.gameObject.SetActive(true);
        en_canvas.gameObject.SetActive(true);

        // Set values ME
        me_health_text.text = "" + healt;
        me_speed_text.text = "" + PlayerPrefs.GetInt(speed_key);
        speed = (short)PlayerPrefs.GetInt(speed_key);
        me_damge_text.text  = "" + PlayerPrefs.GetInt(damage_key)*10;
        damage = (short) (PlayerPrefs.GetInt(damage_key) * 10);

        // Set values Enemy
        en_health_text.text = "" + enemy_healt_level1;
        en_speed_text.text = "" + enemy_speed_level1;
        en_damge_text.text = "" + enemy_damage_level1;

        me_slider.maxValue = healt;
        en_slider.maxValue = enemy_healt_level1;

        StartCoroutine( start_fight() );
    }

    IEnumerator start_fight()
    {
        while (!end)
        {
            // Check for win condition
            if (enemy_healt_level1 <= 0)
            {
                yield return StartCoroutine(win());
                end = true; // Exit the loop
            }


            yield return StartCoroutine( me_hit() );

            // Check for win condition
            if (enemy_healt_level1 <= 0)
            {
                yield return StartCoroutine(win());
                end = true; // Exit the loop
            }


            yield return StartCoroutine( en_hit() );

            // Check for win condition
            if (enemy_healt_level1 <= 0)
            {
                yield return StartCoroutine( win() );
                end = true; // Exit the loop
            }

            yield return null; // Wait for the next frame
        }
    }

    void update_healts() {
        me_slider.maxValue = healt;
        en_slider.maxValue = enemy_healt_level1;

        me_health_text.text = "" + healt;
        en_health_text.text = "" + enemy_healt_level1;
    }

    IEnumerator me_hit()
    {
        if (hit)
        {
           // transform.position = pos;
            //animator.Play("me_hit_1");
            Debug.Log("Me Hit");
            yield return new WaitForSecondsRealtime(0.25f);
            yield return new WaitForSecondsRealtime(1.5f / (float)speed);

            if (enemy_healt_level1 > damage)
            {
                enemy_healt_level1 -= damage;
                update_healts();
            }
            else
            {
                enemy_healt_level1 -= damage;
                end = true;
            }
            update_healts();
            hit = false;
        }
    }
    
    IEnumerator en_hit()
    {
        //animator.Play("enemy_hit_3");
        yield return new WaitForSecondsRealtime(0.25f);
        yield return new WaitForSecondsRealtime(2.0f / (float)enemy_speed_level1);

        if (healt > enemy_damage_level1)
        {
            healt -= enemy_damage_level1;
            update_healts();
        }
        else
        {
            healt -= enemy_damage_level1;
            end = true;
        }
        update_healts();
    }

    IEnumerator win()
    {
        win_canvas.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        int level;

        if (PlayerPrefs.HasKey("level"))
            level = PlayerPrefs.GetInt("level");
        else
            level = 1;

        level = level+1 < 6 ? level+1 : level;

        PlayerPrefs.SetInt("level", level);

        Debug.Log("____________::::::::::      Points:    " + PlayerPrefs.GetInt("points"));

        PlayerPrefs.Save();

        SceneManager.LoadScene("Scenes/level"+level);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
            hit = true;
    }
}
