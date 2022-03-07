using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardPresenter : MonoBehaviour
    {
        [SerializeField] private CardContainer _card = new CardContainer();
        [SerializeField] private CardCounter _counter;
        [SerializeField] private CardLogic _logic;

        private readonly List<Text> _values = new List<Text>();
        private float _removeDuration = .5f;
        
        private void Start()
        {
            CollectValues();
        }
     
        private void CollectValues()
        {
            _values.Add(_card._manaText);
            _values.Add(_card._attackText);
            _values.Add(_card._hpText);
        }
        
        public void CountToRandomValue(int randomValue)
        {
            var valueToChange = _values[UnityEngine.Random.Range(0, _values.Count)];
            _logic.IsHPChanging = valueToChange.Equals(_card._hpText);
            var startValue = Int32.Parse(valueToChange.text);
            
            _counter.StartCounter(valueToChange, startValue, randomValue);
        }

        public void RemoveCardFromHandOnUI()
        {
            iTween.ScaleTo(gameObject, Vector3.zero, _removeDuration);
        }
    }
}
