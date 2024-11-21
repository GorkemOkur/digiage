using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_script : MonoBehaviour
{
    private Vector3 position_on_sword;

    // It is also possible by getting child counts using transform.childCount. However, this way is more efficient.
    // It will be used as Coefficient of the size of irons(Boxes)
    private byte childs_counts;

    private float hilt_size;

    private float forwardSpeed = 3f;
    private float sidewaysSpeed = 3f;
    private float targetX;
    private bool touched = false;
    private List<GameObject> children = new List<GameObject>();

    // Variables for irons
    private Vector3 startPosition;
    private float pushForce = 5f; // Hareket kuvveti
    private float maxDistance = 2f; // Maksimum mesafe

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi from swordS");

        targetX = transform.position.x;

        // Since it is a coefficient, it starts from 1.
        childs_counts = 1;

        // Since the capsule object was rotated. forward could be represented as y axis of the capsule.
        hilt_size = GetComponent<Renderer>().bounds.size.y;
    }


    public void add_iron(Collider iron){

        if( !iron.transform.parent.CompareTag("sword") ) {

            position_on_sword = transform.position + transform.up * (hilt_size + iron.GetComponent<Renderer>().bounds.size.z * childs_counts);
            position_on_sword.y = iron.transform.position.y;

            iron.transform.position = position_on_sword;
            iron.transform.parent = transform;

            // 
            childs_counts++;

            iron.transform.tag = "sword_part";
            children.Add(iron.gameObject);
        }
    }

    private void OnTriggerEnter(Collider iron)
    {
        if (iron.CompareTag("irons")){

            add_iron(iron);
        }
    }

    public bool isFront(GameObject o){
        Debug.Log("index of object: " + children.IndexOf(o) + "   name: " + o.name);
        Debug.Log("children.Count == children.IndexOf(o)+1:    " + (children.Count == children.IndexOf(o) + 1 ) );

        return children.Count == children.IndexOf(o)+1;
    }

    public void delete_part(GameObject o)
    {
        children.Remove(o);
        childs_counts--;
        Destroy(o);
    }

    public void delete_and_apart(GameObject o) {

        Debug.Log("___::: delete_and_apart :::___");

        List<GameObject> deleted = new List<GameObject>();
        deleted.Add(o);
        childs_counts--;

        int start = children.IndexOf(o);
        int total = children.Count;


        Debug.Log("children.IndexOf(o):   " + start);
        Debug.Log("children.Count:   " + total);
        

        if ( children.IndexOf(o) < children.Count)
        {
            for( int i= start+1; i < total; i++)
            {
                Debug.Log(children[i].name);
                deleted.Add(children[i]);
                startPosition = children[i].transform.position;
                children[i].transform.tag = "irons";
                children[i].transform.parent = GameObject.FindGameObjectWithTag("collectibles").transform;
                StartCoroutine( Move_Until_Distance( children[i].gameObject ) );
                childs_counts--;
            }

            foreach(GameObject del in deleted)
                children.Remove(del);

            
            Debug.Log("After everything COUNT :   " + children.Count);
        }

    }

    private void throw_iron(GameObject o)
    {
        
    }

    private IEnumerator Move_Until_Distance(GameObject o)
    {
        while (Vector3.Distance(startPosition, o.transform.position) < maxDistance)
        {
            o.transform.Translate(Vector3.forward * pushForce * Time.deltaTime);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( touched || Input.touchCount > 0) { 
            transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);
            touched = true;
        }

        // Dokunulduðunda hedef x pozisyonunu güncelle
        if (Input.touchCount > 0 && Input.GetTouch(0).position.y < Screen.height * 0.5f)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, transform.position.z));
            targetX = touchPosition.x;
        }

        // X ekseninde yumuþak geçiþ
        float newX = Mathf.Lerp(transform.position.x, targetX, sidewaysSpeed * Time.deltaTime);
        newX = newX < -0.7f ? -0.7f : (newX > 0.7f ? 0.7f : newX );
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);


    }






}
