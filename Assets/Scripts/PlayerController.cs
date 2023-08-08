using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ParticleDeath;
    [SerializeField] GameObject PumkinGO;
    public void Death() {
        PumkinGO.SetActive(false);
        ParticleDeath.SetActive(true);
    }

    public void SpawnPlayer() {

    }
}
