using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaChet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RuaBienMat());
    }
    IEnumerator RuaBienMat()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

