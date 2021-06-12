using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Digit : MonoBehaviour
{

    public Action<GameObject> onCollisionEnter2D;

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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            onCollisionEnter2D(gameObject);
        }
    }
}
