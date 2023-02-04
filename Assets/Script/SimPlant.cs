using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimPlant : MonoBehaviour
{
    public GameObject selectedTile;
    private readonly List<Button> _actionButtons = new();
    private Button _actionButtonNectar;
    private Button _actionButtonBee;
    private Button _actionButtonSeed;
    private Button _actionButtonSun;

    private void Awake()
    {
        _actionButtonNectar = GameObject.Find("ActionButtonNectar").GetComponent<Button>();
        _actionButtonBee = GameObject.Find("ActionButtonBee").GetComponent<Button>();
        _actionButtonSeed = GameObject.Find("ActionButtonSeed").GetComponent<Button>();
        _actionButtonSun = GameObject.Find("ActionButtonSun").GetComponent<Button>();
        
        _actionButtons.Add(_actionButtonNectar);
        _actionButtons.Add(_actionButtonBee);
        _actionButtons.Add(_actionButtonSeed);
        _actionButtons.Add(_actionButtonSun);
    }


    public void SelectTile(GameObject tileGameObj)
    {
        if (selectedTile) // Remove highlight from past selection
        {
            selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
        
        selectedTile = tileGameObj;
        tileGameObj.GetComponent<Highlight>()?.ToggleHighlight(true);
    }

    public void UnselectTile()
    {
        if (!selectedTile) return;
        
        selectedTile.GetComponent<Highlight>()?.ToggleHighlight(false);
        selectedTile = null;
    }

    public void EnableAllActionButtons()
    {
        _actionButtons.ForEach(button => button.interactable = false);
    }

    public void DisableAllActionButtons()
    {
        _actionButtons.ForEach(button => button.interactable = true);
    }

    public void EnableActionButton(Action action)
    {
        ChangeActionButtonInteractive(action, false);
    }
    
    public void DisableActionButton(Action action)
    {
        ChangeActionButtonInteractive(action, false);
    }

    private void ChangeActionButtonInteractive(Action action, bool interactive)
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