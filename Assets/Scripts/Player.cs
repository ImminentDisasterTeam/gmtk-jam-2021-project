using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public bool isControllable { get; private set; }
    public Action onDeath;
    Number leftHand;
    Number rightHand;
    bool summarizing;
    float summDelay = 2f;
    float summPause = 1f;
    float width = 1f;
    [SerializeField] Collider2D collider2;
    [SerializeField] GameObject numberObject;
    [SerializeField] float maxSpeed;
    float speed;
    public float GetSpeed() { return speed; }
    private void SetSpeed()
    {
        float weight = leftHand != null ? leftHand.GetValue() : 0 + (rightHand != null ? rightHand.GetValue() : 0);
        weight = weight == 0 ? 1 : weight;
        this.speed = maxSpeed;
        this.GetComponent<PlayerMovement>().SetSpeed(speed);
    }
    void StartSummarizing()
    {
        if (!summarizing)
            return;
        switchControls();
        Invoke(nameof(Summ), summPause);
    }

    void Summ()
    {
        Number.Summ(leftHand, rightHand);
        summarizing = false;
        switchControls();
    }
    void switchControls()
    {
        isControllable = !isControllable;
        this.GetComponent<PlayerMovement>().SetControl(isControllable);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            onDeath();
            Destroy(gameObject);
        }
    }

    public void Collect(ref Number hand, int offset)
    {
        GameObject overlappingObject = GetOverlappingObject();

        if (overlappingObject == null)
            return;

        if (overlappingObject.tag == "Digit" && overlappingObject.transform.parent.parent != this.transform)
        {
            hand = overlappingObject.GetComponentInParent<Number>();
        }

        if (overlappingObject.tag == "Storage")
        {
            hand = overlappingObject.GetComponentInParent<Storage>().Get();
        }

        if (hand == null)
            return;
        hand.gameObject.transform.SetParent(this.transform);
        hand.gameObject.transform.localPosition = new Vector3(width + offset * hand.GetWidth(), 0, 0);

        SetSpeed();
    }

    public void Drop(ref Number hand)
    {
        GameObject storageObject = GetOverlappingObject();
        if (storageObject != null && storageObject.tag == "Storage")
        {
            Storage storage = storageObject.GetComponent<Storage>();
            storage.Store(hand);
            hand = null;
        }
        else
        {
            hand.gameObject.transform.parent = null;
            hand = null;
        }
        summarizing = false;
        SetSpeed();
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
        SetSpeed();
        switchControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (isControllable)
        {
            if (Input.GetButtonDown("LeftHand"))
                Interact(ref leftHand, -1);

            if (Input.GetButtonDown("RightHand"))
                Interact(ref rightHand, 1);

            if (!summarizing && leftHand != null && rightHand != null)
            {
                summarizing = true;
                //start animation
                Invoke(nameof(StartSummarizing), summDelay);
            }
        }
    }
}
