using System.Collections;
using UnityEngine;

public class SwipeDownCube : MonoBehaviour
{
    private void OnEnable()
    {
        Gameplay.OnSwipeDown += MoveCube;
    }
    private void OnDisable()
    {
        Gameplay.OnSwipeDown -= MoveCube;
    }

    private void MoveCube()
    {
        transform.position += new Vector3(0, 0, 1.1f);
        StartCoroutine(MoveingCube());
    }
    private IEnumerator MoveingCube()
    {
        yield return new WaitForSeconds(1);
        transform.position -= new Vector3(0, 0, 1.1f);
    }
}
