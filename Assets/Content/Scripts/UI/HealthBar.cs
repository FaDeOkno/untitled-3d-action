using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : FancyBehaviour
{
    [SerializeField]
    private bool _alwaysActive = false;

    [SerializeField]
    private float _visibleRange = 4f;

    [SerializeField]
    private Damageable _damageable;

    private Slider _slider;


    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    protected override void InitializeEvents()
    {
        base.InitializeEvents();

        SubscribeEvent<DamageChangedEvent>(OnDamageChanged);
    }

    public void OnDamageChanged(ref DamageChangedEvent ev)
    {
        if (ev.Source != _damageable.gameObject)
            return;

        _slider.value = 1 - ev.CurrentDamage / ev.MaxDamage;
    }
}
