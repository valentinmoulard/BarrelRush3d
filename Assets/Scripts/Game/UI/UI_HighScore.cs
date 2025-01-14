
using UnityEngine;
using TMPro;
using System.Text;
using Base.GameManagement;

namespace Game.Ui
{
    public class UI_HighScore : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_highScoreText = null;



        private void OnEnable()
        {
            ManagersAccess.HighScoreManager.OnSendHighScore += DisplayHighScore;
        }

        private void OnDisable()
        {
            ManagersAccess.HighScoreManager.OnSendHighScore -= DisplayHighScore;
        }


        private void DisplayHighScore(float highScore, bool isNewHighScore)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Clear();

            if (isNewHighScore)
                stringBuilder.Append("New ");

            stringBuilder.Append("High Score\n");
            stringBuilder.Append(FormatTime(highScore));
            m_highScoreText.text = stringBuilder.ToString();
        }


        public string FormatTime(float time)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Clear();


            int minutes = Mathf.FloorToInt(time / 60);
            string minutesText = minutes.ToString();


            int seconds = Mathf.FloorToInt(time % 60);
            if (seconds < 10) stringBuilder.Append("0");

            stringBuilder.Append(seconds.ToString());

            string secondsText = stringBuilder.ToString();


            stringBuilder.Clear();
            stringBuilder.Append(minutesText);
            stringBuilder.Append(":");
            stringBuilder.Append(secondsText);

            return stringBuilder.ToString();
        }
    }
}