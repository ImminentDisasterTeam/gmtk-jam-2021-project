using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    Number number;

    public bool Store(Number newNumber)
    {        
        if (number != null || newNumber == null) {
            if (number != null) Debug.Log("storage full");
            if (newNumber == null) Debug.Log("nothing to store");
            
            return false;
        }
        
        Debug.Log("store something");
        number = newNumber;
        
        number.gameObject.transform.parent = transform;
        number.gameObject.transform.localPosition = Vector3.zero;
        return true;
    }
    public Number Get()
    {
        if (number == null) {
            Debug.Log("nothing to get");
            return null;
        }
        
        Debug.Log("get smth");
        var tmp = number;
        number.gameObject.transform.parent = null;
        number = null;
        return tmp;        
    }
}
