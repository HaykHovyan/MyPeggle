using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform shootingPoint;
    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private float shootingStrength;

    void Update()
    {
        if (GameController.gameFinished)
            return;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float zRotation = Quaternion.LookRotation(pivot.position - mousePosition, Vector3.back).eulerAngles.z;
        pivot.rotation = Quaternion.Euler(0, 0, zRotation);
        if (Input.GetMouseButtonDown(0) && GameController.ballsInScene == 0)
        {
            if (GameController.ammoLeft == 0)
            {
                Debug.Log("Out of Ammo");
                return;
            }
            mousePosition.z = 0;
            GameObject ballInstance = Instantiate(ball, shootingPoint.position, new Quaternion());
            ballInstance.GetComponent<Rigidbody2D>().AddForce(shootingStrength * (mousePosition - shootingPoint.position).normalized);
            GameController.ammoLeft--;
            GameController.ballsInScene++;
        }
    }
}