using UnityEngine;

public class Compass : MonoBehaviour {
    
    private RectTransform _compassNeedle;

    public int CurrentDirection { get; private set; }

    private void Awake() {
        _compassNeedle = GameObject.Find("CompassNeedle").GetComponent<RectTransform>();
    }

    private void Start() {
        CurrentDirection = 0;
        LeanTween.delayedCall(Random.Range(5f, 10f), Callback);
    }

    private void Callback()
    {
        CurrentDirection = Random.Range(0, 8);
        _compassNeedle.transform.rotation = Quaternion.Euler(0, 0, 45 * CurrentDirection);
        LeanTween.delayedCall(Random.Range(5f, 10f), Callback);
    }
}
