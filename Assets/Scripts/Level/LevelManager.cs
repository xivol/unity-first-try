using UnityEngine;
using UnityEditor;
using Xivol.Input;

[ExecuteInEditMode]
public class LevelManager : Singleton<LevelManager>
{
    // Show in Editor and not in scripts
    #pragma warning disable CS0649
    [SerializeField] GameObject cursorPrefab;
    [SerializeField] GameObject levelPrefab;
    [SerializeField] GameObject pathRendererPrefab;
    #pragma warning restore CS0649

    public GameObject Cursor { get; private set; }
    public LevelController Level { get; private set; }
    public PathRenderer PathingRenderer { get; private set; }

    private bool _showCursor = true;

    protected override void Initialize() 
    {
        var level = GameObject.Find(levelPrefab.name);
        if (level == null)
        {
            level = Instantiate(levelPrefab);
            level.name = levelPrefab.name;
        }
        Level = level.GetComponent<LevelController>();

        Cursor = GameObject.Find(cursorPrefab.name);
        if (Cursor == null)
        {
            Cursor = Instantiate(cursorPrefab);
            Cursor.transform.SetParent(Level.transform);
            Cursor.name = cursorPrefab.name;

            //Cursor.GetComponent<Movable2D>().GetHeight.AddListener(Level.OnGetHeight);
            //Cursor.GetComponent<InputValueChangedListener>().RawValueChanged.AddListener( );
        }
        else
        {
            //Cursor.GetComponent<Movable2D>().GetHeight.AddListener(Level.OnGetHeight);
        }

        var pRenderer = GameObject.Find(pathRendererPrefab.name);
        if (pRenderer == null)
        {
            PathingRenderer = Instantiate(pathRendererPrefab).GetComponent<PathRenderer>();
            PathingRenderer.transform.SetParent(Level.transform);
            PathingRenderer.name = pathRendererPrefab.name;
        }
        else
        {
            PathingRenderer = pRenderer.GetComponent<PathRenderer>();
        }

        //ShowCursor = false;
        //ShowPathing = false;
    }

    public bool ShowCursor {
        get { return _showCursor; }
        set 
        {
            _showCursor = value;
            Cursor.SetActive(_showCursor); 
        }
    }

    public bool ShowPathing
    {
        get { return PathingRenderer.enabled; }
        set {PathingRenderer.enabled = value; }
    }
}
