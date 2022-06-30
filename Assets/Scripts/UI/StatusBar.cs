using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class StatusBar : MonoBehaviour
{
    [SerializeField] private float _changeDuration;
    [SerializeField] private TMP_Text _text;
    
    private Tween _tween;
    
    protected Slider Slider;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    public void Init(int maxValue, int value)
    {
        Slider.maxValue = maxValue;
        Slider.value = value;
    }

    protected void UpdateText()
    {
        if(_text != null)
            _text.text = ((int)Slider.value).ToString();
    }

    public virtual void ChangeValue(float newValue)
    {
        _tween = Slider.DOValue(newValue, _changeDuration).SetEase(Ease.Linear).SetLink(gameObject);

        _tween.onUpdate += UpdateText;

        _tween.OnComplete(() =>
        {
            _tween.onUpdate -= UpdateText;
        });
    }
}
