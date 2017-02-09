using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public abstract class TextureGenerator : MonoBehaviour {

    public int textureWidth;
    public int textureHeight;

    protected Texture2D targetTexture;

    public virtual void Initialize()
    {
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
