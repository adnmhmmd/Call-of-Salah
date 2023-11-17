using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundcheck : MonoBehaviour
{
    MovementLogic logicMovement;
    private void OnTriggerEnter(Collider other){
        // Debug.Log("Touch the Ground");
        logicMovement.groundedchanger();
    }
    // Start is called before the first frame update
    void Start()
    {
        logicMovement = this.GetComponentInParent<MovementLogic>();
    }
}
