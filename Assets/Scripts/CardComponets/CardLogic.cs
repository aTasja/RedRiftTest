using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Card
{
    public class CardLogic : MonoBehaviour 
    {
        public delegate void ValueChangedEvent();
        public event ValueChangedEvent OnEventValueChangeCompleted;

        public delegate void HPisOverEvent(CardLogic logic);
        public event HPisOverEvent OnEventHPIsOver;
        
        public bool IsHPChanging;
        public bool IsOnTable;
        public Vector3 PositionInHand;
        
        [SerializeField] private CardPresenter _cardPresenter;

        private const int MinIntValueInclusive = -2;
        private const int MaxIntValueExclusive = 10;
        private const float MinHPIntValue = 1;

        private void Start()
        {
            PositionInHand = transform.position;
        }
        
        public void SetRotation(float rotationZ)
        {
            transform.eulerAngles = new Vector3(0, 0, rotationZ);
        }

        public void CountToRandomValue()
        {
            _cardPresenter.CountToRandomValue(GetRandomValue());
            PositionInHand = transform.position;
        }

        private int GetRandomValue()
            => Random.Range(MinIntValueInclusive, MaxIntValueExclusive);

        public void ValueChangeCompleted()
        {
            if (OnEventValueChangeCompleted != null) {
                OnEventValueChangeCompleted();
            }
        }

        public void RemoveCard()
        {
            _cardPresenter.RemoveCardFromHandOnUI();
        }
        
        internal bool HPisOver(int newValue)
        {
            var isOver = newValue < MinHPIntValue;
            if (isOver && OnEventHPIsOver !=null) {
                OnEventHPIsOver(this);
            }
            return isOver;
        }
    }
}
