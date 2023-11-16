using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameUIManager : MonoBehaviour
{
        public Text countdownText;
        public Text startText;
        private int countdownSeconds = 3;

        public Image Loser;

        void Start()
        {
            // 카운트다운 3초를 세어 UI에 텍스트로 나타내주는 함수 호출
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            while (countdownSeconds > 0)
            {
                // 텍스트 업데이트
                countdownText.text = countdownSeconds.ToString();

                // 1초 대기
                yield return new WaitForSeconds(1f);

                // 카운트다운 감소
                countdownSeconds--;
            }

            // 카운트다운이 끝나면 "START" 텍스트를 표시
            countdownText.gameObject.SetActive(false);
            startText.text = "START";
            // 1초 대기
             yield return new WaitForSeconds(1f);

            // "START" 텍스트를 공백으로 설정
            startText.text = "";


        }

    public void LooserUI()
    {
        Loser.gameObject.SetActive(true);
        SoundMgr.Instance.LoserSound();
        StartCoroutine(BacktoTitle());
    }

    IEnumerator BacktoTitle()
    {
        yield return new WaitForSeconds(4f);

        Loser.gameObject.SetActive(false);
        SceneManager.LoadScene("Title");
    }
}
