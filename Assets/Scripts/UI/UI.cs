using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _buttonCanvasGroup;
        private List<Action> _listeners =  new List<Action>();

        private float _alphaBlocked = .3f;
        private float _alphaFull = 1;
        
        public void AddListener(Action action)
        {
            _listeners.Add(action);
        }
        
        public void ButtonHandler()
        {
            if (_listeners.Count > 0) {
                foreach (var listener in _listeners) {
                    listener();
                }
            }
        }

        public void BlockButton(bool toBlock)
        {
            _buttonCanvasGroup.alpha = toBlock ? _alphaBlocked: _alphaFull;
            _buttonCanvasGroup.blocksRaycasts = !toBlock;
        }
    }
}

