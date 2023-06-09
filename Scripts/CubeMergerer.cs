using UnityEngine;
using System;

public class CubeMergerer : MonoBehaviour
{
    [SerializeField] private GameObject _cube4;
    [SerializeField] private GameObject _cube8;
    [SerializeField] private GameObject _cube16;
    [SerializeField] private GameObject _cube32;
    [SerializeField] private GameObject _cube64;
    [SerializeField] private GameObject _cube128;

    public static event Action<int> OnMergeHappened;

    private void OnEnable()
    {
        Cube.OnMergeingOfCubes += MergeCubes;
    }

    private void OnDestroy()
    {
        Cube.OnMergeingOfCubes -= MergeCubes;
    }

    private void MergeCubes(string tag, Vector3 position, Quaternion rotation)
    {
        switch (tag)
        {
            case "2":
                Instantiate(_cube4, position, rotation);
                break;
            case "4":
                Instantiate(_cube8, position, rotation);
                break;
            case "8":
                Instantiate(_cube16, position, rotation);
                break;
            case "16":
                Instantiate(_cube32, position, rotation);
                break;
            case "32":
                Instantiate(_cube64, position, rotation);
                break;
            case "64":
                Instantiate(_cube128, position, rotation);
                break;
        }
        OnMergeHappened?.Invoke(int.Parse(tag));
    }
}

