using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour
{
    [SerializeField] GameObject blood;
    [SerializeField] GameObject chicken;

    public void ResetChicken() {
        chicken.SetActive(true);
        blood.SetActive(false);
    }

    public void Death() {
        chicken.SetActive(false);
        blood.SetActive(true);
        GameManager.Instance.AddScore();
    }
}
