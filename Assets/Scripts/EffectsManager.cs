using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager instance;
    private void Start()
    {
        instance = this;
    }
    public HitEffect hitEffect;
}
