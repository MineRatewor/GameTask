using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Script.Battle.Equipment
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private Image _headIcon;
        [SerializeField] private Image _torsoIcon;
        [SerializeField] private TMP_Text _headDefense;
        [SerializeField] private TMP_Text _torsoDefense;

        public void SetHeadData(Sprite icon, int defense)
        {
            _headIcon.sprite = icon;
            _headDefense.text = "+" + defense;
        }

        public void SetTorsoData(Sprite icon, int defense)
        {
            _torsoIcon.sprite = icon;
            _torsoDefense.text = "+" + defense;
        }
    }
}