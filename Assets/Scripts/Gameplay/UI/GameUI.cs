using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private PlanetData planetData;

        [SerializeField] private Display populationDisplay;
        [SerializeField] private Display currencyDisplay;
        [SerializeField] private Display housingDisplay;

        private void Start()
        {
            planetData.population
                .Select(value => $"{value:G}")
                .Subscribe(populationDisplay.ShowText);

            planetData.currency
                .Select(value => $"{value:C0}")
                .Subscribe(currencyDisplay.ShowText);

            planetData.housing
                .Select(value => $"{value:G}")
                .Subscribe(housingDisplay.ShowText);
        }
    }
}