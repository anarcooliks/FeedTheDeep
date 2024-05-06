using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyArea : MonoBehaviour
{
    private GameObject parent;
    // Start is called before the first frame update
    void Awake()
    {
        parent = transform.parent.gameObject;
    }

     void OnTriggerEnter(Collider col){
        Debug.Log(col.name);
            Renderer renderer =  parent.transform.gameObject.GetComponent<Renderer>();
            renderer.material.color = new Color(renderer.material.color.r,
                    renderer.material.color.g, renderer.material.color.b,
                    0.25f);      
    }

    void OnTriggerExit(Collider col){
        Renderer renderer =  parent.transform.gameObject.GetComponent<Renderer>();
        renderer.material.color = new Color(renderer.material.color.r,
                    renderer.material.color.g, renderer.material.color.b,
                    1.0f);
    }
}
