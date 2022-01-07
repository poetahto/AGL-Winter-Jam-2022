using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MenuSaveButton : MonoBehaviour
    {
        [SerializeField] private string saveName = "Default Save Name";
        [SerializeField] private string defaultScene = "Gameplay";
        
        [SerializeField] private GameObject availableObject;
        [SerializeField] private GameObject createdObject;

        private Save _save;

        private void Awake()
        {
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
            _save = new Save(saveName);
            _save.SaveData.Add("CurrentScene", defaultScene);
            _save.Write();
            
            UpdateActiveObject(true);
        }

        public void LoadSave()
        {
            var currentScene = (string) _save.SaveData["CurrentScene"];
            SceneManager.LoadScene(currentScene);
        }

        public void DeleteSave()
        {
            _save.Delete();
            _save = null;
            
            UpdateActiveObject(false);
        }
    }
}