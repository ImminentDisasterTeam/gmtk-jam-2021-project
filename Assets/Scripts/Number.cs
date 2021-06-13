using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Number : MonoBehaviour
{
    List<GameObject> digits = new List<GameObject>();
    [SerializeField] int value;
    
    [SerializeField] GameObject digitPrefab;
    [SerializeField] float digitWidth = 0.5f;
    [SerializeField] GameObject destructionPrefab;

    public Action<GameObject> onDestroy;
    public Transform mapObject;

    public int GetValue()
    {
        return value;
    }

    private void Destroy()
    {
        // onDestroy(gameObject);
        var destructionAnimation = Instantiate(destructionPrefab, transform.position, Quaternion.identity);
        destructionAnimation.transform.parent = mapObject;
        Destroy(gameObject);
    }

    private void DestroyDigits() {
        foreach (var d in digits) {
            Destroy(d);
        }
        digits = new List<GameObject>();
    }

    public static void Summ(Number left, Number right)
    {
        int sum = left.GetValue() + right.GetValue();
        left.Initiate(sum);
        Destroy(right.gameObject);
    }

    public void Initiate(int value)
    {
        DestroyDigits();
        this.value = value;
        InstantiateDigits();
    }

    private void InstantiateDigits() {
        int v = value;
        // float offset = -digitWidth;

        var digitCount = v.ToString().Length;
        var offset = (digitCount - 1) / 2f * digitWidth;
        
        while (v > 0) {
            GameObject go = Instantiate(digitPrefab, transform.position + new Vector3(offset, 0, 0), Quaternion.identity, transform);
            go.GetComponent<Digit>().SetValue(v%10);
            digits.Add(go);
            go.GetComponent<Digit>().onEnemyEnter += obj => Destroy();
            offset -= digitWidth;
            v/=10;
        }
    }

    public float GetWidth() {
        return digitWidth * digits.Count;
    }

    public Vector2 GetBordersX() {
        return new Vector2 (transform.position.x - GetWidth(), transform.position.x);
    }
}
