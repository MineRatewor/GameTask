using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Inventory.InvetoryUI
{
    public class UIInventoryDescription : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] private GameObject popupBackground;  
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _defenceIcon;
        [SerializeField] private Image _healthIcon;
        [SerializeField] private Image _weightIcon;
        [SerializeField] private TMP_Text _weightText;
        [SerializeField] private TMP_Text _effectValueText;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _consumeButton;
        [SerializeField] private Button _equipmentButton;
        private void Awake()
        {
          ResetDescription(true);   
        }
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == popupBackground)
            {
                ResetDescription();
            }
        }
        public void ResetDescription(bool isStart = false)
        {
            if (isStart)
            {
                transform.localScale = Vector3.zero;
            }
            else
            {
                Hide();
            }
            _itemImage.gameObject.SetActive(false);
            _defenceIcon.gameObject.SetActive(false);
            _weightIcon.gameObject.SetActive(false);
            _healthIcon.gameObject.SetActive(false);
            _title.text = " ";
            _effectValueText.text = " ";
            _weightText.text = " ";
            
            _consumeButton.gameObject.SetActive(false);
            _equipmentButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);

        }

        private void SetDescription(Sprite sprite, string itemName, float weight)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _title.text = itemName;
            
            _weightIcon.gameObject.SetActive(true);
            _weightText.text = weight + "кг";
            
            Show();
        }

        public void SetDescriptionEquipment(Sprite sprite, string itemName, int defense, float weight)
        {
            SetDescription(sprite,itemName, weight);
            _defenceIcon.gameObject.SetActive(true);
            _effectValueText.text = "+" + defense;
            
            _equipmentButton.gameObject.SetActive(true);
        }

        public void SetDescriptionConsumable(Sprite sprite, string itemName, int health, float weight)
        {
            SetDescription(sprite,itemName, weight);
            _healthIcon.gameObject.SetActive(true);
            _effectValueText.text = "+" + health + " HP";
            
            _consumeButton.gameObject.SetActive(true);
        }

        public void SetDescriptionAmmo(Sprite sprite, string itemName, float weight)
        {
            SetDescription(sprite, itemName, weight);
            _buyButton.gameObject.SetActive(true);
        }

        private void Show()
        {
            transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBounce);
        }

        private void Hide()
        {
            transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        }
    }
}