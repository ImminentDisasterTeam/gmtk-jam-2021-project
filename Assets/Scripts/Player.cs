using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Number leftHand;
    Number rightHand;
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

    }
    void Die()
    {

    }

    public void Collect(ref Number hand)
    {
        GameObject digitObj = GetOverlappingObject();
        if (digitObj != null && digitObj.tag == "Digit" && digitObj.transform.parent.parent != this.transform)
        {
            hand = digitObj.GetComponentInParent<Number>();
            digitObj.transform.parent.SetParent(this.transform);
            Debug.Log(leftHand);
            Debug.Log(rightHand);
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

    void Interact(ref Number hand)
    {
        if (hand == null)
        {
            Collect(ref hand);
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
            Interact(ref leftHand);

        if (Input.GetButtonDown("RightHand"))
            Interact(ref rightHand);
    }
}
