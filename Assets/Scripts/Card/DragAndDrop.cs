using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class DragAndDrop : MonoBehaviour, 
        IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public bool IsOnTable;
        [SerializeField] private GameObject _glow;   
        
        private const float _cardReturnDuration = 1f;

        internal Vector3 _initialPos;
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
            _initialPos = transform.position;
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
            
            if (!IsOnTable) {
                ReturnToHandPosition();
            }
        }
        
        private void ReturnToHandPosition() 
            => iTween.MoveTo(gameObject, iTween.Hash(
        "position", _initialPos, "isLocal", false, "time", _cardReturnDuration));
        
        public void OnDrag(PointerEventData eventData) 
            => _rectTransform.anchoredPosition += eventData.delta/_canvas.scaleFactor;
        
    }
}
