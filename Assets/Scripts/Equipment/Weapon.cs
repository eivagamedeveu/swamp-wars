using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    [SerializeField] private float _attackRange;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private WeaponType _type;

    public int MinDamage => _minDamage;
    public int MaxDamage => _maxDamage;

    public float AttackRange => _attackRange;

    public override void Init()
    {
        _weight *= _type switch
        {
            WeaponType.Short => LocalDataManager.Instance.Config.ShortWeaponWeightMultiplier,
            WeaponType.Medium => LocalDataManager.Instance.Config.MediumWeaponWeightMultiplier,
            WeaponType.Long => LocalDataManager.Instance.Config.LongWeaponWeightMultiplier,
            WeaponType.Range => 1,
            _ => 1
        };
        
        _attackRange = _type switch
        {
            WeaponType.Short => LocalDataManager.Instance.Config.ShortWeaponRange,
            WeaponType.Medium => LocalDataManager.Instance.Config.MediumWeaponRange,
            WeaponType.Long => LocalDataManager.Instance.Config.LongWeaponRange,
            WeaponType.Range => LocalDataManager.Instance.Config.RangeWeaponRange,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum WeaponType
{
    Short,
    Medium,
    Long,
    Range
}
