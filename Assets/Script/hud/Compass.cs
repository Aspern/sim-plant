using UnityEngine;
using UnityEngine.UIElements;

public class Compass : MonoBehaviour {

    private RectTransform _rt;
    private int _currentDirection;

    public int CurrentDirection {
        get { return _currentDirection; }
    }
    
    private void Awake() {
        _rt = gameObject.GetComponent<RectTransform>();
    }

    void Start() {
        _currentDirection = 0;
        LeanTween.delayedCall(Random.Range(5f, 10f), callback);
    }

    void callback() {
        _currentDirection = Random.Range(0, 8);
        _rt.Rotate( new Vector3( 0, 0, 45 * _currentDirection ) );
        LeanTween.delayedCall(Random.Range(5f, 10f), callback);
    }
}
