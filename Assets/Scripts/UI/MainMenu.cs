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
    public GameObject explanationText;
    public GameObject clickText;
    public Button button;

    void Update()
    {
        if(Input.anyKeyDown && clickText.activeSelf) Unload();
    }
    
    public void Play(int index = 1)
    {
        button.onClick.RemoveAllListeners();
        fadeIn.SetTrigger("FadeIn");
        StartCoroutine(LoadGameScene(index));
    }

    private IEnumerator LoadGameScene(int index = 1)
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
        var loading = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        while (!loading.isDone)
        {
            slider.value = loading.progress;
            yield return null;
        }
        slider.gameObject.SetActive(false);
        clickText.SetActive(true);
        if(index != 1) explanationText.SetActive(false);
    }

    private void Unload()
    {
        Debug.Log("unload");
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(0);
    }

    public void Leave()
    {
        Application.Quit();
    }
}
