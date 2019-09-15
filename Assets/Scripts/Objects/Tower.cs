using UnityEngine;

public class Tower : MonoBehaviour, IObserver<long>
{
    public TowerSection towerSectionPrefab;

    private TowerSection[] sections;
    private Background[] backgrounds;
    private GameObject darkEffect;

    private bool started = false;

    public bool FirstGenerate { get; private set; } = true;

    private void Awake()
    {
        sections = new TowerSection[3];
        Create();
    }

    // generowanie wieży
    public void Create()
    {
        for (var i = 0; i < sections.Length; i++) {
            sections[i] = Instantiate( towerSectionPrefab );
            sections[i].Init( i );
            sections[i].transform.parent = transform;
        }

        for (var i = 0; i < TowerFactory.PlatformsForEachSection; i++) {
            CreateNextRow();
        }
    }

    public void Update()
    {
        if ( started ) {
            MoveY( -Time.deltaTime * GameManager.GetGameSpaeed( GameManager.CurrentDifficulty ) );
        }
    }

    // poruszanie wieży lewo/prawo
    public void MoveX(float dx)
    {
        foreach (TowerSection section in sections) {
            section.MoveX( dx );
        }

        CheckSections();
    }

    /*
    public void SynchronizePositionWithPlayer()
    {
        transform.position = new Vector3( GameManager.HumanPlayer.transform.position.x, transform.position.x );

        foreach (TowerSection section in sections) {
            section.SynchronizePositionWithPlayer();
        }

        CheckSections();
    }*/

    public void CheckSections()
    {
        // przesuwamy sekcje w zaleznosci od polozenia skrajnych sekcji oraz ich scian
        if (sections[0].Bounds.Right > 0) {
            // przesuwamy w lewo sekcje, bo prawa sciana skecji na lewym skraju mija gracza
            MoveSectionsToLeft();
        } else if (sections[2].Bounds.Left < 0) {
            // przesuwamy w prawo sekcje, bo lewa sciana skecji na prawym skraju mija gracza
            MoveSectionsToRight();
        }
    }


    // zwraca najnizsza platforme dla sekcji o indeksie
    public Platform GetLowestPlatform(int index)
    {
        return sections[index].GetLowestPlatform();
    }

    // poruszanie wieży góra / dół
    public void MoveY(float dy)
    {
        transform.position = transform.position + new Vector3( 0, dy );

        bool shouldChangeRows = true;

        if (sections[0].Background.GetLowestBackgroundY() + sections[0].Background.GetSpriteHeigth() / 2f < GameManager.CameraYBound.Left) {
            foreach (TowerSection section in sections) {
                section.MoveBackgroundUp();
            }
        }

        foreach (TowerSection section in sections) {
            if (shouldChangeRows && ( section.GetLowestPlatform().transform.position.y ) > GameManager.CameraYBound.Left - 2.5f) {
                return;
            }
        }

        CreateNextRow();
        RemoveLowestRow();
    }

    // Generuje kolejne platformy
    public void CreateNextRow()
    {
        foreach (TowerSection section in sections) {
            section.CreatePlatform();
        }
    }

    // Usuwa najniższe platformy
    public void RemoveLowestRow()
    {
        foreach (TowerSection section in sections) {
            section.RemoveLowestPlatform();
        }
    }

    // zamiana sekcji miejscami
    public void ReplaceSections(int index1, int index2)
    {
        TowerSection tempTs = sections[index1];
        sections[index1] = sections[index2];
        sections[index2] = tempTs;

        Bounds tempB = sections[index1].Bounds;
        Vector3 tempPos = sections[index1].transform.position;

        sections[index1].Bounds = sections[index2].Bounds;
        sections[index1].transform.position = sections[index2].transform.position;

        sections[index2].Bounds = tempB;
        sections[index2].transform.position = tempPos;
    }

    // przesuwa sekcje w lewo
    public void MoveSectionsToLeft()
    {
        sections[2].Bounds = new Bounds( sections[0].Bounds.Left - sections[0].Background.GetSpriteWidth() * 2, sections[0].Bounds.Left );
        Vector3 newPosition = new Vector3( sections[0].Bounds.Left - sections[0].Background.GetSpriteWidth(), sections[0].transform.position.y );
        sections[2].transform.position = newPosition;

        TowerSection[] newSections = new TowerSection[sections.Length];
        newSections[0] = sections[2];
        newSections[1] = sections[0];
        newSections[2] = sections[1];
        sections = newSections;
    }

    // przesuwa sekcje w prawo
    public void MoveSectionsToRight()
    {
        sections[0].Bounds = new Bounds( sections[2].Bounds.Right, sections[2].Bounds.Right + sections[0].Background.GetSpriteWidth() * 2 );
        Vector3 newPosition = new Vector3( sections[2].Bounds.Right + sections[0].Background.GetSpriteWidth(), sections[2].transform.position.y );
        sections[0].transform.position = newPosition;

        TowerSection[] newSections = new TowerSection[sections.Length];
        newSections[0] = sections[1];
        newSections[1] = sections[2];
        newSections[2] = sections[0];
        sections = newSections;
    }

    // usuwa wszystkie sekcje oraz platformy
    public void RemoveSections()
    {
        if (sections != null && sections.Length > 0) {
            foreach (TowerSection section in sections) {
                section.RemoveAllPlatforms();
                Destroy( section.gameObject );
            }
        }
    }

    public void Notice(long t)
    {
        if ( t == 1 ) {
            started = true;
        }
    }

    public GameObject DarkEffect { get => darkEffect; set => darkEffect = value; }
}