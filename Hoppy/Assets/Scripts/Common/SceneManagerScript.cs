using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "SceneManagerScript", menuName = "Scriptable Objects/SceneManagerScript")]
public class SceneManagerScript : ScriptableObject
{
    [SerializeField] private string m_shopSceneName = "ShopScene";
    [SerializeField] private string m_gameSceneName = "GameScene";

    public void SwitchToShop()
    {
        SwitchScene(m_shopSceneName);
    }

   public void SwitchToGame()
    {

        SwitchScene(m_gameSceneName);
    }

    private void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
