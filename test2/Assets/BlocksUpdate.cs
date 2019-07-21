using UnityEngine;

public class BlocksUpdate : MonoBehaviour
{
    public Block[] blocks;

    private int a;
    private int b;
    private int c;
    private int d;
    private int e;
    private int f;
    private int offsetX;
    private int offsetY;

    private void Update()
    {
        //计算相交部分并进行反色
        for (a = 0; a < blocks.Length; ++a)
        {
            blocks[a].Transfer();
        }
        for (a = 0; a < blocks.Length - 1; ++a)
        {
            for (b = a + 1; b < blocks.Length; ++b)
            {
                if (blocks[a].down < blocks[b].down)
                {
                    c = a;
                    d = b;
                }
                else
                {
                    c = b;
                    d = a;
                }
                if (blocks[c].up > blocks[d].down)
                {
                    blocks[c].startY = blocks[d].down - blocks[c].down;
                    blocks[d].startY = 0;
                    if (blocks[c].up > blocks[d].up)
                    {
                        blocks[c].endY = blocks[d].up - blocks[c].down;
                        blocks[d].endY = blocks[d].currentTexture.height - 1;
                    }
                    else
                    {
                        blocks[c].endY = blocks[c].currentTexture.height - 1;
                        blocks[d].endY = blocks[c].up - blocks[d].down;
                    }
                }
                else
                {
                    continue;
                }
                if (blocks[a].left < blocks[b].left)
                {
                    c = a;
                    d = b;
                }
                else
                {
                    c = b;
                    d = a;
                }
                if (blocks[c].right > blocks[d].left)
                {
                    blocks[c].startX = blocks[d].left - blocks[c].left;
                    blocks[d].startX = 0;
                    if (blocks[c].right > blocks[d].right)
                    {
                        blocks[c].endX = blocks[d].right - blocks[c].left;
                        blocks[d].endX = blocks[d].currentTexture.width - 1;
                    }
                    else
                    {
                        blocks[c].endX = blocks[c].currentTexture.width - 1;
                        blocks[d].endX = blocks[c].right - blocks[d].left;
                    }
                }
                else
                {
                    continue;
                }
                offsetX = blocks[a].startX - blocks[b].startX;
                offsetY = blocks[a].startY - blocks[b].startY;
                for (c = blocks[b].startX; c <= blocks[b].endX; ++c)
                {
                    for (d = blocks[b].startY; d <= blocks[b].endY; ++d)
                    {
                        e = blocks[a].currentTexture.width * (d + offsetY) + c + offsetX;
                        f = blocks[b].currentTexture.width * d + c;
                        try
                        {
                            if (blocks[a].isColored[e] && blocks[b].isColored[f])
                            {
                                blocks[a].isTransparent[e] = !blocks[a].isTransparent[e];
                                blocks[b].isTransparent[f] = !blocks[b].isTransparent[f];
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                }
            }
        }
        for (a = 0; a < blocks.Length; ++a)
        {
            blocks[a].Render();
        }
    }
}
