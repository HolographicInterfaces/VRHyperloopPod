using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public abstract class TextureGenerator : MonoBehaviour {

    public int textureWidth;
    public int textureHeight;
    public bool useTargetImageSizeForTextureSize;
    protected Texture2D targetTexture;

    public virtual void Initialize()
    {
        if (useTargetImageSizeForTextureSize)
        {
            textureWidth = (int)this.GetComponent<RawImage>().rectTransform.rect.size.x;
            textureHeight = (int)this.GetComponent<RawImage>().rectTransform.rect.size.y;
        }
        targetTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false, true);
    }

    public virtual void Apply()
    {
        this.GetComponent<RawImage>().texture = targetTexture;
    }

	void Start ()
    {
        Initialize();
        Generate();
        Apply();

    }

    public abstract void Generate();

    void OnValidate()
    {
        Initialize();
        Generate();
        Apply();
    }
}
