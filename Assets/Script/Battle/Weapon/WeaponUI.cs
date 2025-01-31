using System;
using UnityEngine;
using UnityEngine.UI;


namespace Script.Battle
{
    public class WeaponUI : MonoBehaviour
    {
        [SerializeField] private Image _gun;
        [SerializeField] private Image _rifle;

        private void Start()
        {
            UseGun();
        }

        public void UseGun()
        {
            _gun.color = Color.green;
            _rifle.color = Color.white;
        }

        public void UseRifle()
        {
            _gun.color = Color.white;
            _rifle.color = Color.green;
        }
    }
}