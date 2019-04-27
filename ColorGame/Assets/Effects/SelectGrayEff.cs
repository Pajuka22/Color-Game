using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SelectGrayEff : MonoBehaviour
{
    public Material EffectMaterial;
    public LayerMask excludeLayers = 0;
    private GameObject tmpCam = null;
    private Camera _camera;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //aterial.SetTexture("_RampTex", textureRamp);
        //material.SetFloat("_RampOffset", rampOffset);
        Graphics.Blit(source, destination, EffectMaterial);

        // exclude layers
        Camera cam = null;
        if (excludeLayers.value != 0) cam = GetTmpCam();

        if (cam && excludeLayers.value != 0)
        {
            cam.targetTexture = destination;
            cam.cullingMask = excludeLayers;
            cam.Render();
        }
    }
    Camera GetTmpCam()
    {
        if (tmpCam == null)
        {
            if (_camera == null) _camera = GetComponent<Camera>();

            string name = "_" + _camera.name + "_GrayScaleTmpCam";
            GameObject go = GameObject.Find(name);

            if (null == go) // couldn't find, recreate
            {
                tmpCam = new GameObject(name, typeof(Camera));
            }
            else
            {
                tmpCam = go;
            }
        }

        tmpCam.hideFlags = HideFlags.DontSave;
        tmpCam.transform.position = _camera.transform.position;
        tmpCam.transform.rotation = _camera.transform.rotation;
        tmpCam.transform.localScale = _camera.transform.localScale;
        tmpCam.GetComponent<Camera>().CopyFrom(_camera);

        tmpCam.GetComponent<Camera>().enabled = false;
        tmpCam.GetComponent<Camera>().depthTextureMode = DepthTextureMode.None;
        tmpCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;

        return tmpCam.GetComponent<Camera>();
    }
}