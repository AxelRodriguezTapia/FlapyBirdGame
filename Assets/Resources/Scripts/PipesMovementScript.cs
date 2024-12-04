using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesMovementScript : MonoBehaviour
{
    public float velocidad = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
    transform.Translate(Vector2.left * velocidad * Time.deltaTime);
    }

}
