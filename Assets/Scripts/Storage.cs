using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    Number number;

    public bool Store(Number newNumber)
    {        
        if (number != null || newNumber == null) {
            return false;
        }
        
        number = newNumber;
        number.gameObject.transform.parent = transform;
        number.gameObject.transform.localPosition = Vector3.zero;
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
}
