using System;
using System.Collections;
using System.Collections.Generic;
using Card;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace Deck
{
    public class DeckHolder : MonoBehaviour
    {
        [SerializeField] private Transform[] _path;

        private float _minRotation = -10f;
        private float _maxRotation = 10f;
        private float _repositionDelay = 1f;

        private int _replaceCardNumber = 0;
        private bool _movementCompleted = false;
        private bool _rotationCompleted = false;

        public void PlaceCards(List<CardLogic> deck)
        {
            for (int i = 0; i < deck.Count; i++) {
                var percentage = GetPathPercentage(i, PosStep(deck.Count));
                var rotationZ = GetRotation(i, RotationStep(deck.Count));
                PutOnPath(deck[i], percentage, rotationZ);
            }
        }

        public IEnumerator RelocateCards(List<CardLogic> deck, Action callback)
        {
            for (int i = 0; i < deck.Count; i++) {
                _movementCompleted = false;
                _rotationCompleted = false;
                
                var percentage = GetPathPercentage(i, PosStep(deck.Count));
                var rotationZ = GetRotation(i, RotationStep(deck.Count));
                MoveAndRotate(deck[i], percentage, rotationZ);
                
                yield return new WaitUntil(() => ( _movementCompleted && _rotationCompleted ));
            }
            callback();
        }

        private void PutOnPath(CardLogic card, float percentage, float rotationZ)
        {
            iTween.PutOnPath(card.gameObject, _path, percentage);
            SetCardRotation(card, rotationZ);
        }

        private void MoveAndRotate(CardLogic card, float percentage, float rotationZ)
        {
            var position = iTween.PointOnPath(_path, percentage);
            iTween.MoveTo(card.gameObject, iTween.Hash(
                "position", position, "isLocal", false, "time", _repositionDelay,
                "oncomplete", "MovementCompleted", "oncompletetarget", gameObject));

            iTween.RotateTo(card.gameObject, iTween.Hash(
                "z", rotationZ, "time", _repositionDelay, "easetype", iTween.EaseType.easeOutSine,
                "oncomplete", "RotationCompleted", "oncompletetarget", gameObject));

        }

        private bool MovementCompleted() => _movementCompleted = true;
        private bool RotationCompleted() => _rotationCompleted = true;

        private float GetPathPercentage(int cardNumber, float posStep)
            => posStep * (cardNumber + 1);

        private float GetRotation(int cardNumber, float rotationStep)
            => _maxRotation - (rotationStep * cardNumber);

        private void SetCardRotation(CardLogic card, float rotationZ)
            => card.SetRotation(rotationZ);

        private float PosStep(int count)
            => 1f / (count + 1);
        
        private float RotationStep(int count)
            => (Mathf.Abs(_minRotation) + Mathf.Abs(_maxRotation)) / (count - 1);
    }
}
