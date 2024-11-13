using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    [SerializeField] private FenceCreator _creator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.TryGetComponent<Fence>(out Fence fence)))
        {
            _creator.Release(fence);
        }
    }
}
