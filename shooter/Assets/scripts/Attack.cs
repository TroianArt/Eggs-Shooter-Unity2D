using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject enemy;
    public GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" )
        {
           var enemy1=Instantiate(enemy, new Vector3(transform.position.x + 5, transform.position.y +6, -1.2f), Quaternion.identity);
            var enemy2 = Instantiate(enemy, new Vector3(transform.position.x + 6, transform.position.y + 6, -1.2f), Quaternion.identity);
            //var enemy3 = Instantiate(enemy, new Vector3(transform.position.x - 5, transform.position.y + 6, -1.2f), Quaternion.identity);
            //var enemy4 = Instantiate(enemy, new Vector3(transform.position.x - 6, transform.position.y + 6, -1.2f), Quaternion.identity);
            Destroy(gameObject);
            enemy1.gameObject.GetComponent<bot>().hero = hero;
            enemy2.gameObject.GetComponent<bot>().hero = hero;
            //enemy3.gameObject.GetComponent<bot>().hero = hero;
            //enemy4.gameObject.GetComponent<bot>().hero = hero;
        }

        
    }
}
