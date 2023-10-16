using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour, IObjectPlatform
{
    [Header("Componentes")]
    [SerializeField] GameObject particleFire;

    public void ActiveObject() {
        particleFire.SetActive(true);
    }

    public void ResetObject() {
        particleFire.SetActive(false);
    }
}
