using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronCollider : MonoBehaviour
{
    [SerializeField] PatronController patronController;


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag=="end")
            patronController.SetEnablePatron(false);
        if (other.gameObject.tag == "activeEvent")
            patronController.ActiveAllCandle();
    }
}
