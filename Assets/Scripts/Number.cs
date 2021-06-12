using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Number : MonoBehaviour
{
    List<GameObject> digits;
    [SerializeField] int value;
    
    [SerializeField] GameObject digitPrefab;
    [SerializeField] float digitWidth = 0.5f;

    public Action<GameObject> onDestroy;

    public int GetValue()
    {
        return value;
    }

    private void Destroy()
    {
        // onDestroy(gameObject);
        Destroy(gameObject);
    }

    private void DestroyDigits() {
        foreach (var d in digits) {
            Destroy(d);
        }
        digits = new List<GameObject>();
    }

    public static Number Summ(Number left, Number right)
    {
        int sum = left.GetValue() + right.GetValue();
        left.Initiate(sum);
        Destroy(right.gameObject);
        return left;
    }

    public void Initiate(int value)
    {
        DestroyDigits();
        this.value = value;
        InstantiateDigits();
    }

    private void InstantiateDigits() {
        int v = value;
        float offset = -digitWidth;
        while (v > 0) {
            Debug.Log(v%10);
            GameObject go = Instantiate(digitPrefab, transform.position + new Vector3(offset, 0, 0), Quaternion.identity, transform);
            go.GetComponent<Digit>().SetValue(v%10);
            digits.Add(go);
            go.GetComponent<Digit>().onCollisionEnter2D += obj => Destroy();
            offset -= digitWidth;
            v/=10;
        }
    }

    public float GetWidth() {
        return digitWidth*digits.Count;
    }

    public Vector2 GetBordersX() {
        return new Vector2 (transform.position.x - GetWidth(), transform.position.x);
    }

    void Start()
    {
        digits = new List<GameObject>();
        Initiate(value);
    }

    void Update()
    {

    }
}
