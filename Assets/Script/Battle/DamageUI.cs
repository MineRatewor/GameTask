using System;
using DG.Tweening;
using Script.CustomEventBus;
using Script.CustomEventBus.Signal;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.Battle
{
    public class DamageUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;
        
        private EventBus _eventBus;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<DamagePlayerSignal>(ShowDamagePlayer);
        }

        private void Start()
        {
            ResetDamageText();
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<DamagePlayerSignal>(ShowDamagePlayer);
        }

        private void ShowDamagePlayer(DamagePlayerSignal signal)
        {
            ShowDamageText( signal.Damage);
        }
        
        
        private void ShowDamageText(int damage)
        {
            _damageText.text = $"-{damage}";
            
            _damageText.alpha = 0;
           _damageText.transform.localPosition = Vector3.zero;
            
           
            _damageText.DOFade(1, 0.2f);
            
            _damageText.transform.DOLocalMoveY(415f, 1f).SetEase(Ease.OutQuad);
            _damageText.DOFade(0, 1f).SetDelay(1f).OnComplete(() =>
            {
                ResetDamageText();
            });
        }

        private void ResetDamageText()
        {
            _damageText.text = "";

            _damageText.transform.localPosition = 
                new Vector3(_damageText.transform.localPosition.x, 105f,
                _damageText.transform.localPosition.z);
        }
    }
}