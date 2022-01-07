using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MenuSaveButton : MonoBehaviour
    {
        [SerializeField] private string saveName = "Default Save Name";
        [SerializeField] private string defaultScene = "Gameplay 1";
        
        [SerializeField] private GameObject availableObject;
        [SerializeField] private GameObject createdObject;

        private GameplaySystem _gameplaySystem;
        private Save _save;

        private void Start()
        {
            _gameplaySystem = FindObjectOfType<GameplaySystem>();
            
            bool saveIsCreated = Save.IsValid(saveName);
            UpdateActiveObject(saveIsCreated);

            if (saveIsCreated)
                _save = Save.Read(saveName);
        }

        private void UpdateActiveObject(bool isCreated)
        {
            availableObject.SetActive(!isCreated);
            createdObject.SetActive(isCreated);
        }

        public void CreateSave()
        {
            _save = new Save(saveName)
            {
                CurrentScene = defaultScene
            };

            _save.Triggers.Add("Tutorial");
            _save.Write();

            UpdateActiveObject(true);
        }

        public void LoadSave()
        {
            _gameplaySystem.StartGameplay(_save).Forget();
        }

        public void DeleteSave()
        {
            _save.Delete();
            _save = null;
            
            UpdateActiveObject(false);
        }
    }
}