using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }   

    void FixedUpdate()
    {
        var moveH = Input.GetAxisRaw("Horizontal");
        var moveV = Input.GetAxisRaw("Vertical");

        DoMovement(moveH, moveV);
        DoTurning();
        DoAnimation(moveH != 0f || moveV != 0f);
    }

    private void DoAnimation(bool isWalking)
    {
        Debug.Log("IsWalking: " + isWalking.ToString());
        anim.SetBool("IsWalking", isWalking);
    }

    private void DoTurning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0.0f;

            Quaternion rotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(rotation);
        }
    }

    private void DoMovement(float moveH, float moveV)
    {
        movement.Set(moveH, 0.0f, moveV);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }
}
