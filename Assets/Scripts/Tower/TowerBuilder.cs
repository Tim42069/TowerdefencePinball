using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private Material canPlaceMaterial;
    [SerializeField] private Material cannotPlaceMaterial;
    [SerializeField] private GameObject hexPrefab;
    [SerializeField] private AudioSource builderAudioSource;
    [SerializeField] private AudioClip placeClip;
    
    private TowerData _currentTower;
    private GameObject _currentTowerPreview;
    private Renderer _currentTowerPreviewRenderer;

    private GameObject _currentSelectedHexPreview;
    private Renderer _currentHexPreviewRenderer;
    private bool _canPlaceTile = true;

    public static event Action<GameObject> OnTowerPlacement;
        
    private void OnEnable()
    {
        TowerBrowser.OnTowerSelected += OnTowerSelected;
        BuildingState.OnMoveTile += OnMoveTile;
        BuildingState.OnPlaceTowerButton += OnTowerPlace;

        WaveState.WaveStart += OnWaveStart;
        GoblinSpawner.endwave += OnWaveEnd;
        
        _currentSelectedHexPreview = Instantiate(hexPrefab, new Vector3(1000, 0, 0), transform.rotation);
        _currentHexPreviewRenderer = _currentSelectedHexPreview.GetComponentInChildren<Renderer>();
        _currentSelectedHexPreview.GetComponentInChildren<Renderer>().material = canPlaceMaterial;
    }

    private void OnDisable()
    {
        TowerBrowser.OnTowerSelected -= OnTowerSelected;
        BuildingState.OnMoveTile -= OnMoveTile;
        BuildingState.OnPlaceTowerButton -= OnTowerPlace;

        WaveState.WaveStart -= OnWaveStart;
        GoblinSpawner.endwave -= OnWaveEnd;
        
        Destroy(_currentSelectedHexPreview);
    }

    void OnTowerSelected(TowerData tower)
    {
        _currentTower = tower;
        if (_currentTowerPreview != null)
        {
            Destroy(_currentTowerPreview);
        }

        Vector3 newPositon = _currentSelectedHexPreview.transform.position;
        _currentTowerPreview = Instantiate(_currentTower.towerPrefab, newPositon, transform.rotation);
        _currentTowerPreviewRenderer = _currentTowerPreview.GetComponentInChildren<Renderer>();
        if (UpdatePreviewMaterials(newPositon)) return;
        SetPreviewMaterials();
    }

    public void OnTowerPlace()
    {
        // If preview exists, set new position and rotation to the same value
        if (_currentTowerPreview == null) return;
        Transform previewTransform = _currentTowerPreview.transform;
        
        Quaternion spawnRotation = previewTransform.rotation;
        Vector3 buildPosition = previewTransform.position;

        // If there is space, spawn in the tower
        if (!MapGen.instance.IsPositionEmpty(buildPosition, _currentTower.radius)) return;
        if (GameEconomy.Instance.Spend(_currentTower.cost))
        {
            GameObject tower = Instantiate(_currentTower.towerPrefab, buildPosition, spawnRotation);
            Transform radus = tower.transform.Find("radius");
            if (radus != null)
            {
                radus.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("nochild");
            }
            Debug.Log("PLACED");
            // Play audio
            builderAudioSource.PlayOneShot(placeClip);

            // Update materials
            _canPlaceTile = false;
            UpdatePreviewMaterials(cannotPlaceMaterial);
            OnTowerPlacement?.Invoke(tower);
        }
       
    }

    private void OnMoveTile(GameObject newTile)
    {
        // Get the new tile positon and rotation
        Transform newTileTransform = newTile.transform;
        Quaternion spawnRotation = newTileTransform.rotation;
        Vector3 buildPosition = newTileTransform.position;

        // Update the selected tiles positon and rotation
        Transform selectedTileTransform = _currentSelectedHexPreview.transform;
        selectedTileTransform.rotation = spawnRotation;
        selectedTileTransform.position = buildPosition;

        // Update the tower previews position and rotation
        if (_currentTowerPreview == null) return;
        _currentTowerPreview.transform.position = buildPosition;
        _currentTowerPreview.transform.rotation = spawnRotation;
        
        // Update materials to show whether the place can place
        UpdatePreviewMaterials(buildPosition);

    }

    private void OnWaveStart(int waveNumber)
    {
        if (_currentTowerPreview != null) _currentTowerPreview.transform.position -= Vector3.up * 1000;
        if (_currentSelectedHexPreview != null) _currentSelectedHexPreview.transform.position -= Vector3.up * 1000;
    }
    
    private void OnWaveEnd(bool start)
    {

        if (_currentTowerPreview != null) _currentTowerPreview.transform.position += Vector3.up * 1000;
        if (_currentSelectedHexPreview != null) _currentSelectedHexPreview.transform.position += Vector3.up * 1000;
    }

    private bool UpdatePreviewMaterials(Vector3 position)
    {
        if (_canPlaceTile == MapGen.instance.IsPositionEmpty(position, _currentTower.radius)) return false;
        
        _canPlaceTile = !_canPlaceTile;
        SetPreviewMaterials();
        return true;
    }
    private void SetPreviewMaterials()
    {
        ref Material chosenMaterial = ref canPlaceMaterial;
        if (!_canPlaceTile)
        {
            chosenMaterial = ref cannotPlaceMaterial;
        }

        _currentHexPreviewRenderer.material = chosenMaterial;
        _currentTowerPreviewRenderer.material = chosenMaterial;
    }

    private void UpdatePreviewMaterials(Material material)
    {
        _currentHexPreviewRenderer.material = material;
        _currentTowerPreviewRenderer.material = material;
    }
}


public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    internal static void Invoke(object v)
    {
        throw new NotImplementedException();
    }

    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}