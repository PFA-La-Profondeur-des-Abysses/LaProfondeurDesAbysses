using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AppareilPhoto : MonoBehaviour
{
    [SerializeField] private KeyCode startPhotoModeKey; //Touche qui sera utilisée pour lancer le mode photo
    [SerializeField] private KeyCode takePictureKey; //Touche qui sera utilisée pour prendre une photo
    private Camera cam; //variable stockant la caméra qui permettra à faire le screen

    public Image picture;

    void Awake()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);
        
        if(Input.GetKeyDown(startPhotoModeKey))
        {
            bool modeOn = !cam.gameObject.activeSelf;
            Time.timeScale = modeOn ? 0 : 1;
            cam.gameObject.SetActive(modeOn);
        }
        
        if (Input.GetKeyDown(takePictureKey) && cam.gameObject.activeSelf)
        {
            cam.targetTexture = RenderTexture.GetTemporary(400, 400, 16);
            StartCoroutine(TakeScreenshot());
        }
    }

    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = cam.targetTexture;
        RenderTexture.active = renderTexture;

        Texture2D renderResult =
            new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        renderResult.ReadPixels(rect, 0, 0);

        byte[] byteArray = renderResult.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/test.png", byteArray);

        RenderTexture.ReleaseTemporary(renderTexture);
        cam.targetTexture = null;

        Texture2D texture = new Texture2D(400, 400);
        bool isLoaded = texture.LoadImage(byteArray);
        if(isLoaded) picture.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        cam.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("TakePicture");
    }

    private void ReturnToNormalMode()
    {
        Time.timeScale = 1;
    }
}
