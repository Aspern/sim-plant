using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [Header("Environment")] [Tooltip("Time in seconds where compass needle should change shortest.")]
    public float minCompassUpdateTime = 5.0f;

    [Tooltip("Time in seconds where compass needle should change longest.")]
    public float maxCompassUpdateTime = 10.0f;


    public const string CompassComponentName = "Compass";
    public const string LabelNorth = "north";
    public const string LabelNorthWest = "north_west";
    public const string LabelNorthEast = "north_east";
    public const string LabelSouth = "south";
    public const string LabelSouthWest = "south_west";
    public const string LabelSouthEast = "south_east";
    public const string LabelWest = "west";
    public const string LabelEast = "east";

    private RectTransform _compassNeedle;

    private readonly List<string> _directions = new()
    {
        LabelNorth,
        LabelNorthWest,
        LabelWest,
        LabelSouthWest,
        LabelSouth,
        LabelSouthEast,
        LabelEast,
        LabelNorthEast
    };

    public string CurrentDirection { get; private set; }

    private void Awake()
    {
        _compassNeedle = GameObject.Find("CompassNeedle").GetComponent<RectTransform>();
        CurrentDirection = LabelNorth;
    }

    private void Start()
    {
        LeanTween.delayedCall(
            Random.Range(minCompassUpdateTime, maxCompassUpdateTime),
            OnCompassNeedleChange
        );
    }

    private void OnCompassNeedleChange()
    {
        var index = Random.Range(0, _directions.Count - 1);
        CurrentDirection = _directions[index];

        _compassNeedle.transform.rotation = Quaternion.Euler(0, 0, 45 * index);

        LeanTween.delayedCall(
            Random.Range(minCompassUpdateTime, maxCompassUpdateTime),
            OnCompassNeedleChange
        );
    }
}