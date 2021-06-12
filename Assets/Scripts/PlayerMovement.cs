using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveOffset;
    public Rigidbody2D rigidbody2;
    float speed;
    public void SetSpeed(float speed) { this.speed = speed; }
    bool isControllable;
    public void SetControl(bool isControllable) { this.isControllable = isControllable; }

    // Update is called once per frame
    void Update()
    {
        moveOffset.x = Input.GetAxisRaw("Horizontal");
        moveOffset.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        if (isControllable) {
            rigidbody2.MovePosition(rigidbody2.position + moveOffset.normalized * (speed * Time.fixedDeltaTime));
        }
    }
}
