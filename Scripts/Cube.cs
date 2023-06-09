using UnityEngine;
using System;
using System.Collections;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _creatingImpulse;

    private Rigidbody rb;
    private bool letToMerge;

    private static int cube2Count = 0;
    private static int cube4Count = 0;
    private static int cube8Count = 0;
    private static int cube16Count = 0;
    private static int cube32Count = 0;
    private static int cube64Count = 0;

    private static int numberOfCubes = 0;

    public static event Action<string, Vector3, Quaternion> OnMergeingOfCubes;
    public static event Action OnRequestToMerge;
    public static event Action<int> OnInfoCubeNumber;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up*_creatingImpulse,ForceMode.Impulse);
        
    }
    private void Awake()
    {
        numberOfCubes++;
    }
    private void OnDestroy()
    {
        numberOfCubes--;
    }
    private void OnEnable()
    {
        Gameplay.OnSwipeHappend += GetImpulse;
        Gameplay.OnLetMerge += RequestForMerge;
        Gameplay.OnTakeInfoCubeNumber += GiveInfoCubeNumber;
    }
    private void OnDisable()
    {
        Gameplay.OnSwipeHappend -= GetImpulse;
        Gameplay.OnLetMerge -= RequestForMerge;
        Gameplay.OnTakeInfoCubeNumber -= GiveInfoCubeNumber;
    }

    private void GiveInfoCubeNumber()
    {
        OnInfoCubeNumber?.Invoke(numberOfCubes);
    }

    private void GetImpulse(float impulsePower,bool leftSwipe)
    {
        if (leftSwipe)
        {
            rb.AddForce(Vector3.left * impulsePower, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * impulsePower, ForceMode.Impulse);
        }

    }

    private void RequestForMerge(bool letMerge)
    {
        letToMerge = letMerge;
    }


    private void OnCollisionEnter(Collision collision)
    {
        OnRequestToMerge?.Invoke();
        if (letToMerge)
        {
            if (gameObject.tag == collision.gameObject.tag)
            {
                MergeCubes(gameObject.tag);
            }
        }
        
    }

    private void MergeCubes(string tag)
    {
        switch (tag)
        {
            case "2":
                if (cube2Count == 0)
                {
                    Destroy(gameObject);
                    cube2Count++;
                }
                else
                {
                    cube2Count = 0;
                    OnMergeingOfCubes?.Invoke("2",transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            case "4":
                if (cube4Count == 0)
                {
                    Destroy(gameObject);
                    cube4Count++;
                }
                else
                {
                    cube4Count = 0;
                    OnMergeingOfCubes?.Invoke("4", transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            case "8":
                if (cube8Count == 0)
                {
                    Destroy(gameObject);
                    cube8Count++;
                }
                else
                {
                    cube8Count = 0;
                    OnMergeingOfCubes?.Invoke("8", transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            case "16":
                if (cube16Count == 0)
                {
                    Destroy(gameObject);
                    cube16Count++;
                }
                else
                {
                    cube16Count = 0;
                    OnMergeingOfCubes?.Invoke("16", transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            case "32":
                if (cube32Count == 0)
                {
                    Destroy(gameObject);
                    cube32Count++;
                }
                else
                {
                    cube32Count = 0;
                    OnMergeingOfCubes?.Invoke("32", transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            case "64":
                if (cube64Count == 0)
                {
                    Destroy(gameObject);
                    cube64Count++;
                }
                else
                {
                    cube64Count = 0;
                    OnMergeingOfCubes?.Invoke("64", transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                break;
            default:break;
        }
        
    }
}
