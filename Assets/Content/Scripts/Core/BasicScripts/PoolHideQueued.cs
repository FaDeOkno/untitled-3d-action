using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PoolHideQueued : FancyBehaviour
{
    [Inject] private IObjectPool _pool = null!;

    private void Update()
    {
        _pool.HideObject(gameObject);
        Destroy(this);
    }
}
