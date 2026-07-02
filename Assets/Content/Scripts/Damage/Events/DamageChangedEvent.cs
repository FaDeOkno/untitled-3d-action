using UnityEngine;

public record struct DamageChangedEvent(GameObject Source, float CurrentDamage, float MaxDamage)
{
}
