using UnityEngine;
using Zenject;

namespace UI
{
    public class KeyGrid : MonoBehaviour
    {
        [SerializeField] private Key keyPrefab;
        [SerializeField] private KeyCell keyCellPrefab;
        [SerializeField] private int gridSize = 6;
        
        private ColorManager _colorManager;

        [Inject]
        private void Construct(ColorManager colorManager)
        {
            _colorManager = colorManager;
        }

        public void Initialize(ColorData[] requiredColors)
        {
            var totalKeysCount = gridSize * gridSize;
            var data = GenerateTotalColorData(requiredColors, totalKeysCount);

            GenerateKeys(data);
        }

        private ColorData[] GenerateTotalColorData(ColorData[] requiredColors, int totalKeysCount)
        {
            var data = new ColorData[totalKeysCount];

            var iterator = 0;
            for (var i = 0; i < requiredColors.Length; i++, iterator++)
            {
                data[iterator] = requiredColors[i];
            }

            for (; iterator < totalKeysCount; iterator++)
            {
                data[iterator] = _colorManager.GetRandomColor();
            }
            
            return Shuffle(data);;
        }

        private ColorData[] Shuffle(ColorData[] array)
        {
            for (var i = array.Length - 1; i > 0; i--)
            {
                var randomIndex = Random.Range(0, i + 1);
            
                (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
            }
        
            return array;
        }

        private void GenerateKeys(ColorData[] data)
        {
            foreach (var color in data)
            {
                var key = Instantiate(keyPrefab);
                key.Initialize(color);
                
                var keyCell = Instantiate(keyCellPrefab, transform);
                keyCell.SetKey(key);
            }
        }
    }
}
