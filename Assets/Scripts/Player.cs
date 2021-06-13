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
    public Action<int> SummReplic;
    public float GetSpeed() { return speed; }

    public void StopSumm()
    {
        if (_summarizingCoro != null)
        {
            StopCoroutine(_summarizingCoro);
        }
        summarizing = false;
    }
    void SetLeftHand(Number hand)
    {
        leftHand = hand;
        StopSumm();
        SetSpeed();
    }
    void SetRightHand(Number hand)
    {
        rightHand = hand;
        StopSumm();
        SetSpeed();
    }

    Coroutine _summarizingCoro;

    private void SetSpeed()
    {
        float weight = leftHand != null ? leftHand.GetValue() : 0 + (rightHand != null ? rightHand.GetValue() : 0);
        weight = weight == 0 ? 1 : weight;
        speed = maxSpeed;
        GetComponent<PlayerMovement>().SetSpeed(speed);
    }

    IEnumerator Summ()
    {
        yield return new WaitForSeconds(summDelay);
        switchControls();
        yield return new WaitForSeconds(summPause);

        Number.Summ(leftHand, rightHand);
        SummReplic(leftHand.GetValue());

        SetHandPosition(leftHand, -1);
        rightHand = null;
        summarizing = false;
        switchControls();
    }
    void switchControls()
    {
        isControllable = !isControllable;
        this.GetComponent<PlayerMovement>().SetControl(isControllable);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            onDeath();
            Destroy(gameObject);
        }
    }

    public void SetHandPosition(Number hand, int direction)
    {
        hand.gameObject.transform.localPosition =
            new Vector3(direction * width * 0.75f + direction * hand.GetWidth() / 2, 0, 0);
    }

    Number Collect(ref Number hand, int direction)
    {
        Number result = hand;
        GameObject overlappingObject = GetOverlappingObject();

        if (overlappingObject == null)
            return result;

        if (overlappingObject.CompareTag("Digit") && overlappingObject.transform.parent.parent != transform)
        {
            result = overlappingObject.GetComponentInParent<Number>();
        }

        if (overlappingObject.CompareTag("Storage"))
        {
            result = overlappingObject.GetComponentInParent<Storage>().Get();
        }

        if (result != null)
        {
            result.gameObject.transform.SetParent(transform);
            SetHandPosition(result, direction);
        }

        return result;
    }

    Number Drop(ref Number hand)
    {
        Number result = hand;

        var storageObject = GetOverlappingObject();
        if (storageObject != null && storageObject.CompareTag("Storage"))
        {
            var storage = storageObject.GetComponent<Storage>();
            if (storage.Store(result))
                result = null;
        }
        else
        {
            result.gameObject.transform.parent = transform.parent;
            result = null;
        }
        return result;
    }

    GameObject GetOverlappingObject()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        List<Collider2D> colliders = new List<Collider2D>();
        var hits = collider2.OverlapCollider(contactFilter.NoFilter(), colliders);
        for (var i = 0; i < hits; i++)
        {
            var obj = colliders[i].gameObject;
            if (obj.CompareTag("Storage") || obj.CompareTag("Digit"))
            {
                return obj;
            }
        }

        return null;
    }

    Number Interact(ref Number hand, int direction)
    {
        if (hand == null)
        {
            return Collect(ref hand, direction);
        }
        return Drop(ref hand);
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
                SetLeftHand(Interact(ref leftHand, -1));

            if (Input.GetButtonDown("RightHand"))
                SetRightHand(Interact(ref rightHand, 1));

            if (!summarizing && leftHand != null && rightHand != null)
            {
                summarizing = true;
                //start animation
                // Invoke(nameof(StartSummarizing), summDelay);
                _summarizingCoro = StartCoroutine(Summ());
                // TODO: fix summ if numbers have been erased
            }
        }
    }
}
