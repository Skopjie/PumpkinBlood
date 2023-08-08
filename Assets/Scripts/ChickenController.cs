using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] GameObject blood;
    [SerializeField] GameObject chicken;

    private void Start() {
        //Death();
    }

    public void Death() {
        chicken.SetActive(false);
        blood.SetActive(true);
    }
}
