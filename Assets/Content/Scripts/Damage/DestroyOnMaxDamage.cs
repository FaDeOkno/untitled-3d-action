using UnityEngine;

/// <summary>
/// This component destroys owner when it reaches max damage
/// </summary>
public class DestroyOnMaxDamage : FancyBehaviour
{
    protected override void InitializeEvents()
    {
        base.InitializeEvents();

        SubscribeLocalEvent<MaxDamageReachedEvent>(OnMaxDamage);
    }

    void OnMaxDamage(ref MaxDamageReachedEvent ev)
    {
        PoolHide(gameObject);
    }
}
