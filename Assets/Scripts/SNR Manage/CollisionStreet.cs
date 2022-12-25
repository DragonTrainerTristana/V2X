using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionStreet : MonoBehaviour
{

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Street")
        {
            //pos = collision.gameObject.transform.position;
            //Debug.Log(pos.x + pos.z + "\n");
            //Destroy(collision.gameObject);
           
            Debug.Log("Ãæµ¹");
            Destroy(this.gameObject);

        }
    }
}
