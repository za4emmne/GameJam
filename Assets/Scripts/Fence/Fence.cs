using UnityEngine;

public class Fence : MonoBehaviour
{
    private FenceCreator _creator;

    public void Init(FenceCreator creator)
    {
        _creator = creator;
    }
}
