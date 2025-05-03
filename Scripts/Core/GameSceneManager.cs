
using UnityEngine;
using UnityEngine.SceneManagement;


    public class GameSceneManager : MonoBehaviour
    {
        public static GameSceneManager Instance;
        public static string Plotcode ="";

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 如果你希望它持久
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLevel(string levelName)
        {
            if (IsSceneInBuildSettings(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogError($"[GameSceneManager] Scene '{levelName}' is not in Build Settings or name is incorrect.");
            }
        }

        public void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        private bool IsSceneInBuildSettings(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);

            if (name == sceneName)
                return true;
        }

        return false;
    }
    public void SwitchToNext(){
        string levelName = GetLevelName();

        LoadLevel(levelName);
    }
    public string GetLevelName(){
        //Debug.Log("Entered GetLevel Name");
        string nextSceneName = "";
        switch (Plotcode)
        {
            case "0":
                nextSceneName = "0-11";
                break;

            case "1":
                nextSceneName = "0-12";
                break;

            case "00":
                nextSceneName = "11-21";
                break;

            case "01":
                nextSceneName = "11-22";
                break;
            case "10":
                nextSceneName = "12-21";
                break;
            case "11":
                nextSceneName = "12-22";
                break;
            case "001":
                nextSceneName = "HiddenEnd";
                break;
            case "011":
                nextSceneName = "HiddenEnd";
                break;
            case "101":
                nextSceneName = "HiddenEnd";
                break;
            case "111":
                nextSceneName = "BadEnd";
                break;
            case "000":
                nextSceneName = "NormalEnd";
                break;
            case "010":
                nextSceneName = "GoodEnd";
                break;
            case "100":
                nextSceneName = "NormalEnd";
                break;
            case "110":
                nextSceneName = "NormalEnd";
                break;
        }





        return nextSceneName;
    }
}
    
    

