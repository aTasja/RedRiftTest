using Card;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Table : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null) {
                eventData.pointerDrag.GetComponent<CardLogic>().IsOnTable = true;
            }
        }
    }
}
