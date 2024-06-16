using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Shcreepzy
{
    public class CutsceneManager : MonoBehaviour
    {
        [Header("Slide01")]

        [SerializeField] private GameObject slide01;
        [SerializeField] private Button slide01Button;

        [Header("Slide02")]

        [SerializeField] private GameObject slide02;
        [SerializeField] private Button slide02Button;

        [Header("Slide03")]

        [SerializeField] private GameObject slide03;
        [SerializeField] private Button slide03Button;

        [Header("Target")]

        [SerializeField] private SceneAsset nextScene;

        private void OnEnable()
        {
            slide01Button.onClick.AddListener(OnSlide01ButtonClicked);
            slide02Button.onClick.AddListener(OnSlide02ButtonClicked);
            slide03Button.onClick.AddListener(OnSlide03ButtonClicked);
        }

        private void OnDisable()
        {
            slide01Button.onClick.RemoveListener(OnSlide01ButtonClicked);
            slide02Button.onClick.RemoveListener(OnSlide02ButtonClicked);
            slide03Button.onClick.RemoveListener(OnSlide03ButtonClicked);
        }

        private void Start()
        {
            SetCurrentSlide(1);
        }

        private void SetCurrentSlide(int slide)
        {
            switch (slide)
            {
                case 1:
                    slide01.SetActive(true);
                    slide02.SetActive(false);
                    slide03.SetActive(false);
                    break;

                case 2:
                    slide01.SetActive(false);
                    slide02.SetActive(true);
                    slide03.SetActive(false);
                    break;

                case 3:
                    slide01.SetActive(false);
                    slide02.SetActive(false);
                    slide03.SetActive(true);
                    break;

                default:
                    Debug.LogError("Illegal slide index");
                    break;
            }
        }

        private void OnSlide01ButtonClicked()
        {
            SetCurrentSlide(2);
        }

        private void OnSlide02ButtonClicked()
        {
            SetCurrentSlide(3);
        }

        private void OnSlide03ButtonClicked()
        {
            SceneManager.LoadScene(nextScene.name);
        }
    }
}
