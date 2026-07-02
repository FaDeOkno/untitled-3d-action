using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This component defines game object health
/// </summary>
public class Damageable : FancyBehaviour
{
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    private float _damage = 0f;

    public void TryChangeDamage(float damage)
    {
        _damage += damage;

        var ev = new DamageChangedEvent(gameObject, _damage, _health);
        RaiseLocalEvent(ref ev, true);

        if (_damage >= _health)
        {
            var maxEv = new MaxDamageReachedEvent(gameObject);
            RaiseLocalEvent(ref maxEv, true);
        }
    }
}
