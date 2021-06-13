using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Digit : MonoBehaviour
{

    public Action<GameObject> onEnemyEnter;

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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            onEnemyEnter(gameObject);
            GameObject grandparent = transform.parent.parent.gameObject;
            if(grandparent !=null && grandparent.CompareTag("Player"))
            {
                grandparent.GetComponent<Player>().StopSumm();
                grandparent.GetComponent<Player>().AllowMonement();
            }

        }
    }
}
