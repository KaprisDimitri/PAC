using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PAC_MainWindow : EditorWindow
{
    #region Variables

    #region Variable For Menu
    private Rect _rectGroupeMenu;
    private Rect _rectButton;

    private PAC_Menu _menu;
    #endregion

    #region Variable TabProject
    private Rect _rectGroupeTab;
    private Rect _rectButtonTab;

    private PAC_TabProject _tab;
    #endregion

    private PAC_Project _projectSelected;
    public PAC_Project ProjectSelected
    {
        set { _projectSelected = value; if (_toolSelected != null) { _toolSelected.ProjectSelected = value; } }
    }

    private PAC_Tool _toolSelected;
    public PAC_Tool ToolSelected
    {
        set { _toolSelected = value; _optionTools.OptionTools = value.GetOptionTools; _toolSelected.ProjectSelected = _projectSelected; }
        get { return _toolSelected; }
    }

    #region Variable Grid
    private Rect _rectGroupeGrid;
    #endregion

    #region Variable Project
    private int _sizeOverview;
    public int SizeOverview { get { return _sizeOverview; } }
    #endregion

    #region Variable Tools

    private Rect _rectGroupeTools;
    private Rect _rectButtonTools;

    private PAC_Tools _tools;

    #endregion

    #region Variable OptionTool

    private Rect _rectGroupeOptionTool;

    private PAC_OptionTools _optionTools;

    #endregion

    


    #endregion

    #region Fonctions

    #region Fonction Ouverture Fenetre
    [MenuItem("PixelArt/PixelArtCreator")]
    static void PixelArtCreator()
    {
        PAC_MainWindow window = (PAC_MainWindow)EditorWindow.GetWindow(typeof(PAC_MainWindow));
        window.InitMainWindow();
        
        window.Show();
    }

    #endregion

    #region InitFonction
    public void InitMainWindow ()
    {
        InitSizeWindow();
        InitVariable();
        InitObjet();
        _projectSelected = new PAC_Project("New Project", 32, 32, _sizeOverview, this);
        SetNewProject(_projectSelected);
    }

    private void InitSizeWindow ()
    {
        this.minSize = new Vector2(1920, 1000);
        this.maxSize = this.minSize;
    }

    private void InitVariable ()
    {
        
        _rectGroupeMenu = new Rect(0, 0, this.minSize.x, 30);
        _rectButton = new Rect(0, 0, 50, 30);

        _rectGroupeTab = new Rect(0, 30, this.minSize.x, 30);
        _rectButtonTab = new Rect(0, 0, 100, 30);

        _rectGroupeGrid = new Rect(50, 110, this.minSize.x-50, this.minSize.y - 110);

        _rectGroupeTools = new Rect(0, 110, 50, this.minSize.y - 110);
        _rectButtonTools = new Rect(0, 0, 50, 50);

        _rectGroupeOptionTool = new Rect(0, 60, this.minSize.x, 50);

        _sizeOverview = 100;
    }

    private void InitObjet ()
    {
        _menu = new PAC_Menu(_rectGroupeMenu, _rectButton, this);

        _tab = new PAC_TabProject(_rectGroupeTab, _rectButtonTab, this);

        _tools = new PAC_Tools(_rectGroupeTools, _rectButtonTools, this);

        _optionTools = new PAC_OptionTools(_rectGroupeOptionTool);
    }
    #endregion


    private void OnGUI()
    {
        _menu.ShowOnGUI();

        _tab.ShowTabOnGui();

        if(_projectSelected != null)
        {
            _projectSelected.ShowOnGUI();
        }

        _tools.OnShowGUI();

        if(_toolSelected != null)
        {
            _toolSelected.OnShowGui();
            _optionTools.OnShowGUI();
        }
    }

    #region Fonction Utilitaire
    public void OpenWindowCreatNewProject ()
    {
        PAC_WindowNewProject window = ScriptableObject.CreateInstance(typeof(PAC_WindowNewProject)) as PAC_WindowNewProject;
        window.InitWindowNewProject(this);
        window.ShowUtility();
    }

    public void OpenWindowSaveAs ()
    {
        if (_projectSelected != null)
        {
            PAC_WindowSave window = ScriptableObject.CreateInstance(typeof(PAC_WindowSave)) as PAC_WindowSave;
            window.InitWindowSave(_projectSelected, this);
            window.ShowUtility();
        }
    }

    public void Save ()
    {
        if (_projectSelected != null)
        {
            if (_projectSelected.ProjectPath == "")
            {
                OpenWindowSaveAs();
            }
            else
            {
                Texture2D textureFinal = _projectSelected.GridPixelArt.PixelArtTexture;
                Sprite spriteFinale = Sprite.Create(textureFinal, new Rect(0, 0, textureFinal.width, textureFinal.height), Vector2.zero);
                
                textureFinal.filterMode = FilterMode.Point;
                textureFinal.wrapMode = TextureWrapMode.Clamp;
               
                //textureFinal.Compress(false);

                string path = _projectSelected.ProjectPath +"/"+ _projectSelected.ProjectName + ".png"; 

                if (path.Length != 0)
                {
                    byte[] pngData = textureFinal.EncodeToPNG();
                    if (pngData != null)
                    {
                        File.WriteAllBytes(path, pngData);

                        // As we are saving to the asset folder, tell Unity to scan for modified or new assets
                        AssetDatabase.Refresh();

                        #region Importe texture set up
                        //A FAIRE FONCTION QUI REPAIRE LE DOC ASSET
                        char[] charPath = path.ToCharArray();
                        path = "";
                        for (int i = 34; i < charPath.Length; i++)
                        {
                            path += charPath[i];
                        }

                        AssetDatabase.ImportAsset(path);

                        
                        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                        Debug.Log(path);
                        Debug.Log(importer);
                        importer.textureType = TextureImporterType.Sprite; 
                        importer.filterMode = FilterMode.Point;
                        importer.spriteImportMode = SpriteImportMode.Single;
                        importer.spritePixelsPerUnit = textureFinal.width;
                        // importer.compressionQuality = 0;

                        //Permet d enregister et appliquer les changement de l importeTexture
                        importer.SaveAndReimport();
                        AssetDatabase.WriteImportSettingsIfDirty(path);
                        #endregion
                    }
                }
            }
        }
    }

    /// <summary>
    /// Fonction appeler par la fenetre de creation de projet pour set le projet dans les Tab
    /// La Fonction permet aussi de donner les Rect de la grille
    /// </summary>
    /// <param name="newProject"></param>
    public void SetNewProject (PAC_Project newProject)
    {
        newProject.InitProject(_rectGroupeGrid);
        _tab.CreatNewTab(newProject);
    }


    #endregion 

    #endregion
}
