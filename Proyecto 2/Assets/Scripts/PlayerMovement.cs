using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D playerRB;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized; //Velocidad normal, evita efecto hipotenusa.
    }

    private void FixedUpdate()
    {
        //Fisicas
        playerRB.MovePosition(playerRB.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
