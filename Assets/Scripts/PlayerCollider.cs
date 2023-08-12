using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]PlayerController controller;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag =="Obstaculo") {
            controller.Death();
        }
        if(other.gameObject.tag == "Pollo") {
            other.gameObject.GetComponent<ChickenController>().Death();
        }
    }
}
