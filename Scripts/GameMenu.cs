using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _score;

    private int scoreNumber = 0;
    private int coefficientDivisoin = 2;

    private void OnEnable()
    {
        CubeMergerer.OnMergeHappened += UpdateScore;
    }

    private void OnDisable()
    {
        CubeMergerer.OnMergeHappened -= UpdateScore;
    }

    private void UpdateScore(int tagNumber)
    {
        scoreNumber += tagNumber/coefficientDivisoin;
        _score.text = "Score : " +scoreNumber.ToString();
    }
}
