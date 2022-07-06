using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UnitAnimationsHandler))]
public abstract class Unit : MonoBehaviour, IDamageable, IAttacker
{
    [SerializeField] private UnitStateMachine _unitStateMachine;
    [SerializeField] private float _attackZoneSize;
    [SerializeField] protected int _health;
    [SerializeField] protected float _weight = 0;
    
    
    private AttackZone _attackZone;
    private bool _isDied = false;

    protected Weapon Weapon;
    
    public int LookDirection { get; private set; }
    public UnitAnimationsHandler UnitAnimationsHandler { get; protected set; }
    public UnitStamina UnitStamina { get; private set; }
    public UnitStateMachine UnitStateMachine => _unitStateMachine;
    public float AttackZoneSize => _attackZoneSize;
    public float Weight => _weight;
    public float AttackSpeed => Weapon.AttackSpeed;
    public event UnityAction<float> IsHealthChanged;

    private void Awake()
    {
        _attackZone = GetComponentInChildren<AttackZone>();
        UnitAnimationsHandler = GetComponent<UnitAnimationsHandler>();
        UnitStamina = GetComponent<UnitStamina>();
    }

    protected virtual void Start()
    {
        InitEquipment();
        
        CalculateWeight();
    }

    private void InitEquipment()
    {
        Weapon = GetComponentInChildren<Weapon>();
        
        if (Weapon != null)
        {
            Weapon.Init();
            UnitAnimationsHandler.SetWeapon(Weapon.Name);
        }

        _attackZone.SetAttackZone(Weapon != null ? Weapon.AttackRange : 2);
    }

    private void CalculateWeight()
    {
        var equipments = GetComponentsInChildren<Equipment>();

        foreach (var equipment in equipments)
        {
            _weight += equipment.Weight;
        }
    }
    
    private void Die()
    {
        _isDied = true;
        UnitStateMachine.SetState(UnitState.Die);
    }

    public virtual void TakeDamage(int damage, float attackerYPosition)
    {
        if(_isDied) return;
        
        if(!UnitStateMachine.SetState(UnitState.TakeDamage)) return;
        
        var yDifference = transform.position.y - attackerYPosition;
        
        if(yDifference is > 2f or < -2f) return;
        
        float finalDamage = damage;
        
        if (UnitStateMachine.GetStateByName(UnitState.Block).IsEnable)
        {
            finalDamage *= LocalDataManager.Instance.Config.BlockPercent;

            UnitStamina.Spend(LocalDataManager.Instance.Config.BlockStaminaConsume);
        }
        
        _health -= (int)finalDamage;
        
        IsHealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Die();
        }
    }

    public virtual void Attack()
    {
        foreach (var target in _attackZone.Targets)
        {
            target.TakeDamage(Random.Range(Weapon.MinDamage, Weapon.MaxDamage), transform.position.y);
        }
    }

    public void SetLookDirection(int direction)
    {
        LookDirection = direction;

        transform.rotation = Quaternion.Euler(0f, LookDirection > 0 ? 0f : 180f, 0f);
    }
}
