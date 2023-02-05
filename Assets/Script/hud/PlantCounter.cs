using TMPro;
using UnityEngine;

public class PlantCounter  : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    public void SetCounter(int plants, int maxPlants)
    {
        counterText.SetText($"{plants} / {maxPlants}");
    }
}
