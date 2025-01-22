using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextIntroEffect : MonoBehaviour
{
    public Text[] introTexts;  // 5���� �ؽ�Ʈ UI ������Ʈ (I, N, T, R, O)
    public float animationDuration = 1f;  // �� �ؽ�Ʈ �ִϸ��̼� ���� �ð�
    public float delayBetweenAnimations = 0.3f;  // �ؽ�Ʈ�� ���� ����

    void Start()
    {
        // �� �ؽ�Ʈ���� �ڷ�ƾ�� 0.3�� �������� ����
        for (int i = 0; i < introTexts.Length; i++)
        {
            StartCoroutine(PlayTextIntroEffect(introTexts[i], i * delayBetweenAnimations,i+1));
        }
    }

    IEnumerator PlayTextIntroEffect(Text text, float delay, int i)
    {
        // �ִϸ��̼��� ������Ű�� ���� ��ٸ���
        yield return new WaitForSeconds(delay);

        // �ؽ�Ʈ�� �ʱ� ��ġ�� ȭ�� �ۿ� �ΰ� ũ�⸦ ũ�� ����
       
        text.transform.localPosition = new Vector3(-600f+(i*300), 0f, 0f);  // ȭ�� ���� ��
        text.transform.localScale = new Vector3(5f, 5f, 1f);  // ū ũ��
        float elapsedTime = 0f;

        // �ִϸ��̼��� ���� ������ �ݺ�
        while (elapsedTime < animationDuration)
        {
            // �ð��� ���� ��ġ�� ũ�� ����
            float t = elapsedTime / animationDuration;
            switch (i)
            {
                case 1:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(-600f, 0f, 0f), new Vector3(-360f, 190f, 0f), t);
                    break;
                case 2:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(-300f, 0f, 0f), new Vector3(-190f, 190f, 0f), t); 
                    break;
                case 3:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(0f, 0f, 0f), new Vector3(0f, 190f, 0f), t);
                    break;
                case 4:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(300f, 0f, 0f), new Vector3(190f, 190f, 0f), t); 
                    break;
                case 5:
                    text.transform.localPosition = Vector3.Lerp(new Vector3(600f, 0f, 0f), new Vector3(360f, 190f, 0f), t); 
                    break;

            }
            text.transform.localScale = Vector3.Lerp(new Vector3(5f, 5f, 1f), new Vector3(1f, 1f, 1f), t); // ũ�� �پ��
            elapsedTime += Time.deltaTime;
            yield return null;  // ���� �����ӱ��� ��ٸ�
        }

        // ���� ��ġ�� ũ��� ��Ȯ�� ���߱�
        switch (i)
        {
            case 1:
                text.transform.localPosition = new Vector3(-360f, 190f, 0f); // ȭ�� ����� �̵�
                break;

            case 2:
                text.transform.localPosition = new Vector3(-190f, 190f, 0f); // ȭ�� ����� �̵�
                break;
            case 3:
                text.transform.localPosition = new Vector3(0f, 190f, 0f); // ȭ�� ����� �̵�
                break;
            case 4:
                text.transform.localPosition = new Vector3(190f, 190f, 0f); // ȭ�� ����� �̵�
                break;
            case 5:
                text.transform.localPosition = new Vector3(360f, 190f, 0f); // ȭ�� ����� �̵�
                break;

        }
        text.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
