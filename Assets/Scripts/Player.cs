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

    Coroutine _summarizingCoro;
    
    private void SetSpeed()
    {
        float weight = leftHand != null ? leftHand.GetValue() : 0 + (rightHand != null ? rightHand.GetValue() : 0);
        weight = weight == 0 ? 1 : weight;
        speed = maxSpeed;
        GetComponent<PlayerMovement>().SetSpeed(speed);
    }

    IEnumerator Summ() {
        yield return new WaitForSeconds(summDelay);
        switchControls();
        yield return new WaitForSeconds(summPause);
        Number.Summ(leftHand, rightHand);
        rightHand = null;
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
        if (other.gameObject.CompareTag("Enemy"))
        {
            onDeath();
            Destroy(gameObject);
        }
    }

    public void Collect(ref Number hand, int direction)
    {
        GameObject overlappingObject = GetOverlappingObject();

        if (overlappingObject == null)
            return;

        if (overlappingObject.CompareTag("Digit") && overlappingObject.transform.parent.parent != transform)
        {
            hand = overlappingObject.GetComponentInParent<Number>();
        }

        if (overlappingObject.CompareTag("Storage"))
        {
            hand = overlappingObject.GetComponentInParent<Storage>().Get();
        }

        if (hand != null) {
            hand.gameObject.transform.SetParent(transform);
            hand.gameObject.transform.localPosition =
                new Vector3(direction * width * 0.75f + direction * hand.GetWidth() / 2, 0, 0);
        }
        
        SetSpeed();
    }

    void Drop(ref Number hand)
    {
        if (_summarizingCoro != null) {
            StopCoroutine(_summarizingCoro);
        }
        summarizing = false;
        
        var storageObject = GetOverlappingObject();
        if (storageObject != null && storageObject.CompareTag("Storage"))
        {
            var storage = storageObject.GetComponent<Storage>();
            if (storage.Store(hand))
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

    void Interact(ref Number hand, int direction)
    {
        if (hand == null)
        {
            Collect(ref hand, direction);
        }
        else {
            Drop(ref hand);
        }
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
                // Invoke(nameof(StartSummarizing), summDelay);
                _summarizingCoro = StartCoroutine(Summ());
            }
        }
    }
}
