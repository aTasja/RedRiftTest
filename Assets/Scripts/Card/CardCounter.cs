using System;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class CardCounter: MonoBehaviour
    {
        [SerializeField] private CardLogic _logic;
        private const float _duration = .5f;

        private Text _valueToChange;
        private int _startValue;
        private int _finishValue;
        private int _sign;
        
        public void StartCounter(Text valueToChange, int start, int finish)
        {
            _valueToChange = valueToChange;
            _startValue = start;
            _finishValue = finish;
            _sign = _finishValue > _startValue ? 1 : -1;

            if(CounterIsOver()) return;

            CountToValue();
        }

        private void CountToValue()
        {
            _startValue += _sign;
            TextUpdate(_startValue);
        }
  
        private void TextUpdate(int newValue)
        {
            _valueToChange.text = newValue.ToString();
            
            if (_logic._isHPChanging && _logic.HPisOver(newValue)) return;
            if(CounterIsOver()) return;
            
            Invoke(nameof(CountToValue), _duration);
        }
        
        private bool CounterIsOver()
        {
            if (!_finishValue.Equals(_currentIntValue))
                return false;
            
            _logic.ValueChangeCompleted();
            return true;
        }

        private int _currentIntValue {
            get {
                return Int32.Parse(_valueToChange.text);
            }
        }
    }
}
