using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

[RequireComponent(typeof(RawImage))]
public abstract class TextureGenerator : MonoBehaviour {

    public int textureWidth;
    public int textureHeight;
    public bool useTargetImageSizeForTextureSize;
    protected Texture2D targetTexture;
    public string savePath = "Assets/";
    public string textureName = "MyTexture";
    public ColorPercentage[] textureColors;

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
        Rebuild();

    }

    public abstract void Generate();

    void OnValidate()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        Initialize();
        Generate();
        Apply();
    }

    public List<Vector2> GetPolygonPoints(Vector2[] polygonPoints)
    {
        // http://alienryderflex.com/polygon_fill/

        List<Vector2> points = new List<Vector2>();

        var IMAGE_BOT = (int)polygonPoints.Max(p => p.y);
        var IMAGE_TOP = (int)polygonPoints.Min(p => p.y);
        var IMAGE_LEFT = (int)polygonPoints.Min(p => p.x);
        var IMAGE_RIGHT = (int)polygonPoints.Max(p => p.x);
        var MAX_POLY_CORNERS = polygonPoints.Count();
        var polyCorners = MAX_POLY_CORNERS;
        var polyY = polygonPoints.Select(p => p.y).ToArray();
        var polyX = polygonPoints.Select(p => p.x).ToArray();
        int[] nodeX = new int[MAX_POLY_CORNERS];
        int nodes, i, j, swap;

        //  Loop through the rows of the image.
        for (int pixelY = IMAGE_TOP; pixelY <= IMAGE_BOT; pixelY++)
        {

            //  Build a list of nodes.
            nodes = 0;
            j = polyCorners - 1;
            for (i = 0; i < polyCorners; i++)
            {
                if (polyY[i] < (float)pixelY && polyY[j] >= (float)pixelY || polyY[j] < (float)pixelY && polyY[i] >= (float)pixelY)
                {
                    nodeX[nodes++] = (int)(polyX[i] + (pixelY - polyY[i]) / (polyY[j] - polyY[i]) * (polyX[j] - polyX[i]));
                }
                j = i;
            }

            //  Sort the nodes, via a simple “Bubble” sort.
            i = 0;
            while (i < nodes - 1)
            {
                if (nodeX[i] > nodeX[i + 1])
                {
                    swap = nodeX[i]; nodeX[i] = nodeX[i + 1]; nodeX[i + 1] = swap; if (i != 0) i--;
                }
                else
                {
                    i++;
                }
            }

            //  Fill the pixels between node pairs.
            for (i = 0; i < nodes; i += 2)
            {
                if (nodeX[i] >= IMAGE_RIGHT)
                    break;
                if (nodeX[i + 1] > IMAGE_LEFT)
                {
                    if (nodeX[i] < IMAGE_LEFT)
                        nodeX[i] = IMAGE_LEFT;
                    if (nodeX[i + 1] > IMAGE_RIGHT)
                        nodeX[i + 1] = IMAGE_RIGHT;
                    for (j = nodeX[i]; j < nodeX[i + 1]; j++)
                    {
                        points.Add(new Vector2(j, pixelY));
                    }
                }
            }
        }
        return points;
    }

    public void SaveTexture()
    {
        string filePath = Application.dataPath + savePath + (savePath.EndsWith("/")?"":"/") + textureName + ".png";
        Debug.Log("Writing Texture to " + filePath);
        byte[] textureByteData = targetTexture.EncodeToPNG();
        if(File.Exists(filePath))
        {
            Debug.Log("Error! Texture already exists at that path. Choose a different path or remove the existing file");
        } else
        {
            File.WriteAllBytes(filePath, textureByteData);
        }
    }

    [Serializable]
    public class ColorPercentage
    {
        public Color color;
        [Range(1,100)]
        public int percentage;

        public ColorPercentage(Color color, int percentage)
        {
            this.color = color;
            this.percentage = percentage;
        }
    }
}
