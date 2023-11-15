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
            // ī��Ʈ�ٿ� 3�ʸ� ���� UI�� �ؽ�Ʈ�� ��Ÿ���ִ� �Լ� ȣ��
            StartCoroutine(StartCountdown());
        }

        IEnumerator StartCountdown()
        {
            while (countdownSeconds > 0)
            {
                // �ؽ�Ʈ ������Ʈ
                countdownText.text = countdownSeconds.ToString();

                // 1�� ���
                yield return new WaitForSeconds(1f);

                // ī��Ʈ�ٿ� ����
                countdownSeconds--;
            }

            // ī��Ʈ�ٿ��� ������ "START" �ؽ�Ʈ�� ǥ��
            countdownText.gameObject.SetActive(false);
            startText.text = "START";
            // 1�� ���
             yield return new WaitForSeconds(1f);

            // "START" �ؽ�Ʈ�� �������� ����
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
