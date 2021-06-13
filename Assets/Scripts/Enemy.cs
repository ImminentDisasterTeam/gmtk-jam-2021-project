using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    [Range(50f, 600f)] float speed = 400;

    [SerializeField] float deltaAngle;

    bool _setuped;
    Rigidbody2D _rb;
    Animator _animator;

    public void Setup() {
        // ???: start
        var direction = Random.Range(0.2f, 0.8f);
        var sum = Mathf.Sqrt(1 / (direction * direction + (1 - direction) * (1 - direction)));
        var x = direction * sum;
        var y = (1 - direction) * sum;
        // ???: finish
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(x, y) * speed);

        var collider = GetComponent<Collider2D>();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), collider);
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        _rb.velocity = Quaternion.Euler(0, 0, Random.Range(-deltaAngle / 2, deltaAngle / 2)) * _rb.velocity;
    }
}
