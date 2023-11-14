using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem hitEffect;
    private void Start()
    {
        hitEffect = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayHitEffect()
    {
         hitEffect.Play();
    }
}
