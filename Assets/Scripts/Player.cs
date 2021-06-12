using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Number leftHand;
    Number rightHand;

    [SerializeField]
    float speed;

    public float GetSpeed() { return speed; }
    private void SetSpeed(float speed)
    {
        this.speed = speed;
        this.GetComponent<PlayerMovement>().SetSpeed(speed);
    }

    public void Summ()
    {

    }

    public void Collect(Number number, bool left)
    {

    }

    public void Drop(bool left)
    {

    }

    void Die()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(speed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
