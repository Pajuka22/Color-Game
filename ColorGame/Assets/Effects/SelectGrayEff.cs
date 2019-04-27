using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SelectGrayEff : MonoBehaviour
{
    public Material EffectMaterial;
    public ColorIncreasor IRed;
    public ColorIncreasor IOrange;
    public ColorIncreasor IYellow;
    public ColorIncreasor IGreen;
    public ColorIncreasor IBlue;
    public ColorIncreasor IPurple;
    float DeltaT;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, EffectMaterial);
    }
    private void Start()
    {
        IRed = new ColorIncreasor("_Red");
        IOrange = new ColorIncreasor("_Orange");
        IYellow = new ColorIncreasor("_Yellow");
        IGreen = new ColorIncreasor("_Green");
        IBlue = new ColorIncreasor("_Blue");
        IPurple = new ColorIncreasor("_Purple");
        EffectMaterial.SetFloat(IRed.ID, 0);
        EffectMaterial.SetFloat(IOrange.ID, 0);
        EffectMaterial.SetFloat(IYellow.ID, 0);
        EffectMaterial.SetFloat(IGreen.ID, 0);
        EffectMaterial.SetFloat(IBlue.ID, 0);
        EffectMaterial.SetFloat(IPurple.ID, 0);
        DeltaT = Time.fixedDeltaTime;
    }
    private void FixedUpdate()
    {
        if (IRed.increasing)
        {
            EffectMaterial.SetFloat(IRed.ID, EffectMaterial.GetFloat(IRed.ID) + IRed.interval);
            if(EffectMaterial.GetFloat(IRed.ID) >= 1)
            {
                EffectMaterial.SetFloat(IRed.ID, 1);
                IRed.increasing = false;
            }
        }
        if (IOrange.increasing)
        {
            EffectMaterial.SetFloat(IOrange.ID, EffectMaterial.GetFloat(IOrange.ID) + IOrange.interval);
            if(EffectMaterial.GetFloat(IOrange.ID) >= 1)
            {
                EffectMaterial.SetFloat(IOrange.ID, 1);
                IOrange.increasing = false;
            }
        }
        if (IYellow.increasing)
        {
            EffectMaterial.SetFloat(IYellow.ID, EffectMaterial.GetFloat(IYellow.ID) + IYellow.interval);
            if (EffectMaterial.GetFloat(IYellow.ID) >= 1)
            {
                EffectMaterial.SetFloat(IYellow.ID, 1);
                IYellow.increasing = false;
            }
        }
        if (IGreen.increasing)
        {
            EffectMaterial.SetFloat(IGreen.ID, EffectMaterial.GetFloat(IGreen.ID) + IGreen.interval);
            if (EffectMaterial.GetFloat(IGreen.ID) >= 1)
            {
                EffectMaterial.SetFloat(IGreen.ID, 1);
                IGreen.increasing = false;
            }
        }
        if (IBlue.increasing)
        {
            EffectMaterial.SetFloat(IBlue.ID, EffectMaterial.GetFloat(IBlue.ID) + IBlue.interval);
            if (EffectMaterial.GetFloat(IBlue.ID) >= 1)
            {
                EffectMaterial.SetFloat(IBlue.ID, 1);
                IBlue.increasing = false;
            }
        }
        if (IPurple.increasing)
        {
            EffectMaterial.SetFloat(IPurple.ID, EffectMaterial.GetFloat(IPurple.ID) + IPurple.interval);
            if (EffectMaterial.GetFloat(IPurple.ID) >= 1)
            {
                EffectMaterial.SetFloat(IPurple.ID, 1);
                IPurple.increasing = false;
            }
        }
    }
    public void IncreaseColor(ref ColorIncreasor Color, float inTime)
    {
        Color.increasing = true;
        Color.interval = (1 - EffectMaterial.GetFloat(Color.ID));
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
}
