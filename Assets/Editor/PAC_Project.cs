using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAC_Project
{
    private PAC_MainWindow _window;

    private string _projectName;


    private string _projectPath;

    private Rect _rectGroupeProject;
    #region Variable Pour Grille

    private Rect _rectGrid;
   
    private int _width;
    private int _height;

    private PAC_Grid _gridPixelArt;

    public PAC_Grid GridPixelArt
    {
        get { return _gridPixelArt; }
    }
    #endregion

    #region Variable OverView
    private Rect _rectOverView;
    private int _sizeOverview;
    private Rect _rectTextureOverView;
    #endregion

    Rect _rectGroupeColor;

    private PAC_ProjectColor _projectColor;

    public PAC_ProjectColor ProjectColor
    {
        get { return _projectColor; }
    }

    public string ProjectName
    {
        get { return _projectName; }
        set { _projectName = value; }
    }

    public string ProjectPath
    {
        get { return _projectPath; }
        set { _projectPath = value; }
    }

    public PAC_Project ( string nameProject , int width, int height, int sizeOverview, PAC_MainWindow window)
    {
        _window = window;

        _projectName = nameProject;
        _width = width;
        _height = height;

        _sizeOverview = sizeOverview;

        _projectPath = "";
    }

    public void InitProject (Rect rectGroupeGrid)
    {
        _rectGroupeProject = rectGroupeGrid;
        _rectOverView = new Rect(rectGroupeGrid.width- _sizeOverview, rectGroupeGrid.height- _sizeOverview, _sizeOverview, _sizeOverview);
        _rectGrid = new Rect(0, 0, rectGroupeGrid.width - _sizeOverview, rectGroupeGrid.height - 100);
        _rectGroupeColor = new Rect(rectGroupeGrid.width - _sizeOverview, 0, rectGroupeGrid.width -( rectGroupeGrid.width - _sizeOverview), rectGroupeGrid.height - 100);
        _gridPixelArt = new PAC_Grid(_height, _width, _projectName, _rectGroupeProject, _rectGrid);

        _projectColor = new PAC_ProjectColor(_rectGroupeColor, _window);

    }

    public void ShowOnGUI ()
    {
        GUI.BeginGroup(_rectGroupeProject);

       
        _gridPixelArt.ShowOnGUI();

        _projectColor.OnShowGUI();
        GUI.BeginGroup(_rectOverView);
        OverViewPixelArt();
        GUI.DrawTexture(_rectTextureOverView, _gridPixelArt.PixelArtTexture, ScaleMode.StretchToFill, true);
        GUI.EndGroup();

        GUI.EndGroup();
    }

    private void OverViewPixelArt ()
    {
        #region Choix Rect celon le ratio de l'image
        if (_gridPixelArt.PixelArtTexture.height == _gridPixelArt.PixelArtTexture.width)
        {
            _rectTextureOverView = new Rect(0, 0, 100, 100);
        }
        else if (_gridPixelArt.PixelArtTexture.height > _gridPixelArt.PixelArtTexture.width)
        {
            int ratio = _gridPixelArt.PixelArtTexture.height / _gridPixelArt.PixelArtTexture.width;
            int width = 100 / ratio;
            int x =  (100 - width) / 2;
            _rectTextureOverView = new Rect(x, 0, width, 100);
        }
        else
        {
            int ratio = _gridPixelArt.PixelArtTexture.width / _gridPixelArt.PixelArtTexture.height;
            int height = 100 / ratio;
            int y = (100 - height) / 2;
            _rectTextureOverView = new Rect(0, y, 100, height);
        }
        #endregion
    }


}
