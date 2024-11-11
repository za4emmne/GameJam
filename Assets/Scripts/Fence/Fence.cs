using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private FenceCreator _creator;

    public void Init(FenceCreator creator)
    {
        _creator = creator;
    }
}
