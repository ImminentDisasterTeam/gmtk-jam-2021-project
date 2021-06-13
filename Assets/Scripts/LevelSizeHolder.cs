using System;
using UnityEngine;

public class LevelSizeHolder : MonoBehaviour {
    Vector2 _levelSize;

    public void SetSize(Vector2 newSize) {
        _levelSize = newSize;
        OnChange?.Invoke(_levelSize);
    }
    
   public Action<Vector2> OnChange;
}
