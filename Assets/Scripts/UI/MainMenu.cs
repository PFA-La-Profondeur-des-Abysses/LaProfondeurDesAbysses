using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator fadeIn;
    public Slider slider;
    public GameObject clickText;
    public Button button;

    void Update()
    {
        if(Input.anyKeyDown && clickText.activeSelf) Unload();
    }
    
    public void Play()
    {
        fadeIn.SetTrigger("FadeIn");
        StartCoroutine(LoadGameScene());
        button.onClick.RemoveAllListeners();
    }

    private IEnumerator LoadGameScene()
    {
        Time.timeScale = 0;
        var loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!loading.isDone)
        {
            slider.value = loading.progress;
            yield return null;
        }
        slider.gameObject.SetActive(false);
        clickText.SetActive(true);
    }

    private void Unload()
    {
        Debug.Log("unload");
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(0);
    }
}
