using UnityEngine;

public class FenceMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
    }
}
