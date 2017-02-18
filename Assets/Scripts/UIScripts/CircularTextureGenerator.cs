using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class CircularTextureGenerator : TextureGenerator {

    public Vector2 centerPositionOffset;
    public int radius;
    public int thickness;
    
    [Range(0, 360)]
    public int startAngle;

    [Range(0, 360)]
    public int arcLengthDegrees;

    

    public override void Generate()
    {
        if (textureColors == null || textureColors.Length < 1)
        {
            Debug.Log("You must add at least one color to " + this.name);
            return;
        }
            

        targetTexture.SetPixels(Enumerable.Repeat(Color.clear, targetTexture.width * targetTexture.height).ToArray());
        int centerX = (int)(targetTexture.width / 2 + centerPositionOffset.x);
        int centerY = (int)(targetTexture.height / 2 + centerPositionOffset.y);

        Color32[] tempArray = targetTexture.GetPixels32();
        List<KeyValuePair<Color32, List<Vector2>>> polygonsByColor = new List<KeyValuePair<Color32, List<Vector2>>>();

        int colorCount = textureColors.Count();
        int percentageSum = textureColors.Sum(tc => tc.percentage);
        if (percentageSum > 100)
        {
            int delta = 100 - percentageSum;
            foreach (ColorPercentage cp in textureColors)
            {
                if ((cp.percentage + delta / textureColors.Length) > 100)
                    cp.percentage = 100;
                else if (cp.percentage + delta / textureColors.Length < 0)
                    cp.percentage = 0;
                else
                    cp.percentage += delta / textureColors.Length;
            }
        }

        float arcLengthGenerated = 0;
        for (int i = 0; i < textureColors.Length; i++)
        {
            float thisArcLength = ((float)textureColors[i].percentage/100f) * arcLengthDegrees;
            List<Vector2> points = this.GenerateSubSection(centerX, centerY, startAngle + arcLengthGenerated, thisArcLength);

            arcLengthGenerated += thisArcLength;

            polygonsByColor.Add(new KeyValuePair<Color32, List<Vector2>>(textureColors[i].color, GetPolygonPoints(points.ToArray())));
        }

        foreach (KeyValuePair<Color32, List<Vector2>> polygon in polygonsByColor)
        {
            foreach (Vector2 point in polygon.Value)
            {
                //targetTexture.SetPixel((int)point.x, (int)point.y, polygon.Key);
                int index = ((int)point.y * textureWidth) + (int)point.x;
                
                tempArray[index] = polygon.Key;
            }
        }
        targetTexture.SetPixels32(tempArray);
        targetTexture.Apply();
    }

    private List<Vector2> GenerateSubSection(int centerX, int centerY, float startAngle, float arcLengthDegrees)
    {
        float a, px, py;
        List<Vector2> points = new List<Vector2>();
        for (a = startAngle; a <= startAngle + arcLengthDegrees + 1; a++)
        {
            px = centerX + (radius - thickness) * Mathf.Sin(a * Mathf.Deg2Rad);
            py = centerY + (radius - thickness) * Mathf.Cos(a * Mathf.Deg2Rad);

            px = Mathf.Clamp(px, 0, targetTexture.width-1);
            py = Mathf.Clamp(py, 0, targetTexture.height-1);

            points.Add(new Vector2(px, py));
        }

        for (a = startAngle + arcLengthDegrees + 1; a >= startAngle; a--)
        {
            px = centerX + radius * Mathf.Sin(a * Mathf.Deg2Rad);
            py = centerY + radius * Mathf.Cos(a * Mathf.Deg2Rad);

            px = Mathf.Clamp(px, 0, targetTexture.width-1);
            py = Mathf.Clamp(py, 0, targetTexture.height-1);

            points.Add(new Vector2(px, py));
        }
        return points;
    }
}
