using System;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviour {
    [SerializeField] Vector2 triggerOffset;
    public Action<int> OnStore;
    Number number;
    BoxCollider2D _collider;
    BoxCollider2D _trigger;
    SpriteRenderer _renderer;
    Vector2 _lastSize;

    public bool Store(Number newNumber)
    {        
        if (number != null || newNumber == null) {
            return false;
        }
        
        number = newNumber;
        number.transform.parent = transform;
        number.transform.localPosition = Vector3.zero;
        
        OnStore?.Invoke(number.GetValue());
        return true;
    }
    public Number Get()
    {
        if (number == null) {
            return null;
        }
        
        var tmp = number;
        number.gameObject.transform.parent = null;
        number = null;
        return tmp;        
    }

    void Awake() {
        _collider = GetComponents<BoxCollider2D>().First(c => !c.isTrigger);
        _trigger = GetComponents<BoxCollider2D>().First(c => c.isTrigger);
        _renderer = GetComponent<SpriteRenderer>();
        UpdateSizes(_renderer.size);
    }

    void Update() {
        if (_renderer.size != _lastSize) {
            UpdateSizes(_renderer.size);
        }
    }

    void UpdateSizes(Vector2 newSize) {
        _lastSize = newSize;
        _collider.size = _lastSize;
        _trigger.size = _lastSize + triggerOffset;
    }
}
