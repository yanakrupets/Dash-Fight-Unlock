using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private const float HeightHealthBound = 0.6f;
        private const float MediumHealthBound = 0.3f;
        
        private int _healthPoints;
        private int _currentHealthPoints;

        public void Initialize(int healthPoints)
        {
            _healthPoints = healthPoints;
            _currentHealthPoints = healthPoints;
            
            fillImage.fillAmount = 1f;
            
            UpdateColor();
        }

        public void UpdateHealthBar(int currentHealthPoints)
        {
            _currentHealthPoints = currentHealthPoints;
            
            var healthPercentage = (float)_currentHealthPoints / _healthPoints;
            fillImage.fillAmount = healthPercentage;
        
            UpdateColor();
        }
        
        private void UpdateColor()
        {
            var healthPercentage = (float)_currentHealthPoints / _healthPoints;
        
            if (healthPercentage > HeightHealthBound)
            {
                fillImage.color = Color.green;
            }
            else if (healthPercentage > MediumHealthBound)
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