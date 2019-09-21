using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown_ImageResolution : MonoBehaviour
{
    /// <summary>
    /// このスクリプトをアタッチしたDropdown
    /// </summary>
    private UnityEngine.UI.Dropdown m_Dropdown = null;

    [SerializeField]
    private UnityEngine.Camera m_MainCamera = null;

    [SerializeField]
    private UnityEngine.UI.RawImage m_TextureTarget = null;

    private UnityEngine.RenderTexture m_RTexture = null;

    // Use this for initialization
    void Start()
    {
        m_Dropdown = gameObject.GetComponent<UnityEngine.UI.Dropdown>() as UnityEngine.UI.Dropdown;
        m_RTexture = m_MainCamera.targetTexture;
    }

    public void OnDropdownValueChange()
    {
        int v = m_Dropdown.value;
        int height = 1, width = 1;
        switch (v)
        {
            case 0:                 //1440p
                height = 1440;
                width = 2560;
                break;
            case 1:                 //1080p
                height = 1080;
                width = 1920;
                break;
            case 2:                 //720p
                height = 720;
                width = 1280;
                break;
            case 3:                 //480p
                height = 340;
                width = 480;
                break;
            case 4:                 //360p
                height = 360;
                width = 640;
                break;
            case 5:                 //240p
                height = 240;
                width = 427;
                break;
            case 6:                 //144p
                height = 144;
                width = 256;
                break;
        }

        Debug.Log("width=" + width + " : height=" + height);

        m_RTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);


        m_MainCamera.targetTexture = m_RTexture;

        m_TextureTarget.texture = m_RTexture;

    }
}
