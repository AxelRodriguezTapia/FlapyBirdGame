using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sumScore(){
        score++;
        Debug.Log(score);
    }


}
