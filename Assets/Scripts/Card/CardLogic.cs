using UnityEngine;

namespace Card
{
    public class CardLogic : MonoBehaviour 
    {
        public delegate void ValueChangedEvent();
        public event ValueChangedEvent OnEventValueChangeCompleted;

        public delegate void HPisOverEvent(CardLogic logic);
        public event HPisOverEvent OnEventHPIsOver;
        
        [SerializeField] private CardPresenter _cardPresenter;
        [SerializeField] private DragAndDrop _dragAndDrop;

        private const int _minIntValueInclusive = -2;
        private const int _maxIntValueExclusive = 10;

        private const float _minHPIntValue = 1;
        internal bool _isHPChanging;

        public void SetRotation(float rotationZ)
        {
            transform.eulerAngles = new Vector3(0, 0, rotationZ);
        }

        public void CountToRandomValue()
        {
            _cardPresenter.CountToRandomValue(GetRandomValue());
            _isHPChanging = _cardPresenter._isHP;
            _dragAndDrop._initialPos = transform.position;
        }

        private int GetRandomValue()
            => Random.Range(_minIntValueInclusive, _maxIntValueExclusive);

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
            var isOver = newValue < _minHPIntValue;
            if (isOver && OnEventHPIsOver !=null) {
                OnEventHPIsOver(this);
            }
            return isOver;
        }
    }
}
