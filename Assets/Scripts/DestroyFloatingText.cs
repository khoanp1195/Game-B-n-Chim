using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloatingText : MonoBehaviour
{
    public float destroytime = 3f;
    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, destroytime);
    }
}
