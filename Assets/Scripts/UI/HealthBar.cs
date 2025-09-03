using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        
        private int _healthPoints;
        private int _currentHealthPoints;

        public void Initialize(int healthPoints)
        {
            _healthPoints = healthPoints;
            _currentHealthPoints = healthPoints;
            
            fillImage.fillAmount = 1f;
            
            UpdateColor();
        }

        public void UpdateHealthBar(int damage)
        {
            _currentHealthPoints = Mathf.Max(0, _currentHealthPoints - damage);
        
            var healthPercentage = (float)_currentHealthPoints / _healthPoints;
            fillImage.fillAmount = healthPercentage;
        
            UpdateColor();
        }
        
        private void UpdateColor()
        {
            var healthPercentage = (float)_currentHealthPoints / _healthPoints;
        
            if (healthPercentage > 0.6f)
            {
                fillImage.color = Color.green;
            }
            else if (healthPercentage > 0.3f)
            {
                fillImage.color = Color.yellow;
            }
            else
            {
                fillImage.color = Color.red;
            }
        }
    }
}