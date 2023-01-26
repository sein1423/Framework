using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//건들지 말것
public class LodingManager : MonoBehaviour
{

    public Slider mProgressBar;

    private bool isDone = false;

    private void Awake()
    {
        Global.Instance.b_isLoading = true;
    }

    void Start()
    {
        Debug.Log($"{Global.Instance.NowScene} : {Global.Instance.NextScene}");
        SceneManager.UnloadSceneAsync(Global.Instance.NowScene);

        //if(Global.Instance.NowScene == DataTable.SceneTable.STR_SCENE_GAMEPAGE) SceneManager.UnloadSceneAsync(Global.Instance.StageNumber); 
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync(Global.Instance.NextScene, LoadSceneMode.Additive);
        async.allowSceneActivation = false;

        float _timer = 0.0f;

        while (!async.isDone)
        {
            yield return null;

            _timer += Time.deltaTime;

            if (async.progress >= 0.9f)
            {

                float _fillamount = Mathf.Lerp(mProgressBar.value, 1f, _timer);
                mProgressBar.value = _fillamount;

                if (mProgressBar.value == 1.0f)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                float _fillamount = Mathf.Lerp(mProgressBar.value, async.progress, _timer);
                mProgressBar.value = _fillamount;

                if (mProgressBar.value >= async.progress)
                {
                    _timer = 0f;
                }
            }
        }

        if (async.isDone == true)
        {
            isDone = async.isDone;
            Global.Instance.b_isLoading = false;
            Global.Instance.NowScene = Global.Instance.NextScene;
            Global.Instance.NextScene = null;
            SceneManager.UnloadSceneAsync(DataTable.SceneTable.STR_SCENE_LODING);
        }

    }
}
