using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Spawn : Singleton<Spawn>
{
    [Header("----------------------- Количество объектов -----------------------")]
    [SerializeField] private int _countKaujy = 2;
    [SerializeField] private int _countPanzer = 2;
    [SerializeField] private int _countPlane = 2;
    [SerializeField] private int _countMariner = 2;
    [SerializeField] private int _countArtillery = 2;
    [Space]
    [Header("---------------------------- Системные --------------------------")]
    public LayerMask _tileMask;
	public PlayerEntity Kaujy;
    [SerializeField] private string[] _names;
    public PlayerEntity EpicKaujy;
	[SerializeField] private Enemy [] _enemy;
	public List<Enemy> Enemyes;
	public List<PlayerEntity> Players;
    public GameObject PlayerControler;
    [SerializeField] private GameObject _vfxKaugySpawn;
    [SerializeField] private GameObject NextTurnButton;
    private bool _readySpawn;
    private Vector3 _coordCell;
	private Transform _targetCell;
    
	public static event System.Action OnGameStart;

    public Spawn(bool readySpawn)
    {
        _readySpawn = readySpawn;
    }

    private void Start()
    {
        Invoke("SpawnPing", 0.2f);
        NextTurnButton.SetActive(false);
    }

    private void SpawnPing()
    {
        GridSpawnKaujy(1, true);
        SpawnEnemy();
    }

    private void GridSpawnKaujy(int index, bool setUnset)
    {
        if (index == 1)
        {
            RaySetColor(3, 2, 0, setUnset);
        }
        else if (index == 2)
        {
            RaySetColor(9, 2, 5, setUnset);
        }
    }

    private void RaySetColor(int xGrid, int yGrid, int limit, bool setUnset)
    {
        for (int x = limit; x < xGrid; x++)
        {
            for (int y = 0; y < yGrid; y++)
            {
                Vector3 spawnCoord = new Vector3(x, 0, y);
                Ray ray = new Ray(new Vector3(spawnCoord.x, 6, spawnCoord.z), Vector3.down);
                RaycastHit hit = new RaycastHit();
                
                if (!Physics.Raycast(ray, out hit, 100f, _tileMask, QueryTriggerInteraction.Ignore)) continue;
                if (hit.transform.GetComponent<TileParameters>() != null &&
                    hit.transform.GetComponent<TileParameters>().SpawnKaujy && setUnset)
                {
                    hit.transform.gameObject.GetComponent<Tile>().SetColor(new Color(0f, 0.7f, 1f));
	                hit.transform.gameObject.GetComponent<Tile>().CanBuild = true;
                }
                else 
                {
                	hit.transform.gameObject.GetComponent<Tile>().SetColor(Color.white);
	                hit.transform.gameObject.GetComponent<Tile>().CanBuild = false;
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        Creator(0, _countPanzer);
        Creator(1, _countPlane);
        Creator(2, _countMariner);
        Creator(3, _countArtillery);
    }

    public void Creator(int index, int count)
    {
        var co = 0;
        while (co < count)
        {
            to:
            Vector3 spawnCoord = new Vector3(Random.Range(0, 8), 0, Random.Range(6, 8));
            Ray ray = new Ray(new Vector3(spawnCoord.x, 6, spawnCoord.z), Vector3.down);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit, 100f, _tileMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.GetComponent<Tile>() != null &&
                    hit.transform.GetComponent<TileParameters>().SpawnPanzer)
                {
                    if (Enemyes.Count != 0)
                    {
                        foreach (var e in Enemyes)
                        {
	                        if (e.transform.position == spawnCoord + Vector3.up)
                            {
                                goto to;
                            }
                        }
                    }

                    var enemy = Instantiate(_enemy[index], spawnCoord + Vector3.up, Quaternion.identity);
                    enemy.name = _names[index];
                    Enemyes.Add(enemy);
                    co += 1;

                }
            }
            
        }
    }

    public void ClearSpawnCoord()
    {
        _coordCell = Vector3.zero;
	    if (_targetCell is { }) _targetCell.gameObject.GetComponent<Tile>().ResetColor();
        _targetCell = null;
    }


    public void SpawnKaujy()
    {
        if (_targetCell != null)
        {
            var vfx = Instantiate(_vfxKaugySpawn, _coordCell, Quaternion.identity);
            Destroy(vfx, 3f);
            var player = Instantiate(Kaujy, _coordCell, Quaternion.identity);
            player.name = "Kaiju";
            _countKaujy -= 1;
            Players.Add(player);
            _targetCell.GetComponent<TileParameters>().SpawnKaujy = false;
            ClearSpawnCoord();
            if (_countKaujy <= 0)
            {
	            GridSpawnKaujy(2, false);
                NextTurnButton.SetActive(true);
	            PlayerControler.SetActive(true);
	            //OnGameStart?.Invoke();

                return;
            }
            
            GridSpawnKaujy(1, false);
            GridSpawnKaujy(2, true);
            
        }
        else
        {
            Debug.Log("нельзя создать");
        }
    }
    
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
	    if (EventSystem.current.IsPointerOverGameObject()) return;
        
	    ClearSpawnCoord();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        
	    if (!(Physics.Raycast(ray, out hit, 100f, _tileMask, QueryTriggerInteraction.Ignore) &&
              hit.transform.gameObject.GetComponent<Tile>().CanBuild)) return;
        if(hit.transform.GetComponent<TileParameters>() != null && hit.transform.GetComponent<TileParameters>().SpawnKaujy)
        {
            _coordCell = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.8f, hit.transform.position.z);
            hit.transform.gameObject.GetComponent<Tile>().SetColor(Color.blue);
            _targetCell = hit.transform;
            SpawnKaujy();
        }
        else
        {
            //Debug.Log("нельзя выделить");
        }
    }
}
