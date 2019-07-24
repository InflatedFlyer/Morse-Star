using UnityEngine;

public class Block : MonoBehaviour
{
    public float blockBorder;
    public Texture2D currentTexture;
    public Texture2D startTexture;
    [HideInInspector] public int down;
    [HideInInspector] public int endX;
    [HideInInspector] public int endY;
    [HideInInspector] public bool[] isColored;
    [HideInInspector] public bool[] isTransparent;
    [HideInInspector] public int left;
    [HideInInspector] public int right;
    [HideInInspector] public int startX;
    [HideInInspector] public int startY;
    [HideInInspector] public int up;

    private int a;
    private int b;
    private float scale;
    private Color[] startColors;
    private Color transparent;

    private void Start()
    {
        isColored = new bool[currentTexture.height * currentTexture.width];
        startColors = startTexture.GetPixels();
        for (a = 0; a < startColors.Length; ++a)
        {
            if (startColors[a].a < blockBorder)
            {
                isColored[a] = false;
            }
            else
            {
                isColored[a] = true;
            }
        }
        currentTexture.SetPixels(startColors);
        currentTexture.Apply();
        isTransparent = new bool[currentTexture.height * currentTexture.width];
        scale = 100f / transform.localScale.x;
        transparent = new Color(0f, 0f, 0f, 0f);
    }

    public void Render()
    {
        for (a = 0; a < currentTexture.height; ++a)
        {
            for (b = 0; b < currentTexture.width; ++b)
            {
                if (isTransparent[a * currentTexture.width + b])
                {
                    currentTexture.SetPixel(b, a, transparent);
                }
                else
                {
                    currentTexture.SetPixel(b, a, startColors[a * currentTexture.width + b]);
                }
            }
        }
        currentTexture.Apply();
    }

    public void Transfer()
    {
        for (a = 0; a < currentTexture.height; ++a)
        {
            for (b = 0; b < currentTexture.width; ++b)
            {
                isTransparent[a * currentTexture.width + b] = false;
            }
        }
        down = (int)(transform.position.y * scale);
        left = (int)(transform.position.x * scale);
        right = currentTexture.width + left - 1;
        up = currentTexture.height + down - 1;
    }
}
