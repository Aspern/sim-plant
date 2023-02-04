using UnityEngine;

public class TreePlant :  MonoBehaviour
{
    public float initialGrowTime = 60f;
    private void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), initialGrowTime);
    }
}