using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    float speed = 3f;
    public Transform aimTarget;
    bool hitting;
    public Transform bot;
    public Transform ball;
    Vector3 aimTargetInitialPosition;
    Animator animator;
    ShotManager shotManager;
    Shot curruntShot;
    bool isPlaying;

    public Transform serveLeftBot;
    public Transform serveRightBot;

    [SerializeField] public Transform serveRight;
    [SerializeField] public Transform serveLeft;

    bool isServeRight = true;

    // Start is called before the first frame update
    void Start()
    {
        animator  = GetComponent<Animator>();
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        curruntShot = shotManager.topSpin;
        ball.GetComponent<Ball>().isPlaying = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F) && ball.GetComponent<Ball>().isPlaying)
        {
            hitting = true;
            curruntShot = shotManager.topSpin;
        }else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && ball.GetComponent<Ball>().isPlaying)
        {
            hitting = true;
            curruntShot = shotManager.flat;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && !ball.GetComponent<Ball>().isPlaying)
        {
            hitting = true;
            curruntShot = shotManager.flatServe;
            GetComponent<BoxCollider>().enabled = false;
            animator.Play("prepare-serve");
        }
        

        if (Input.GetKeyDown(KeyCode.T) && !ball.GetComponent<Ball>().isPlaying)
        {
            hitting = true;
            curruntShot = shotManager.kickServe;
            GetComponent<BoxCollider>().enabled = false;
            animator.Play("prepare-serve");
        }
        if ((Input.GetKeyUp(KeyCode.T) || Input.GetKeyUp(KeyCode.R)) && !ball.GetComponent<Ball>().isPlaying)
        {
            hitting = false;
            
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.2f, 1, 0);
            Vector3 dir = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * curruntShot.hitForce + new Vector3(0, curruntShot.upForce, 0);
            animator.Play("serve");
            
            ball.GetComponent<Ball>().hitter = "player";
            ball.GetComponent<Ball>().isPlaying = true;
            



        }
        



        
        




        if (hitting)
        {
            aimTarget.Translate(new Vector3(h, 0, v) * speed * 2 * Time.deltaTime);
        }

        if ((h != 0 || v != 0) && !hitting)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }


        
    }
    public void Reset()
    {
        if (isServeRight)
        {
            transform.position = serveLeft.position;
            bot.transform.position = serveLeftBot.position;

        }

        else
        {
            transform.position = serveRight.position;
            bot.transform.position = serveRightBot.position;
        }
            isServeRight = !isServeRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * curruntShot.hitForce + new Vector3(0 , curruntShot.upForce, 0);
            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
                animator.Play("forhand");
            else
                animator.Play("backend");
        }
        ball.GetComponent<Ball>().hitter = "player";
        aimTarget.position = aimTargetInitialPosition;
    }
}
