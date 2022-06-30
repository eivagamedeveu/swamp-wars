using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Unit))]
public class UnitAnimationsHandler : MonoBehaviour
{
    private SkeletonAnimation _skeletonAnimation;
    private TrackEntry _track;
    
    public event UnityAction IsAnimationCompleted;
    
    private void Awake()
    {
        _skeletonAnimation = GetComponent<SkeletonAnimation>();

        var attachment = _skeletonAnimation.Skeleton.GetAttachment("weapons-r", "weapons/octopus 1");
        
        _skeletonAnimation.Skeleton.FindSlot("weapons-r").Attachment = attachment;
    }

    private void OnAnimationCompleted(TrackEntry trackEntry)
    {
        IsAnimationCompleted?.Invoke();
        
        _track.Complete -= OnAnimationCompleted;
    }
    
    public void SetAnimation(AnimationReferenceAsset targetAnimation, bool loop)
    {
        _track = _skeletonAnimation.state.SetAnimation(0, targetAnimation, loop);
        
        _track.Complete += OnAnimationCompleted;
    }
}