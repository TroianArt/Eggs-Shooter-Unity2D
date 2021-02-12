using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;
    public GameObject PSboom;
    public GameObject AnimBoom;
    public bool withRotate = true;
    public bool ForEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        if (withRotate)
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, 1.9f);
        }
        
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" || col.tag == "bullet")
        {
            Destroy(gameObject);
            var b = Instantiate(AnimBoom, transform.position, Quaternion.identity);
            var a = Instantiate(PSboom, transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(b, 2f);
        }

        else if (col.tag == "enemy")
        {
            if (!ForEnemy)
            {
                Destroy(gameObject);
                var b = Instantiate(AnimBoom, transform.position, Quaternion.identity);
                var a = Instantiate(PSboom, transform.position, Quaternion.identity);
                Destroy(a, 2f);
                Destroy(b, 2f);
            }
        }
    }

}
