using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScrolling : MonoBehaviour
{
    Material _mat;
    public Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().material;    
    }

    // Update is called once per frame
    void Update()
    {
        _mat.mainTextureOffset += offset * Time.deltaTime;
    }
}
