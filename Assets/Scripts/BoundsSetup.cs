using System;
using System.Linq;
using UnityEngine;

public class BoundsSetup : MonoBehaviour {
    [SerializeField] LevelSizeHolder levelSizeHolder;
    const float DefaultSize = 1;

    void Awake() {
        levelSizeHolder.OnChange += OnChangeSize;
    }

    void OnChangeSize(Vector2 levelSize) {
        GetComponents<BoxCollider2D>().ToList().ForEach(Destroy);
        var horizontalOffset = levelSize.x / 2 + DefaultSize / 2;
        var verticalOffset = levelSize.y / 2 + DefaultSize / 2;
        
        var up = gameObject.AddComponent<BoxCollider2D>();
        var down = gameObject.AddComponent<BoxCollider2D>();
        up.size = down.size = new Vector2(levelSize.x, DefaultSize);
        up.offset = new Vector2(0, verticalOffset);
        down.offset = new Vector2(0, -verticalOffset);
        
        var left = gameObject.AddComponent<BoxCollider2D>();
        var right = gameObject.AddComponent<BoxCollider2D>();
        left.size = right.size = new Vector2(DefaultSize, levelSize.y);
        left.offset = new Vector2(horizontalOffset, 0);
        right.offset = new Vector2(-horizontalOffset, 0);
    }
}
