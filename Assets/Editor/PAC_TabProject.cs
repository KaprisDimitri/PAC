using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// Class qui contient les projet et cree les onglet Renvoie le projet selectionner
/// </summary>
public class PAC_TabProject
{
    private PAC_MainWindow _window;

    private Rect _rectGroupeTab;
    private Rect _rectButtonTab;

    private PAC_Project[] _projects;
    private PAC_Project _projectSelect;


    public PAC_TabProject (Rect rectGroupeTab, Rect rectButtonTab, PAC_MainWindow window)
    {
        _rectGroupeTab = rectGroupeTab;
        _rectButtonTab = rectButtonTab;

        _projects = new PAC_Project[0];

        _window = window;
    }

    /// <summary>
    /// Fonction qui affiche les onglet des projet existant 
    /// </summary>

    public void ShowTabOnGui()
    {
        GUI.BeginGroup(_rectGroupeTab);

        for(int i = 0; i< _projects.Length;i++)
        {
            Rect rectButtonTab = _rectButtonTab;
            rectButtonTab.x = rectButtonTab.width * i;
            if (GUI.Button(rectButtonTab, _projects[i].ProjectName))
            {
                ChangeSelectedProject(_projects[i]);
            }
        }

        GUI.EndGroup();
    }

    /// <summary>
    /// Fonction qui permet de mettre une projet en temps qu actif (actif = visible)
    /// </summary>
    /// <param name="project"></param>
    private void ChangeSelectedProject (PAC_Project project)
    {
        if (_projectSelect != project)
        {
            _projectSelect = project;
            _window.ProjectSelected = _projectSelect;
        }
    }


    /// <summary>
    /// Fonction qui met le projet dans la liste et qui fait selectionner le projet comme projet actif
    /// </summary>
    /// <param name="newProjec"></param>
    public void CreatNewTab (PAC_Project newProjec)
    {
        PAC_Project[] projecTemp = _projects;
        _projects = new PAC_Project[projecTemp.Length + 1];
        
        for(int i = 0; i< projecTemp.Length;i++)
        {
            _projects[i] = projecTemp[i];
        }

        _projects[projecTemp.Length] = newProjec;

        ChangeSelectedProject(newProjec);
    }
    
}
