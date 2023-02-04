using UnityEngine;

public class TreePlant :  MonoBehaviour
{
    public float initialGrowTime = 60f;
    public GameObject flowerPrefab;
    public GameObject beePrefab;

    private GameObject _flowerGameObj;
    private GameObject _beeGameObj;

    private void Awake()
    {
        transform.localScale = new Vector3(0, 0.5f, 0);
    }

    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), initialGrowTime).setOnComplete(o =>
        {
            gameObject.GetComponentInParent<Tile>().OnFullyGrown();
        });
    }
    
    public void CreateFlower()
    {
        if(_flowerGameObj) return;

        var parentTransform = transform;
        _flowerGameObj = Instantiate(
            flowerPrefab,
            parentTransform.position,
            Quaternion.identity,
            parentTransform
        );
    }

    public void CreateBees()
    {
        if(_beeGameObj) return;

        var parentTransform = transform;
        _beeGameObj = Instantiate(
            beePrefab,
            parentTransform.position,
            Quaternion.identity,
            parentTransform
        );
    }
}