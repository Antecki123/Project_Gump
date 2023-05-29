using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IKillable killable))
        {
            print(killable);
        }
    }
}