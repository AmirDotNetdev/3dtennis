using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    Vector3 initialPosition;
    public string hitter;
    int playerScore;
    int botScore;

    [SerializeField] Text playerScoreText;
    [SerializeField] Text botScoreText;

    public bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        botScore = 0;
        playerScore = 0;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //transform.position = initialPosition;
            GameObject.Find("player").GetComponent<player>().Reset();
            if (isPlaying)
            {
                if (hitter == "player")
                {
                    playerScore++;
                }
                else if (hitter == "bot")
                {
                    botScore++;
                }
                isPlaying = false;
                UpdateScore();

            }
            
        }
        else if (collision.transform.CompareTag("Out"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            //transform.position = initialPosition;
            GameObject.Find("player").GetComponent<player>().Reset();
            if (isPlaying)
            {
                if (hitter == "player")
                {
                    botScore++;
                }
                else if (hitter == "bot")
                {
                    playerScore++;
                }
                isPlaying = false;
                UpdateScore();

            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Out") && isPlaying)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            // transform.position = initialPosition;
            GameObject.Find("player").GetComponent<player>().Reset();
            if (hitter == "player")
            {
                botScore++;
            }else if(hitter == "bot")
            {
                playerScore++;
            }
            isPlaying = false;
            UpdateScore();
        }
    }
    void UpdateScore()
    {
        playerScoreText.text = "Player :" + playerScore;
        botScoreText.text = "Bot :" + botScore;
    }
}
