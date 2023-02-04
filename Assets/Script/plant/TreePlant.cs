using UnityEngine;

public class TreePlant :  MonoBehaviour
{
    public float initialGrowTime = 60f;
    public GameObject flowerPrefab;

    private GameObject _flowerGameObj;

    private void Awake()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), initialGrowTime).setOnComplete(o =>
        {
            gameObject.GetComponentInParent<Tile>().PlantGrown = true;
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
}