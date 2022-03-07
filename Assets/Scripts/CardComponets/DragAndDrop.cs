using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class DragAndDrop : MonoBehaviour, 
        IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private CardLogic _logic;
        [SerializeField] private GameObject _glow;   
        
        private const float CardReturnDuration = 1f;
        
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _canvas = FindObjectOfType<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _glow.SetActive(false);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _glow.SetActive(true);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _glow.SetActive(false);
            
            if (!_logic.IsOnTable) {
                ReturnToHandPosition();
            }
        }
        
        private void ReturnToHandPosition() 
            => iTween.MoveTo(gameObject, iTween.Hash(
        "position", _logic.PositionInHand, "isLocal", false, "time", CardReturnDuration));
        
        public void OnDrag(PointerEventData eventData) 
            => _rectTransform.anchoredPosition += eventData.delta/_canvas.scaleFactor;
        
    }
}
