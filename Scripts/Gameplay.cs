using UnityEngine;
using System;
using System.Collections;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private float timeToFreezeGame;
    [SerializeField] private float _lowImpusle;
    [SerializeField] private float _normalImpulse;
    [SerializeField] private float _highImpusle;
    [SerializeField] private GameObject _2Cube;
    [SerializeField] private GameObject _4Cube;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _secondsForMerge;
    [SerializeField] private float _lowSpeed;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _highSpeed;
    [SerializeField] private int _maxCubeNumber;


    private bool newCubeIsReady=true;
    private Vector2 inputPoint;
    private Vector2 unInputPoint;
    private float timeToUnInput;
    private bool gameIsFreezed;
    private bool letToMerge;
    private bool letToCreateCube;


    public static event Action<float, bool> OnSwipeHappend;
    public static event Action OnSwipeDown;
    public static event Action OnSwipeUp;
    public static event Action <bool>OnLetMerge;
    public static event Action OnTakeInfoCubeNumber;

    private void OnEnable()
    {
        Cube.OnRequestToMerge += LetToMerge;
        Cube.OnInfoCubeNumber += LetToCreateCube;
    }
    private void OnDisable()
    {
        Cube.OnRequestToMerge -= LetToMerge;
        Cube.OnInfoCubeNumber -= LetToCreateCube;
    }
    private void LetToMerge()
    {
        OnLetMerge?.Invoke(letToMerge);
    }

    private void LetToCreateCube(int cubeNumber)
    {
        if (cubeNumber < _maxCubeNumber)
        {
            letToCreateCube = true;
        }
        else
        {
            letToCreateCube = false;
        }
    }

    private void Update()
    {
        if (gameIsFreezed == false) 
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    inputPoint = touch.position;
                    unInputPoint = touch.position;
                    timeToUnInput = 0f;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    unInputPoint = touch.position;
                    timeToUnInput += Time.deltaTime;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    StartCoroutine(FrezzeGame(timeToFreezeGame));
                    float deltaX = Mathf.Abs(inputPoint.x - unInputPoint.x);
                    float deltaY = Mathf.Abs(inputPoint.y - unInputPoint.y);

                    if (deltaX > deltaY)
                    {
                        if (inputPoint.x > unInputPoint.x)
                        {
                            float distance = Vector2.Distance(inputPoint, unInputPoint);
                            float speed = distance / timeToUnInput;
                            WhereAndHowPowerIsSwipe(speed, true);
                        }
                        else
                        {
                            float distance = Vector2.Distance(inputPoint, unInputPoint);
                            float speed = distance / timeToUnInput;
                            WhereAndHowPowerIsSwipe(speed, false);
                        }
                    }
                    else if (deltaY > deltaX)
                    {
                        if (inputPoint.y > unInputPoint.y)
                        {
                            if (newCubeIsReady==true) 
                            {
                                OnSwipeDown?.Invoke();
                                newCubeIsReady = false; 
                            }
                            
                        }
                        else
                        {
                            if (newCubeIsReady==false)
                            {
                                SpawnCube();
                            }
                        }
                    }
                }
            }
        }
    }

    private IEnumerator LetToMerge(float seconds)
    {
        letToMerge = true;
        yield return new WaitForSeconds(seconds);
        letToMerge = false;
    }

    private void WhereAndHowPowerIsSwipe(float speed, bool leftSwipe)
    {
        if (speed < _lowSpeed)
        {
            OnSwipeHappend?.Invoke(_lowImpusle, leftSwipe);
        }
        else if(speed >= _lowSpeed && speed < _highSpeed)
        {
            OnSwipeHappend?.Invoke(_normalImpulse, leftSwipe);
        }
        else
        {
            OnSwipeHappend?.Invoke(_highImpusle, leftSwipe);
            StartCoroutine(LetToMerge(_secondsForMerge));
        }
    }

    private IEnumerator FrezzeGame(float time)
    {
        gameIsFreezed = true;
        yield return new WaitForSeconds(time);
        gameIsFreezed = false;
    }

    

    private void SpawnCube()
    {
        OnTakeInfoCubeNumber?.Invoke();
        if (letToCreateCube)
        {
            int spawnChanceOfCubes = UnityEngine.Random.Range(1, 5);
            if (spawnChanceOfCubes == 1)
            {
                Instantiate(_4Cube, _spawnPoint.position, _spawnPoint.rotation);
            }
            else
            {
                Instantiate(_2Cube, _spawnPoint.position, _spawnPoint.rotation);
            }

            newCubeIsReady = true;
        }
    }
}
