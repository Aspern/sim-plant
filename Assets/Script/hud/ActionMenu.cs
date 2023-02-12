using Script.plant;
using Script.tile;
using UnityEngine;
using UnityEngine.UI;

namespace Script.hud
{
    public class ActionMenu : MonoBehaviour
    {
        public const string ActionMenuComponentName = "ActionMenu";

        [Header("UI")] 
        public Button nectarButton;
        public Button beeButton;
        public Button seedButton;
        public Button scytheButton;

        public void CheckActionButtonsInteractable(PlantableTile tile)
        {
      
            if (tile.IsPlanted())
            { 
               nectarButton.interactable = IsNectarButtonInteractable(tile);
               beeButton.interactable = IsBeeButtonInteractable(tile);
               seedButton.interactable = IsSeedButtonInteractable(tile);
               scytheButton.interactable = IsSScytheButtonInteractable(tile);
            }
            else
            {
                ResetButtonInteractable();
            }
        }

        public void ResetButtonInteractable()
        {
            nectarButton.interactable = false;
            beeButton.interactable = false;
            seedButton.interactable = false;
            scytheButton.interactable = false;
        }

        public void DisableNectarButton()
        {
            nectarButton.interactable = false;
        }
        
        public void DisableBeeButton()
        {
            beeButton.interactable = false;
        }
        
        public void DisableSeedButton()
        {
            seedButton.interactable = false;
        }
        
        public void DisableScytheButton()
        {
            scytheButton.interactable = false;
        }

        private static bool IsNectarButtonInteractable(PlantableTile tile)
        {
            var plant = tile.Plant.GetComponent<Plant>();
               
            return tile.IsPlanted() 
                   && !plant.IsBlooming 
                   && !plant.IsBloomed()
                   && !plant.IsBudGrowing
                   && !plant.IsBudGrown()
                   && !plant.IsWithered;
        }

        private static bool IsBeeButtonInteractable(PlantableTile tile)
        {
            var plant = tile.Plant.GetComponent<Plant>();

            return tile.IsPlanted()
                   && plant.IsBloomed()
                   && tile.BeesPresent;
        }
        
        private static bool IsSeedButtonInteractable(PlantableTile tile)
        {
            var plant = tile.Plant.GetComponent<Plant>();

            return tile.IsPlanted()
                   && plant.IsBudGrown();
        }

        private static bool IsSScytheButtonInteractable(PlantableTile tile)
        {
            var plant = tile.Plant.GetComponent<Plant>();

            return tile.IsPlanted()
                   && plant.IsWithered;
        }
    }
}