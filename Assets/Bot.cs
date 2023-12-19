using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    
    float speed = 4;
   
    Animator animator;
   
    public Transform ball;

    bool isPlaying;
    public Transform[] targets;
   
    Vector3 targetPosition;

    ShotManager shotManager;

    
    
    float forced = 13;
   
    void Start()
    {
        targetPosition = transform.position;   
        animator = GetComponent<Animator>();
        shotManager = GetComponent<ShotManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (ball.GetComponent<Ball>().isPlaying == true)
        {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        }
    }
    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    Shot PickShot()
    {
        int randomValue = Random.Range(0, 1);
        if (randomValue == 0)
            return shotManager.topSpin;
        else
            return shotManager.flat;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            Shot currentShot = PickShot();
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
                animator.Play("forhand");
            else
                animator.Play("backend");
            ball.GetComponent<Ball>().hitter = "bot";
        }
        
    }
}
