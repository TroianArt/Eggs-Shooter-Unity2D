using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
public class bot : MonoBehaviour
{
    bool is_ground = false;
    public GameObject hero;
    public int HP = 3;
    public float force = 2;
    Rigidbody2D rb;
    private bool seeRight = true;
    public Rigidbody2D star;
    public Rigidbody2D star2;
    float run;
    public float starRate = 2f;
    float nextstar = 0.0f;
    Animator anim;
    bool active = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (hero != null)
            {
                if (Mathf.Round(hero.transform.position.y) == Mathf.Round(transform.position.y) && Time.time > nextstar &&
                    ((Mathf.Round(hero.transform.position.x) > Mathf.Round(transform.position.x - 7f)) || (Mathf.Round(hero.transform.position.x) < Mathf.Round(transform.position.x + 7f))))
                {
                    nextstar = Time.time + starRate;
                    if (seeRight) Instantiate(star2, new Vector3(transform.position.x + 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity);
                    else if (!seeRight) Instantiate(star, new Vector3(transform.position.x - 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity);


                }

                if (Mathf.Round(hero.transform.position.x) < Mathf.Round(transform.position.x - 5f))
                {
                    run = -2f;
                }

                else if (Mathf.Round(hero.transform.position.x) > Mathf.Round(transform.position.x + 5f))
                {
                    run = 2f;

                }

                else { run = 0f; }
            }
            if (run == 2f || run == -2f)
            {
                anim.SetInteger("State", 1);
            }
            else if (run == 0f)
            {
                anim.SetInteger("State", 0);
            }
            rb.velocity = new Vector2(run, rb.velocity.y);

            if (transform.position.y > 30 || transform.position.y < -10)
            {
                transform.position = new Vector2(0, 0);

            }
        }
    }
    void FixedUpdate()
    {
        if (active)
        {
            if (hero != null)
            {
                //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 7f, rb.velocity.y);
                if (hero.transform.position.x > transform.position.x && !seeRight) flip();
                if (hero.transform.position.x < transform.position.x && seeRight) flip();
            }
        }

    }
    void Jump()
    {

        rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (active)
        {//если в тригере что то есть и у обьекта тег "ground"
            if (col.tag == "ground") is_ground = true;
            if (col.tag == "bullet")
            {
                star temp;
                star2 temp2;
                if (col.gameObject.GetComponent<star>() == null)
                {
                     temp2 = col.gameObject.GetComponent<star2>();
                    if (!temp2.ForEnemy)
                    {
                        HP -= 1;
                        //anim.SetInteger("State", 2);
                        anim.Play("ShootBullet");
                        if (HP < 1)
                        {
                            Destroy(gameObject);

                        }
                    }
                }
                else
                {
                    temp = col.gameObject.GetComponent<star>();
                    if (!temp.ForEnemy)
                    {
                        HP -= 1;
                        //anim.SetInteger("State", 2);
                        anim.Play("ShootBullet");
                        if (HP < 1)
                        {
                            Destroy(gameObject);

                        }
                    }
                }


            }
        }
            if (col.tag == "MainCamera") active = true;
        
    }
    void OnTriggerExit2D(Collider2D col)
    {              //если из триггера что то вышло и у обьекта тег "ground"
        if (col.tag == "ground") is_ground = false;     //то вЫключаем переменную "на земле"
    }
    void flip()
    {
        seeRight = !seeRight;
        transform.Rotate(Vector2.up * 180);
    }
}
