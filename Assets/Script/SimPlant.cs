using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimPlant : MonoBehaviour
{
    public GameObject selectedTile;

    private readonly List<Button> _actionButtons = new();
    private PlantCounter _plantCounter;
    private Button _actionButtonNectar;
    private Button _actionButtonBee;
    private Button _actionButtonSeed;
    private Button _actionButtonScythe;
    private MapData _mapData;
    private int _maxEdgeTiles = 0;

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

    private void Update()
    {
        if (_maxEdgeTiles == 0)
        {
            _maxEdgeTiles = _mapData.Tiles.FindAll(e => e.tile.type == TileType.PLAIN).Count;
        }
       
        var plantedTiles = _mapData.Tiles.FindAll(e => e.tile.planted).Count;
        
        _plantCounter.SetCounter(plantedTiles, _maxEdgeTiles);

        if (plantedTiles == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        } else if (plantedTiles == _maxEdgeTiles)
        {
            SceneManager.LoadScene("WinScene");
        }
    }


    public void SelectTile(GameObject tileGameObj)
    {
        if (selectedTile) // Remove highlight from past selection
        {
            selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
        
        selectedTile = tileGameObj;
        selectedTile.GetComponent<Tile>().ActionHandler = ChangeActionButtonInteractive;
        selectedTile.GetComponent<Highlight>()?.ToggleHighlight(true);
    }

    public void UnselectTile()
    {
        if (!selectedTile) return;
        
        selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
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

    public void OnGrowthChanged(Tile tile)
    {
        if (selectedTile && selectedTile.GetComponent<Tile>().Equals(tile))
        {
            EnableActionButton(ActionType.NECTAR);
        }
    }
    public void OnFlourishedChanged(Tile tile)
    {
        if (selectedTile && selectedTile.GetComponent<Tile>().Equals(tile))
        {
            EnableActionButton(ActionType.BEE);
        }
    }
    
    public void OnPollinatedChanged(Tile tile)
    {
        if (selectedTile && selectedTile.GetComponent<Tile>().Equals(tile))
        {
            EnableActionButton(ActionType.SEED);
        }
    }
}