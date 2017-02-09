using UnityEngine;
using System.Collections;
using System;

public class CircularTextureGenerator : TextureGenerator {

    public Vector2 centerPositionOffset;
    public int radius;
    public int innerRadius;
    public int startAngle;
    public int endAngle;
    public Color textureColor;

    public override void Generate()
    {
        int centerX = (int)(targetTexture.width / 2 + centerPositionOffset.x);
        int centerY = (int)(targetTexture.height / 2 + centerPositionOffset.y);

        float r, a, px, py;
        Color32[] tempArray = targetTexture.GetPixels32();

        for (r = innerRadius; r <= radius; r+= .5f)
        {
            for (a = startAngle; a <= endAngle; a+= .5f)
            {

                px = centerX + r * Mathf.Sin(a * Mathf.Deg2Rad);
                py = centerY + r * Mathf.Cos(a * Mathf.Deg2Rad);

                targetTexture.SetPixel((int)px, (int)py, textureColor);
                //tempArray[(int)(py * targetTexture.height + px)] = textureColor;
            }
        }
        //targetTexture.SetPixels32(tempArray);
        targetTexture.Apply();
    }
}
