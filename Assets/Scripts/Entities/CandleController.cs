using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] GameObject particleFire;

    public void ActiveCandle() {
        particleFire.SetActive(true);
    }
    public void DisactiveCandle() {
        particleFire.SetActive(false);
    }

}
