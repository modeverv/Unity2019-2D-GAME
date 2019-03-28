using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    // スコア 
    int score;

    public UnityEngine.UI.Text scoreValue;
    public AudioClip[] sounds;
    public bool isStop;
    public SpriteRenderer nekobako;
    public Sprite[] nekobakoImages;

    // 落下速度 
    float downSpeed;
    // 物理演算コンポーネント
    Rigidbody2D rb;
    Animator animCtlr;
    AudioSource audioSource; // audio


    // Start is called before the first frame update
    void Start()
    {
        // 初期化処理
        rb = GetComponent<Rigidbody2D>();
        animCtlr = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        var forwardSpeed = 1;
        if (isStop)
        {
            forwardSpeed = 0;
            animCtlr.SetBool("isIdle", true);
            //downSpeed = 0;
            //return; 
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + new Vector3(-0.32f, -0.32f), Vector2.right, 0.62f);
        if (null != hit.transform)
        {
            downSpeed = 0;
            animCtlr.SetBool("isGraund", true);
            if (Input.GetButtonDown("Jump") && !isStop)
            {
                downSpeed = 6.5f;
                transform.Translate(Vector3.up * 0.01f);
                if(!isStop)
                {
                    audioSource.PlayOneShot(sounds[0]);
                }

            }
        }
        else
        {
            animCtlr.SetBool("isGraund", false);
            downSpeed += -0.3f;
        }

        hit = Physics2D.Raycast(transform.position + new Vector3(-0.32f, 0.32f), Vector2.right, 0.64f);
        if (null != hit.transform)
        {
            downSpeed += -0.1f;
            transform.position += new Vector3(0, -0.1f, 0);
        }

        hit = Physics2D.Raycast(transform.position + new Vector3(0.34f, 0.26f), Vector2.down, 0.52f);
        if(null != hit.transform || isStop)
        {
            //当たった
            animCtlr.SetBool("isIdle", true);
        }
        else
        {
            animCtlr.SetBool("isIdle", false);
        }


        // 現在
        Vector2 nowpos = rb.position;
        nowpos += new Vector2( forwardSpeed, downSpeed) * Time.deltaTime;
        rb.MovePosition(nowpos);
        animCtlr.SetFloat("DownSpeed", downSpeed);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        score += 1;
        scoreValue.text = score.ToString();
        audioSource.PlayOneShot(sounds[1]);
    }

    public void Reset()
    {
        downSpeed = 0;
        score = 0;
        scoreValue.text = score.ToString();
        isStop = true;
        //animCtlr.SetBool("isIdle", true);
        this.transform.position = new Vector3(-1.17f, -1.46f, 0);
        //this.transform.position = new Vector3(12.17f, -1.46f, 0);
        animCtlr.SetTrigger("demoEnd");
        nekobako.sprite = nekobakoImages[0];
    }

    public void DemoStart()
    {
        animCtlr.SetTrigger("demoStart");
    }

    public void playSound()
    {
        audioSource.PlayOneShot(sounds[0]);
    }

    void ChangeNekobako()
    {
        nekobako.sprite = nekobakoImages[1];
    }

}
