using System;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float initialRotateTime = 30f;
    public bool flowerAttracted { get; set;}
    private Tile _tileObj;
    private void Start()
    {
        _tileObj = gameObject.GetComponentInParent<Tile>();
    }

    private bool tweening;
    private bool tournAround = true;

    private void Update()
    {
        if (flowerAttracted && !tweening)
        {
            tweening = true;
            LeanTween.rotateAround(gameObject, new Vector3(0, 1, 0), 360, initialRotateTime).setOnComplete(o =>
            {
                gameObject.GetComponentInParent<Tile>().OnPollinated();
                gameObject.GetComponentInParent<TreePlant>().Pollinated = true;
                //TODO do not destroy Bees
                //gameObject.GetComponentInParent<TreePlant>().RemoveBees();
                tweening = false;
            });
        }
        else if (tournAround)
        {
            if (!tweening)
            {
                tournAround = false;
                tweening = true;
                LeanTween.rotateAround(gameObject, new Vector3(0, 1, 0), 360, 10f).setOnComplete(o => tweening = false);

            }
        }
        else
        {
            if (!tweening)
            {
                Vector3 newDirection = _tileObj.CreateBee(this, gameObject);
                Debug.Log(newDirection);
                tweening = true;
                tournAround = true;
                if (newDirection != Vector3.zero)
                {
                    var position = transform.position;
                    newDirection = new Vector3(position.x + newDirection.x, position.y,
                        position.z + newDirection.z);
                    Debug.Log("current Location" + position.ToString());
                    Debug.Log(newDirection.ToString());
                    LeanTween.move(gameObject, newDirection, 1f).setOnComplete(o1 => tweening = false);
                }
                else
                {
                    tweening = false;
                }
            }
        }
    }

    public void OnNewTile(Tile tile)
    {
        Debug.Log("New Tile set: " + tile);
        _tileObj = tile;
    }
}