
using UnityEngine;

namespace Script.Inventory.InvetoryUI.Dragging
{
    public class MouseFollower : MonoBehaviour
    {
         private Canvas _canvas;
         private UIInventoryItem _item;

        private void Awake()
        {
           _canvas = transform.root.GetComponent<Canvas>();
            _item = GetComponentInChildren<UIInventoryItem>();
        }

        public void SetData(Sprite sprite, int quantity)
        {
            _item.SetData(sprite, quantity);
        }

        private void Update()
        {
            Vector2 position = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            Input.mousePosition,
            _canvas.worldCamera,
            out position);
            transform.position = _canvas.transform.TransformPoint(position);
        }

        public void Toogle(bool val)
        {
            gameObject.SetActive(val);
        }
        
    }
}