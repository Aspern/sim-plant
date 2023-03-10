using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimPlant : MonoBehaviour
{
    public GameObject selectedTile;
    public GameObject selectionPrefab;

    private readonly List<Button> _actionButtons = new();
    private PlantCounter _plantCounter;
    private Button _actionButtonNectar;
    private Button _actionButtonBee;
    private Button _actionButtonSeed;
    private Button _actionButtonScythe;
    private MapData _mapData;
    private int _maxEdgeTiles;

    private GameObject _selection;

    private void Awake()
    {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
        _actionButtonNectar = GameObject.Find("ActionButtonNectar").GetComponent<Button>();
        _actionButtonBee = GameObject.Find("ActionButtonBee").GetComponent<Button>();
        _actionButtonSeed = GameObject.Find("ActionButtonSeed").GetComponent<Button>();
        _actionButtonScythe = GameObject.Find("ActionButtonScythe").GetComponent<Button>();
        _plantCounter = GameObject.Find("PlantCounter").GetComponent<PlantCounter>();
        
        _actionButtons.Add(_actionButtonNectar);
        _actionButtons.Add(_actionButtonBee);
        _actionButtons.Add(_actionButtonSeed);
        _actionButtons.Add(_actionButtonScythe);
    }

    private void Start()
    {
        _selection = Instantiate(selectionPrefab);
        _selection.SetActive(false);
    }

    private void Update()
    {
        if (_maxEdgeTiles == 0)
        {
            _maxEdgeTiles = _mapData.Tiles.FindAll(e => e.tile.type == TileType.PLAIN).Count;
        }
       
        var plantedTiles = _mapData.Tiles.FindAll(e => e.tile.planted && !e.tile.PlantDead).Count;
        
        _plantCounter.SetCounter(plantedTiles, _maxEdgeTiles);

        if (plantedTiles == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        } else if (plantedTiles == _maxEdgeTiles)
        {
            SceneManager.LoadScene("WinScene");
            PlayerPrefs.SetInt("finishedLevel", PlayerPrefs.GetInt("currentLevel"));

        }
    }


    public void SelectTile(GameObject tileGameObj)
    {
        // if (selectedTile) // Remove highlight from past selection
        // {
        //     selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
        // }
        
        selectedTile = tileGameObj;
        selectedTile.GetComponent<Tile>().ActionHandler = ChangeActionButtonInteractive;
        // selectedTile.GetComponent<Highlight>()?.ToggleHighlight(true);
        
        _selection.SetActive(true);
        _selection.transform.position = tileGameObj.transform.position;
    }

    public void UnselectTile()
    {
        if (!selectedTile) return;
        
        // selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
        _selection.SetActive(true);

        selectedTile.GetComponent<Tile>().ActionHandler = null;
        selectedTile = null;
    }

    public void EnableAllActionButtons()
    {
        _actionButtons.ForEach(button => button.interactable = true);
    }

    public void DisableAllActionButtons()
    {
        _actionButtons.ForEach(button => button.interactable = false);
    }

    public void EnableActionButton(ActionType action)
    {
        ChangeActionButtonInteractive(action, true);
    }
    
    public void DisableActionButton(ActionType action)
    {
        ChangeActionButtonInteractive(action, false);
    }

    public void UseNectarAction()
    {
        selectedTile.GetComponent<Tile>().UseNectar();
    }
    
    public void UseBeeAction()
    {
        selectedTile.GetComponent<Tile>().UseBee();
    }
    
    public void UseSeedAction()
    {
        selectedTile.GetComponent<Tile>().UseSeed();
    }
    
    public void UseScytheAction()
    {
        selectedTile.GetComponent<Tile>().KillPlant();
    }

    private void ChangeActionButtonInteractive(ActionType action, bool interactive)
    {
        _actionButtons.ForEach(button =>
        {
            if (button.GetComponent<ActionButton>().action == action)
            {
                button.interactable = interactive;
            }
        });
    }
}