using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private int pointMultiplier = 1;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Peg")
        {
            GameController.pegsLeft--;
            GameController.UpdatePoints(pointMultiplier, collision.transform.position);
            pointMultiplier++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            GameController.ballsInScene--;
            GameController.UpdateGameStatus();
            Destroy(this.gameObject);
        }
    }
}