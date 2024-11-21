using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class iron : MonoBehaviour
{
    public GameObject sword;
    public GameObject points_win;
    public GameObject points_lose;
    public Material lava;
    public Material water;
    public Material silver;

    private byte type;
    private bool showed;
    private Vector3 points_offset;

    public GameObject left;
    public GameObject right;

    bool some=true;
    private List<float> r_p;

    private Vector3 startPosition;
    private float pushForce = 5f; // Hareket kuvveti
    private float maxDistance = 2f; // Maksimum mesafe

    // Start is called before the first frame update
    void Start(){
        type = 0;
        showed = false;
        points_offset = new Vector3(-0.3f, 1.1f, -0.8f);
        if (!PlayerPrefs.HasKey("points"))
            PlayerPrefs.SetInt("points", 0);

        if(some)
        {
            PlayerPrefs.SetInt("points", 0);
            some = false;
        }

        PlayerPrefs.SetInt("cnt", 0);
        r_p = new List<float> {-0.4f, -0.2f, 0f, 0.2f, 0.4f, -0.4f, -0.2f, 0f, 0.2f, 0.4f };
    }

    private void OnTriggerEnter(Collider other)
    {

        if (transform.CompareTag("irons"))
        {
            if(!showed)
                showPoints(points_win);

            PlayerPrefs.SetInt("points", (PlayerPrefs.GetInt("points") + 10));
            sword.GetComponent<sword_script>().add_iron(this.GetComponent<Collider>());
        }
            
        if ( type == 0 && transform.CompareTag("sword_part") && other.transform.CompareTag("lava_point") )
        {
            type = 1;
            showPoints(points_win);
            PlayerPrefs.SetInt("points", (PlayerPrefs.GetInt("points") + 10));
            transform.GetComponent<Renderer>().material = lava;
        }

        if (type == 1 && transform.CompareTag("sword_part") && other.transform.CompareTag("water_point"))
        {
            type = 2;
            showPoints(points_win);
            PlayerPrefs.SetInt("points", (PlayerPrefs.GetInt("points") + 10));
            transform.tag = "sword_part_smithing";
            transform.GetComponent<Renderer>().material = water;
        }

        if( type == 2 && transform.CompareTag("sword_part_smithing") && other.CompareTag("smithing_point") )
        {
            type = 3;
            PlayerPrefs.SetInt("points", (PlayerPrefs.GetInt("points") + 10));
            Vector3 newScale = transform.localScale;

            // Here the tag given from the smithing mashine because of the concurency issues.
            //other.transform.tag = "sword_part_sharpening";
            newScale.y = 0.1f;
            transform.localScale = newScale;
            showPoints(points_win);
        }

        if (type == 3 && transform.CompareTag("sword_part_sharpening") && other.CompareTag("sharpening_point"))
        {
            type = 3;
            showPoints(points_win);
            PlayerPrefs.SetInt("points", (PlayerPrefs.GetInt("points") + 10));
            transform.tag = "sword_part_ready";
            transform.GetComponent<Renderer>().material = silver;
            Instantiate(right, transform);
            Instantiate(left, transform);
        }

        if (other.CompareTag("obstacle") && !transform.CompareTag("irons"))
        {

            if ( sword.GetComponent<sword_script>().isFront(this.gameObject) )
            {
                showPoints(points_lose);
                sword.GetComponent<sword_script>().delete_part(this.gameObject);
            }
            else
            {

                sword.GetComponent<sword_script>().delete_and_apart(this.gameObject);
                StartCoroutine( wait_3_second() );
                
                //Destroy( this.transform.gameObject );
            }

        }

    }

    private IEnumerator wait_3_second()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
    }

    private IEnumerator MoveUntilDistance()
    {
        while (Vector3.Distance(startPosition, transform.position) < maxDistance)
        {
            transform.Translate(Vector3.forward * pushForce * Time.deltaTime);
            yield return null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void showPoints(GameObject points)
    {
        showed = true;
        points_offset.x = r_p[ PlayerPrefs.GetInt("cnt") % r_p.Count];
        PlayerPrefs.SetInt("cnt", (PlayerPrefs.GetInt("cnt")+1));

        if ( (PlayerPrefs.GetInt("cnt") % 2) == 0 )
            points_offset.y = 1.4f;
        else
            points_offset.y = 1.1f;

        Instantiate(points, (sword.transform.position + points_offset) , Quaternion.identity, sword.transform);
    }

}
