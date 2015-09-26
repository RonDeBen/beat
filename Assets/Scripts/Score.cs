using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    public static int streakToIncreaseMultiplier, maxMultiplier;
    public static int pointsForHurdle, pointsForEnemy;
    private static int combo, currentMultiplier, score;

	public Text scoreText, comboText, multiplierText;

    void Start(){
        pointsForEnemy = 200;
        pointsForHurdle = 50;
        streakToIncreaseMultiplier = 8;
        maxMultiplier = 4;
    }

    void Update(){
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo;
        multiplierText.text = "Multiplier: " + currentMultiplier + "x";
    }

    public static void EnemyPoints(){
        score +=  pointsForEnemy * currentMultiplier;
    }

    public static void HurdlePoints(){
        IncrementCombo();
        score += pointsForHurdle * currentMultiplier;
    }

    public static void ResetCombo(){
        combo = 0;
        currentMultiplier = 1;
    }

    public static void IncrementCombo(){
        combo++;
        currentMultiplier = Mathf.Clamp((combo / streakToIncreaseMultiplier) + 1, 1, maxMultiplier);
    }
}
