using UnityEngine;

public class Player : Unit
{
    [SerializeField] private UseZone _useZone;
    [SerializeField] private HealthBar _healthBar;

    protected override void Start()
    {
        base.Start();
        
        if(_healthBar != null)
            _healthBar.Init(_health, _health);
    }

    public void TryUseSomething()
    {
        _useZone.Target?.Use();
    }
}
