using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Fenetre' qui cree le jeu  A REFAIRE TROP BROUILLON
/// </summary>
public class PAC_WindowNewProject : EditorWindow
{
    private PAC_MainWindow _window;

    private Rect _rectGroupeField;
    private Rect _rectGroupeButton;

    private int _wight;
    private int _height;
    private Rect _rectWidthHeight;

    private string _name;
    private Rect _rectName;


    public void InitWindowNewProject (PAC_MainWindow window)
    {
        _rectGroupeField = new Rect(0, 0,400, 90);
        _rectGroupeButton = new Rect(0, 90, 400, 30);
        _rectWidthHeight = new Rect(0, 0, 400 / 2, 30);
        _window = window;
    }
    private void OnGUI()
    {
        GUI.BeginGroup(_rectGroupeField);
        GUI.Label(_rectWidthHeight, "Width");
        Rect rectWidthHeight = _rectWidthHeight;
        rectWidthHeight.x = rectWidthHeight.width;
        _wight = EditorGUI.IntField(rectWidthHeight, _wight);
        rectWidthHeight.y = rectWidthHeight.height;
        _height = EditorGUI.IntField(rectWidthHeight, _height);
        rectWidthHeight = _rectWidthHeight;
        rectWidthHeight.y = _rectWidthHeight.height;
        GUI.Label(rectWidthHeight, "Height");
        rectWidthHeight.y *= 2;
        GUI.Label(rectWidthHeight, "ProjectName");
        rectWidthHeight.x = rectWidthHeight.width;
        _name = GUI.TextField(rectWidthHeight, _name);
        

        GUI.EndGroup();



        GUI.BeginGroup(_rectGroupeButton);
        if (CheckIfAllVariableSetUp())
        {
            if (GUI.Button(_rectWidthHeight, "Create"))
            {
                CreateANewProject();
            }

            Rect rectWdthHeight = _rectWidthHeight;
            rectWdthHeight.x = rectWdthHeight.width;

            if (GUI.Button(rectWdthHeight, "Close"))
            {
                this.Close();
            }
        }
        else if(GUI.Button(new Rect(_rectWidthHeight.x,_rectWidthHeight.y, _rectWidthHeight.width*2, _rectWidthHeight.height), "Close"))
        {
            this.Close();
        }
        GUI.EndGroup();
        
    }

    private void CreateANewProject ()
    {
        _window.SetNewProject(new PAC_Project(_name, _wight, _height, _window.SizeOverview,_window));
        this.Close();
    }

    private bool CheckIfAllVariableSetUp ()
    {
        if(_height == 0 || _wight == 0 || _name == null || _name == "" || _name == " ")
        {
            return false;
        }
        return true;
    }
}
