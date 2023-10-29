using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectGround : MonoBehaviour
{
    [SerializeField] ParticleSystem groundParticle;

    public void PlayParticle() {
        groundParticle.Play();
    }
}
