using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    [SerializeField] private AnimationReferenceAsset _takeDamageAnimation;
    [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private DamageDisplayer _damageDisplayer;

    private SkeletonAnimation _skeletonAnimation;
    private TrackEntry _trackEntry;

    private void Awake()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    public void TakeDamage(int damage, float attackerYPosition)
    {
        var yDifference = transform.position.y - attackerYPosition;
                
        if(yDifference is > 2f or < -2f) return;
    
        if(_trackEntry != null)
            if(!_trackEntry.Loop && !_trackEntry.IsComplete) return;
        
        _trackEntry = _skeletonAnimation.state.SetAnimation(0, _takeDamageAnimation, false);
        _trackEntry.Complete += ReturnToIdle;
      
        _damageDisplayer.ShowDamage(damage);
    }

    private void ReturnToIdle(TrackEntry trackEntry)
    {
        _trackEntry = _skeletonAnimation.state.SetAnimation(0, _idle, true);
        _trackEntry.Complete -= ReturnToIdle;
    }
}
