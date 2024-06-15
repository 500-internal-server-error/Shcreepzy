using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void Cutscene()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void Materi()
    {
        SceneManager.LoadScene("Materi");
    }
}
