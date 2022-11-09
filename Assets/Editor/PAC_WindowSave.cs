using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PAC_WindowSave : EditorWindow
{
    PAC_MainWindow _window;

    private PAC_Project _project;
    private Texture2D _textureToSave;

    private Rect _rectTexture;

    private Rect _rectChangeNameProject;

    private Rect _rectGroupeChangePath;
    private Rect _rectLabelFieldPath;
    private Rect _rectLabelButtonPath;

    private Rect _rectButtonSave;

    public void InitWindowSave (PAC_Project project, PAC_MainWindow window)
    {
        _window = window;
        _project = project;
        _textureToSave = _project.GridPixelArt.PixelArtTexture;
        InitSizeWindow();
        InitVariable();
    }

    private void InitSizeWindow ()
    {
        
        this.minSize = new Vector2(300, 500);
        this.maxSize = this.minSize;
    }

    private void InitVariable ()
    {
        #region Choix Rect celon le ratio de l'image
        if (_textureToSave.height == _textureToSave.width)
        {
            _rectTexture = new Rect(5, 5, 300 - 10, 300 - 10);
        }
        else if (_textureToSave.height > _textureToSave.width)
        {
            int ratio = _textureToSave.height / _textureToSave.width;
            int width = 300 / ratio;
            int x = 5 + (300 - width) / 2;
            _rectTexture = new Rect(x, 5, width - 10, 300 - 10);
        }
        else
        {
            int ratio = _textureToSave.width / _textureToSave.height;
            int height = 300 / ratio;
            int y = 5 + (300 - height) / 2;
            _rectTexture = new Rect(5, y, 300 - 10, height - 10);
        }
        #endregion

        _rectChangeNameProject = new Rect(5, 310, 300 - 10, 20);

        _rectGroupeChangePath = new Rect(5, 335, 300 - 10, 20);
        _rectLabelFieldPath = new Rect(0, 0, 265, 20);
        _rectLabelButtonPath = new Rect(270, 0, 20, 20);

        _rectButtonSave = new Rect(5, 360, 300 - 10, 50);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(_rectTexture, _textureToSave, ScaleMode.StretchToFill, true);

        _project.ProjectName = GUI.TextField(_rectChangeNameProject, _project.ProjectName);

        GUI.BeginGroup(_rectGroupeChangePath);
        _project.ProjectPath = GUI.TextField(_rectLabelFieldPath, _project.ProjectPath);
        if(GUI.Button(_rectLabelButtonPath,"..."))
        {
            _project.ProjectPath = EditorUtility.OpenFolderPanel("Salut1", "Assets", "");
        }
        GUI.EndGroup();

        if (GUI.Button(_rectButtonSave, "Save"))
        {
            if(_project.ProjectPath != "")
            {
                _window.Save();
                this.Close();
            }
        }
    }
}
