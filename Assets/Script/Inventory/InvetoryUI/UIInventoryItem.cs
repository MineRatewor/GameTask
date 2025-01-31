using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Inventory.InvetoryUI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, 
             OnItemBeginDrag, OnItemEndDrag, OnRightMouseClick;
        
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _quantityTxt;
        [SerializeField] private Image _borderImage;

        private bool _isEmpty = false;

        private void Awake()
        {
            ResetData();
            Deselect(); 
        }

        public void ResetData()
        {
            if (_itemImage != null)
            {
                _itemImage.gameObject.SetActive(false);
            }
            _isEmpty = true;
        }

        public void Deselect()
        {
            if (_borderImage != null)
            {
                _borderImage.enabled = false;
            }
        }

        public void SetData(Sprite sprite, int quantity = 1)
        {
            if (_itemImage != null)
            {
                _itemImage.gameObject.SetActive(true);
                _itemImage.sprite = sprite;
                if (quantity > 1)
                {
                    _quantityTxt.text = quantity + "";
                }
                else
                {
                    _quantityTxt.text = "";
                }
                _isEmpty = false;
            }
            
        }

        public void Select()
        {
            _borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isEmpty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}