using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    Number number;

    public bool Store(Number newNumber)
    {
        if (number != null)
            return false;
        number = newNumber;
        number.gameObject.transform.parent = transform;
        number.gameObject.transform.localPosition = Vector3.zero;
        return true;
    }
    public Number Get()
    {
        Number tmp = number;
        number.gameObject.transform.parent = null;
        number = null;
        return tmp;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
