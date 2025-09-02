using UnityEngine;

public class ColorManager
{
    private readonly ColorData[] _colorData;

    public ColorManager(ColorData[] colorData)
    {
        _colorData = colorData;
    }
    
    public ColorData[] GetRandomColors(int count, bool unique = false)
    {
        var colorData = new ColorData[count];

        if (unique && _colorData.Length >= count)
        {
            var shuffledColors = Shuffle(_colorData);
            for (var i = 0; i < count; i++)
            {
                colorData[i] = shuffledColors[i];
            }
        }
        else
        {
            for (var i = 0; i < count; i++)
            {
                colorData[i] = GetRandomColor();
            }
        }
        
        return colorData;
    }
    
    public ColorData GetRandomColor()
    {
        return _colorData[Random.Range(0, _colorData.Length)];
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
}
