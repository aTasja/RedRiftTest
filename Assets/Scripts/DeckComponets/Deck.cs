using System.Collections.Generic;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private UI.UI _ui;
        
        [SerializeField] private DeckHolder _deckHolder;
        private List<CardLogic> _deck = new List<CardLogic>(6);

        private int _minCapacity = 4;
        private int _maxCapasity = 7;
        
        private int _currentCardNumber;
        private CardLogic _currentCardLogic;
        
        void Start()
        {
            InstantiateCards();
            _deckHolder.PlaceCards(_deck);
            _ui.AddListener(ChangeDeckValues);
            _currentCardNumber = 0;
        }
        private void InstantiateCards()
        {
            var deckCapacity = Random.Range(_minCapacity, _maxCapasity);
            for (int i = 0; i < deckCapacity; i++) {
                var card = Instantiate(_cardPrefab, _ui.gameObject.transform);
                _deck.Add(card.GetComponent<CardLogic>());
            }
        }

        public void ChangeDeckValues()
        {
            _ui.BlockButton(true);
            
            if(_currentCardNumber >= _deck.Count) _currentCardNumber = 0;
            
            _currentCardLogic = _deck[_currentCardNumber];
            _currentCardLogic.OnEventValueChangeCompleted += GetNextCard;
            _currentCardLogic.OnEventHPIsOver += RemoveCardFromDeck;
            _currentCardLogic.CountToRandomValue();
        }
        
        private void RemoveCardFromDeck(CardLogic cardLogic)
        {
            cardLogic.OnEventHPIsOver -= RemoveCardFromDeck;
            cardLogic.OnEventValueChangeCompleted -= GetNextCard;

            for (int i = 0; i < _deck.Count; i++){
                if (cardLogic.Equals(_deck[i])) {
                    _deck.RemoveAt(i);
                }
            }
            cardLogic.RemoveCard();
            
            StartCoroutine(_deckHolder.RelocateCards(_deck, GetNextCard ));
            
            _currentCardLogic = null;
            _currentCardNumber--;

        }
        private void GetNextCard()
        {
            if (_currentCardLogic != null) {
                _currentCardLogic.OnEventValueChangeCompleted -= GetNextCard;
                _currentCardLogic.OnEventHPIsOver -= RemoveCardFromDeck;
                _currentCardLogic = null;
            }
            
            if(_deck.Count == 0) return;
            
            _currentCardNumber ++;
            _ui.BlockButton(false);
        }
    }
}  
