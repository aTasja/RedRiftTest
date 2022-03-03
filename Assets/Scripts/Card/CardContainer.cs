using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    [System.Serializable]
    public class CardContainer
    {
        [SerializeField] internal Text _title;
        [SerializeField] internal Text _decription;
        [SerializeField] internal Text _manaText;
        [SerializeField] internal Text _attackText;
        [SerializeField] internal Text _hpText;
    }
}
