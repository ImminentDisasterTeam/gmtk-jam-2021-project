using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Digit : MonoBehaviour
{

    public Action<GameObject> onTriggerEnter2D;

    int value;
    public int GetValue() {
        return value;
    }
    
    public void SetValue(int value) {
        this.value = value;
    }

    void Start()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.LoadAll<Sprite>("Sprites/digits")[value];
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            onTriggerEnter2D(gameObject);
        }
    }
}
