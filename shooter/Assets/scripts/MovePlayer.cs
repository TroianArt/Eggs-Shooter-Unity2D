using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    bool is_ground = false;
    public float force = 10;
    Rigidbody2D rb;
    Animator anim;
    private bool seeRight = true;
    public Rigidbody2D star;
    public Rigidbody2D star2;
    public GameObject rightbut;
    public GameObject leftbut;
    public GameObject jumpbut;
    public GameObject starbut;
    public GameObject slowmobut;
    public GameObject PSfire;
    public int HP = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    float posbutR;
    float posbutL;
    float posbutJ;
    float posbutS;
    float posbutSlow;
    float position;
    private float fixedDeltaTime;
    float run;
    public float starRate = 0.1f;
    float nextstar = 0.0f;

    bool isEcho = false;
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echo;
    bool fixBag=true;
    bool fixBag2 = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        posbutL = leftbut.transform.position.y;
        posbutR = rightbut.transform.position.y;
        posbutJ= jumpbut.transform.position.y;
        posbutS = starbut.transform.position.y;
        posbutSlow = slowmobut.transform.position.y;
        position = transform.position.x;

    }
    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        

        if (position != transform.position.x)
        {
            anim.SetInteger("State", 0);
        }
 
        if (isEcho)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject temp;
                if (seeRight)
                {
                    temp=Instantiate(echo, transform.position, Quaternion.identity);
                }
                else
                {
                    temp = Instantiate(echo, transform.position, Quaternion.identity);
                    temp.transform.Rotate(0.0f, 180, 0.0f);
                }
                Destroy(temp, 3);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }


        if (Mathf.Round(posbutJ) != Mathf.Round(jumpbut.transform.position.y) && is_ground && fixBag) 
        {
            anim.SetInteger("State", 2);
            Jump();
            fixBag = false;
            Invoke("ChangeFix", 0.1f);
            

        }
       

        if (Mathf.Round(posbutSlow) != Mathf.Round(slowmobut.transform.position.y) && fixBag2)
        {
      
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.5f;
                isEcho = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                isEcho = false;
            }
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            fixBag2 = false;
            Invoke("ChangeFix2", 0.1f);


        }
        
        if (Mathf.Round(posbutS) != Mathf.Round(starbut.transform.position.y)&& Time.time>nextstar)
        {
            anim.SetInteger("State", 1);
            nextstar = Time.time+starRate;
            if (seeRight)
            {
                Instantiate(PSfire, new Vector3(transform.position.x + 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity).transform.Rotate(0,0,-90);
                Instantiate(star2, new Vector3(transform.position.x + 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity);
            }
            else if (!seeRight) {
                Instantiate(PSfire, new Vector3(transform.position.x - 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity).transform.Rotate(0,0,90);
                Instantiate(star, new Vector3(transform.position.x - 1.2f, transform.position.y - 0.1f, -1.2f), Quaternion.identity);
                
            }


        }

        if (Mathf.Round(posbutL) != Mathf.Round(leftbut.transform.position.y))
        {
            anim.SetInteger("State", 3);
            run = -5f;
            if(seeRight) flip();
        }

        else if (Mathf.Round(posbutR) != Mathf.Round(rightbut.transform.position.y))
        {
            anim.SetInteger("State", 3);
            run = 5f;
            if (!seeRight) flip();
  
        }

        else { run = 0f;}

        rb.velocity = new Vector2(run, rb.velocity.y);

        if (is_ground && (Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }
        if(transform.position.y>30 || transform.position.y < -10)
        {
            transform.position = new Vector2(0, 0);

        }
    }
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 7f, rb.velocity.y);
        if (move > 0 && !seeRight) flip();
        if (move < 0 && seeRight) flip();


    }
    void Jump()
    {

        rb.AddForce(Vector3.up * force, ForceMode2D.Impulse);
    }
    void ChangeFix()
    {
        fixBag = !fixBag;
    }
    void ChangeFix2()
    {
        fixBag2 = !fixBag2;
    }

    void OnTriggerStay2D(Collider2D col)
    {               //если в тригере что то есть и у обьекта тег "ground"
        if (col.tag == "ground") is_ground = true;
        if (col.tag == "bullet")
        {
            Debug.Log("-1");
            Debug.Log(HP.ToString());
            HP -= 1;
            if (HP == 2)
            {
                Destroy(heart3);
            }
            else if (HP == 1)
            {
                Destroy(heart2);
            }
            else if (HP < 1)
            {
                Destroy(heart1);
                    Invoke("restart", 2);
                    this.gameObject.SetActive(false);
                }
            //anim.SetInteger("State", 2);
            anim.Play("ShootBullet");

        }
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
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
