using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class MaterialManager : MonoBehaviour
    {
        [SerializeField] private Image slide;
        [SerializeField] private List<Sprite> material;
        private int currentMaterialIndex;

        [SerializeField] private Button quitButton;
        [SerializeField] private string quitTargetScene;
        [SerializeField] private Button backButton;
        [SerializeField] private Button nextButton;

        private void OnEnable()
        {
            quitButton.onClick.AddListener(QuitMaterialScene);
            backButton.onClick.AddListener(DisplayPreviousSlide);
            nextButton.onClick.AddListener(DisplayNextSlide);
        }

        private void OnDisable()
        {
            quitButton.onClick.RemoveListener(QuitMaterialScene);
            backButton.onClick.RemoveListener(DisplayPreviousSlide);
            nextButton.onClick.RemoveListener(DisplayNextSlide);
        }

        private void Start()
        {
            currentMaterialIndex = 0;
            slide.sprite = material[currentMaterialIndex];
        }

        private void QuitMaterialScene()
        {
            SceneManager.LoadScene(quitTargetScene, LoadSceneMode.Single);
        }

        private void DisplayPreviousSlide()
        {
            if (currentMaterialIndex > 0) currentMaterialIndex--;
            slide.sprite = material[currentMaterialIndex];
        }

        private void DisplayNextSlide()
        {
            if (currentMaterialIndex < material.Count - 1) currentMaterialIndex++;
            slide.sprite = material[currentMaterialIndex];
        }
    }
}
