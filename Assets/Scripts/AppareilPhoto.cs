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
    [SerializeField] private Image rapportImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private RapportManager rapport;

    public Image picture;

    private bool modeOn;

    void Awake()
    {
        cam = transform.GetChild(0).GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
        }
        
        /*if(Input.GetKeyDown(startPhotoModeKey))
        {
            modeOn = !cam.gameObject.activeSelf;
            Time.timeScale = modeOn ? 0 : 1;
            cam.gameObject.SetActive(modeOn);
        }*/
        
        if (Input.GetKeyDown(takePictureKey) && cam.gameObject.activeSelf)
        {
            cam.targetTexture = RenderTexture.GetTemporary(400, 400, 16);
            StartCoroutine(TakeScreenshot());
        }
    }

    public void onClick()
    {
        modeOn = true;
        cam.gameObject.SetActive(modeOn);
        rapport.CloseCurrentPage();
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
        File.WriteAllBytes(Application.dataPath + "/test.png", byteArray);

        RenderTexture.ReleaseTemporary(renderTexture);
        cam.targetTexture = null;

        Texture2D texture = new Texture2D(400, 400);
        bool isLoaded = texture.LoadImage(byteArray);
        if(isLoaded)
        {
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            picture.sprite = sprite;
            rapport.GetCurrentPageImage().sprite = sprite;
        }
        cam.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("TakePicture");
    }

    private void ReturnToNormalMode()
    {
        rapport.transform.GetChild(0).gameObject.SetActive(true);
        rapport.OpenCurrentPage();
        rapport.FillPage();
        picture.gameObject.SetActive(false);
    }
}
