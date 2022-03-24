using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemeState
{
    walk,
    attack,
    run
}

public class Enemy : MonoBehaviour
{
    public EnemeState currentState;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
