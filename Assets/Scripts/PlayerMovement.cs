using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveOffset;
    public Rigidbody2D rb;
    float speed;
    public void SetSpeed(float speed) { this.speed = speed; }

    // Update is called once per frame
    void Update()
    {
        moveOffset.x = Input.GetAxisRaw("Horizontal");
        moveOffset.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveOffset * speed * Time.fixedDeltaTime);
        Debug.Log(moveOffset);
    }
}
