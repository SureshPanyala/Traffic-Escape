using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandPersonElement : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("SwimTrigger"))
        {
            _animator.SetBool("Swim", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("SwimTrigger"))
        {
            _animator.SetBool("Swim", false);
        }
    }
}
