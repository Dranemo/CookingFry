using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureManager : MonoBehaviour
{
    [SerializeField] GameObject renderCamera;
    RenderTexture renderTexture;


    // Start is called before the first frame update
    void Start()
    {
        renderCamera.SetActive(false);
    }



    public void CaptureRenderTexture(RenderTexture renderTexture, Vector3 posCam, Quaternion rotCam, LayerMask layerMask)
    {
        StartCoroutine(WaitEnOfFrameCapture(renderTexture, posCam, rotCam, layerMask));
    }

    IEnumerator WaitEnOfFrameCapture(RenderTexture renderTexture, Vector3 posCam, Quaternion rotCam, LayerMask layerMask)
    {
        yield return new WaitForEndOfFrame();

        renderCamera.transform.position = posCam;
        renderCamera.transform.rotation = rotCam;
        renderCamera.SetActive(true);
        renderCamera.GetComponent<Camera>().targetTexture = renderTexture;
        renderCamera.GetComponent<Camera>().cullingMask = layerMask;
        renderCamera.GetComponent<Camera>().Render();
        renderCamera.SetActive(false);
    }
}
