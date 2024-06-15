using System;
using UnityEngine;
using UnityEngine.Serialization;

public class MapBuilder : MonoBehaviour
{
    
    [SerializeField] private Grid _grid;
    [SerializeField] private Map _map;
    private SetColor _currentTile;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void StartPlacingTile(GameObject tilePrefab)
    {
        if (_currentTile)
        {
            Destroy(_currentTile.gameObject);
        }

        var tileObject = Instantiate(tilePrefab, _map.transform);
        _currentTile = tileObject.GetComponent<SetColor>();
    }

    private void Update()
    {
        if (_currentTile == null)
        {
            return;
        }
        
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            var worldPosition = hit.point;
            
            var cellPosition = _grid.WorldToCell(worldPosition);
            var cellCenterWorld = _grid.GetCellCenterWorld(cellPosition);
            _currentTile.transform.position = new Vector3(cellCenterWorld.x, 0 , cellCenterWorld.z);
            
            var isAvailable = _map.IsCellAvailable(cellPosition);
            _currentTile.SetRightColor(isAvailable);
            
            if (!isAvailable)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _map.SetTile(cellPosition, _currentTile);
                _currentTile.ResetColor();
                _currentTile = null;
            }
        }
    }
}