using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Range(50f, 600f)] float speed = 400;
    float direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0.2f, 0.8f);
        var sum = Mathf.Sqrt(speed*speed / (direction*direction + (1-direction)*(1-direction)));
        var x = direction*sum;
        var y = (1-direction)*sum;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
