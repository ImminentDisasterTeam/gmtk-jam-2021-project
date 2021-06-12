using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Number leftHand;
    Number rightHand;

    float width = 1f;
    [SerializeField] Collider2D collider2;
    [SerializeField] GameObject numberObject;
    [SerializeField] float speed;
    public float GetSpeed() { return speed; }
    private void SetSpeed(float speed)
    {
        this.speed = speed;
        this.GetComponent<PlayerMovement>().SetSpeed(speed);
    }

    public void Summ()
    {
        Number.Summ(leftHand, rightHand);
    }
    void Die()
    {

    }

    public void Collect(ref Number hand, int offset)
    {
        GameObject digitObject = GetOverlappingObject();
        if (digitObject != null && digitObject.tag == "Digit" && digitObject.transform.parent.parent != this.transform)
        {
            hand = digitObject.GetComponentInParent<Number>();
            digitObject.transform.parent.SetParent(this.transform);

            digitObject.transform.parent.transform.localPosition = new Vector3(width + offset * hand.GetWidth(), 0, 0);


            Debug.Log(transform.localPosition);
            Debug.Log(digitObject.transform.parent.transform.localPosition);
        }

    }

    public void Drop(ref Number hand)
    {
        Debug.Log("drop");
        hand.gameObject.transform.parent = null;
        hand = null;
    }

    GameObject GetOverlappingObject()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        List<Collider2D> colliders = new List<Collider2D>();
        if (collider2.OverlapCollider(contactFilter.NoFilter(), colliders) > 0)
        {
            return colliders[0].gameObject;
        }
        return null;
    }

    void Interact(ref Number hand, int offset)
    {
        if (hand == null)
        {
            Collect(ref hand, offset);
        }
        else
            Drop(ref hand);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LeftHand"))
            Interact(ref leftHand, -1);

        if (Input.GetButtonDown("RightHand"))
            Interact(ref rightHand, 1);

        if (Input.GetKeyDown("space"))
            Summ();
    }
}
