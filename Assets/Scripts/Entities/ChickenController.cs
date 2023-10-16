using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChickenController : MonoBehaviour, IObjectPlatform
{
    [SerializeField] GameObject blood;
    [SerializeField] GameObject chicken;

    [SerializeField] PatronEnemyController ObjectPlatformParent;

    private void Start() {

    }

    public void Death() {
        chicken.SetActive(false);
        blood.SetActive(true);
        MusicManager.Instance.PlaySFXSound(SoundEffects.SmashChicken);
        GameManager.Instance.AddScore();

        if (ObjectPlatformParent != null) ObjectPlatformParent.EnableMovement(false);
    }

    public void ResetObject() {
        chicken.SetActive(true);
        blood.SetActive(false);
    }

    public void ActiveObject() { }
}
