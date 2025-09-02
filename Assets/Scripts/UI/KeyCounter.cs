using TMPro;
using UnityEngine;

namespace UI
{
    public class KeyCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;

        private int _count;

        public void Initialize(int count)
        {
            _count = count;
            UpdateText(0);
        }

        public void UpdateText(int currentKeyCount)
        {
            countText.text = $"{currentKeyCount} / {_count}";
        }
    }
}