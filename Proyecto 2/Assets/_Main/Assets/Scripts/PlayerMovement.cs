using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D playerRB;
    private Vector2 moveInput;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized; //Normal velocity, avoids hipotenuse effect.
    }

    private void FixedUpdate()
    {
        //Physics
        playerRB.MovePosition(playerRB.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
