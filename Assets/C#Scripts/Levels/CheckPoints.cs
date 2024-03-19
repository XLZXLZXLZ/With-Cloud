using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoints : Singleton<CheckPoints>
{
    private int index;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += CheckingPoint;
    }

    public void UpdateIndex(Transform t)
    {
        int _index = 0;
        var c = CameraActions.Instance;
        foreach (var s in c.savePoints)
            _index += t.position.x > s ? 1 : 0;
        index = Mathf.Max(index, _index);
    }

    public void CheckingPoint(Scene scene,LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            Destroy(gameObject);
            SceneManager.sceneLoaded -= CheckingPoint;
        }
        var c = CameraActions.Instance;
        if(index > 0)
        {
            for (int i = 0; i < c.checkItems.Length; i++)
            {
                c.checkItems[i].transform.position = c.checkPoints[(index-1) * c.checkItems.Length + i];
                if (c.checkItems[i].TryGetComponent<ChasingBorad>(out var board))
                    board.CheckOn();
            }
        }
    }
}
