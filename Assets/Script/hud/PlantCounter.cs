using TMPro;
using UnityEngine;

public class PlantCounter  : MonoBehaviour
{
    public const string PlantCounterComponentName = "PlantCounter";
    
    public TextMeshProUGUI counterText;

    public void SetCounter(int plants, int maxPlants)
    {
        counterText.SetText($"{plants} / {maxPlants}");
    }
}
