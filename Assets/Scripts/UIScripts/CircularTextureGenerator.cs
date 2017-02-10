using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CircularTextureGenerator : TextureGenerator {

    public Vector2 centerPositionOffset;
    public int radius;
    public int innerRadius;
    

    public int innerStartAngle;
    public int innerEndAngle;

    public int startAngle;
    public int endAngle;

    public Color textureColor;

    public override void Generate()
    {
        
        targetTexture.SetPixels(Enumerable.Repeat(Color.clear, targetTexture.width * targetTexture.height).ToArray());
        int centerX = (int)(targetTexture.width / 2 + centerPositionOffset.x);
        int centerY = (int)(targetTexture.height / 2 + centerPositionOffset.y);

        float r, a, px, py;
        Color32[] tempArray = targetTexture.GetPixels32();

        List<Vector2> points = new List<Vector2>();

        for (a = innerStartAngle; a <= innerEndAngle; a ++)
        {
            px = centerX + innerRadius * Mathf.Sin(a * Mathf.Deg2Rad);
            py = centerY + innerRadius * Mathf.Cos(a * Mathf.Deg2Rad);
            points.Add(new Vector2(px, py));
        }

        for (a = endAngle; a >= startAngle; a--)
        {
            px = centerX + radius * Mathf.Sin(a * Mathf.Deg2Rad);
            py = centerY + radius * Mathf.Cos(a * Mathf.Deg2Rad);
            points.Add(new Vector2(px, py));
        }

        targetTexture.FillPolygon(points.ToArray(), textureColor);
        targetTexture.Apply();
    }
}
