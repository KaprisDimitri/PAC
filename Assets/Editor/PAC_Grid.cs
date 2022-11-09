using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PAC_Grid
{
    private Texture2D _pixelArtTexture;

    public Texture2D PixelArtTexture
    {
        get { return _pixelArtTexture; }
    }

    private string _projectName;

    private int _sizeCell;
    private int _height;
    private int _width;

    private Rect _rectGroupeProject;

    private Rect _recteGrid;

    public PAC_Grid (int height, int width, string projectName, Rect rectGroupeProject, Rect rectGrid)
    {
        _height = height;
        _width = width;

        _projectName = projectName;

        _sizeCell = 10;
        _recteGrid = rectGrid;
        _rectGroupeProject = rectGroupeProject;

        InitPixelArtTexture();
    }

    private void InitPixelArtTexture ()
    {
        _pixelArtTexture = new Texture2D(_width, _height);
        for(int x = 0; x<_width;x++ )
        {
            for (int y = 0; y < _height; y++)
            {
                _pixelArtTexture.SetPixel(x, y, new Color(1, 0, 0, 1));
            }
        }
        _pixelArtTexture = SaveTemp(_projectName, _pixelArtTexture);
    }

    public void ShowOnGUI ()
    {
        GUI.BeginGroup(_recteGrid);
        
            GUI.DrawTexture(new Rect(0, 0, _width * _sizeCell, _height * _sizeCell), _pixelArtTexture, ScaleMode.StretchToFill, true, _sizeCell);
        
        GUI.EndGroup();
    }

    Texture2D SaveTemp(string pathName, Texture2D textureToSaveTemps)
    {
        textureToSaveTemps.Apply();
           /* string path = Application.persistentDataPath + pathName + ".png";
            byte[] pngData = textureToSaveTemps.EncodeToPNG();
            File.WriteAllBytes(path, pngData);


            var fileContent = File.ReadAllBytes(path);
            textureToSaveTemps.LoadImage(fileContent);*/
            textureToSaveTemps.filterMode = FilterMode.Point;
            textureToSaveTemps.wrapMode = TextureWrapMode.Clamp;
            return textureToSaveTemps;
       
        //_pixelArtTexture.Compress(false);

    }


    /// <summary>
    /// Fonction qui change la position 0,0 a la position 0,0 de la grille
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public Vector2 ScreenOriginToGridOrigin (Vector2 mousePos)
    {
        return new Vector2(mousePos.x - _rectGroupeProject.x, mousePos.y - _rectGroupeProject.y);
    }

    /// <summary>
    /// Fonction qui renvoie si la souris est dans la grille Il faut faire passer la valeur dans la fonction ScreenPosToGridPos
    /// </summary>
    /// <param name="mouseOrigGrid">Position de la souris avec la meme origine que la grille </param>
    /// <returns></returns>
    public bool IsMouseInGrid (Vector2 mouseOrigGrid)
    {
        
        if(mouseOrigGrid.x > _width * _sizeCell || mouseOrigGrid.y > _height * _sizeCell || mouseOrigGrid.x < 0 || mouseOrigGrid.y < 0)
        {
            return false;
        }
        
        return true;
    }

    public Vector2 ScreenPosToGridPos (Vector2 mouseOrigGrid)
    {
        return new Vector2(mouseOrigGrid.x / _sizeCell , (_height )-(mouseOrigGrid.y / (_sizeCell)));
    }

    public void ColorPixels (Vector2[] pixelToColor, Color colorToSet)
    {
        for(int i = 0; i<pixelToColor.Length;i++)
        {
           if(_pixelArtTexture.GetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y) != colorToSet)
            {
                _pixelArtTexture.SetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y, colorToSet);
            }
        }

        _pixelArtTexture = SaveTemp(_projectName, _pixelArtTexture);
    }

    public void SubOpacityPixel(Vector2[] pixelToColor, float opacity)
    {
        for (int i = 0; i < pixelToColor.Length; i++)
        {
            Color colorpixel = _pixelArtTexture.GetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y);
            colorpixel.a = opacity;
            
            if (_pixelArtTexture.GetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y).a > opacity && _pixelArtTexture.GetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y).a != opacity)
            {
                _pixelArtTexture.SetPixel((int)pixelToColor[i].x, (int)pixelToColor[i].y, colorpixel);
            }
            
        }

        _pixelArtTexture = SaveTemp(_projectName, _pixelArtTexture);
    }

    private bool CheckIfIsInGrid (Vector2 index)
    {
        if (index.x < 0 || index.y < 0 || index.x >= _width || index.y >= _height)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Vector2[] GetPixelWithSize (Vector2 mouseGridPos,int size)
    {
        Vector2[] gridToReturn = new Vector2[size * size];
        int t = 0;
        int t2 = 0;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector2 index = new Vector2((mouseGridPos.x - (int)(size / 2)) + x, (mouseGridPos.y - (int)(size / 2)) + y);
                if (CheckIfIsInGrid(index))
                {

                    gridToReturn[t2] = index;
                    t++;
                }
                else
                {
                    gridToReturn[t2] = new Vector2(-1, -1);
                }
                t2++;
            }
        }

        Vector2[] gridTemps = gridToReturn;
        gridToReturn = new Vector2[t];
        t = 0;
        for (int i = 0; i < gridTemps.Length; i++)
        {
            if (gridTemps[i].x != -1)
            {
                gridToReturn[t] = gridTemps[i];
                t++;
            }
        }


        return gridToReturn;
    }

    public Texture2D GetTextureFromVectors (Vector2[] indexRef, Color color)
    {
        Vector2 indexRefOrigine = indexRef[0];
        for(int i = 0; i< indexRef.Length;i++ )
        {
            
            indexRef[i].x -= indexRefOrigine.x;
            indexRef[i].y -= indexRefOrigine.y;
        }
        int width = (int)(indexRef[indexRef.Length - 1].x)+1;
        int height = (int)(indexRef[indexRef.Length - 1].y)+1;
        Debug.Log(width + "+" + height);
        Texture2D folTexure = new Texture2D(width * _sizeCell, height * _sizeCell);

        for (int x = 0; x < folTexure.width; x++)
        {
            for (int y = 0; y < folTexure.height; y++)
            {
                folTexure.SetPixel(x, y, color);
            }
        }


        return SaveTemp("FollowTexture", folTexure);
    }
}
