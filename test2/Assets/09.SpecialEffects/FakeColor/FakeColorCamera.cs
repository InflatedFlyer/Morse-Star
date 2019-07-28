using UnityEngine;
using System.Collections;
//此脚本用于摄像机可使全屏变为假色，PS：需要在摄像机的脚本组件里拖入假色材质
[ExecuteInEditMode]
public class FakeColor : MonoBehaviour
{

    #region Variables  
    public Shader curShader;
    private Material curMaterial;

    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
        }

        if (!curShader && !curShader.isSupported)
        {
            enabled = false;
        }
    }

    void Update()
    {

    }

    void OnRenderImage(RenderTexture source, RenderTexture target)
    {
        if (curShader != null)
        {
            Graphics.Blit(source, target, material);
        }
        else
        {
            Graphics.Blit(source, target);
        }
    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}