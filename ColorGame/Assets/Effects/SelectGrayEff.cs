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
    public ColorIncreasor[] Increasors;
    float DeltaT;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, EffectMaterial);
        // exclude layers
        Camera cam = null;
        if (excludeLayers.value != 0) cam = GetTmpCam();

        if (cam && excludeLayers.value != 0)
        {
            cam.targetTexture = dest;
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
    private void Start()
    {
        Increasors = new ColorIncreasor[9];
        Increasors[2] = new ColorIncreasor("_Red");
        Increasors[3] = new ColorIncreasor("_Orange");
        Increasors[4] = new ColorIncreasor("_Yellow");
        Increasors[5] = new ColorIncreasor("_Green");
        Increasors[6] = new ColorIncreasor("_Blue");
        Increasors[7] = new ColorIncreasor("_Purple");
        for(int i = 2; i < 8; i++)
        {
            EffectMaterial.SetFloat(Increasors[i].ID, 0);
        }
        DeltaT = Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        for (int i = (int)Colors.ColorEnum.Red; i < (int)Colors.ColorEnum.Purple + 1; i++)
        {
            if (Increasors[i].increasing)
            {
                EffectMaterial.SetFloat(Increasors[i].ID, EffectMaterial.GetFloat(Increasors[i].ID) + Increasors[i].interval);
                if (EffectMaterial.GetFloat(Increasors[i].ID) >= 1)
                {
                    EffectMaterial.SetFloat(Increasors[i].ID, 1);
                    Increasors[i].increasing = false;
                }
            }
        }
    }
    public void IncreaseColor(int Color, float inTime)
    {
        Increasors[Color].increasing = true;
        Increasors[Color].interval = (1 - EffectMaterial.GetFloat(Increasors[Color].ID))*DeltaT/inTime;
    }
    public struct ColorIncreasor
    {
        public string ID;//never write to this except for initialization.
        public bool increasing;
        public float interval;
        public ColorIncreasor(string Name)
        {
            ID = Name;
            increasing = false;
            interval = 0;
        }
    }
    void OnGUI()
    {
        GUI.depth = 0;
    }
}
