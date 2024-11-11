using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceCreator : SpawnerObject<Fence>
{
    public override void OnGet(Fence spawnObject)
    {
        spawnObject.transform.position = this.transform.position;
        base.OnGet(spawnObject);
    }
}
