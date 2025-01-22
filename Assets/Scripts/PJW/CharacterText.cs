
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterText : MonoBehaviour
{
    
    public RectTransform m_rectTransform;
    public CanvasGroup m_canvasGroup;
    public Text m_contentText;
    private bool m_isAction = false;
    public List<string> m_contextList; 
    
    public void Alert()
    {
        if (!m_isAction && gameObject.activeSelf){
            int random = Random.Range(0,m_contextList.Count);
            StartCoroutine(AnimateUIElements(m_contextList[random]));
        }
    }

    public void Alert(string _context)
    {
        if (!m_isAction && gameObject.activeSelf){
            StartCoroutine(AnimateUIElements(_context));
        }
    }

    public void Init()
    {
        m_rectTransform.anchoredPosition = Vector2.zero;
        m_canvasGroup.alpha = 1f;
    }

    private IEnumerator AnimateUIElements(string _context)
    {
        m_isAction = true;

        m_contentText.text = _context;
        Init();

        yield return new WaitForSeconds(0.5f);

        // 각 애니메이션 코루틴 시작
        Coroutine move = StartCoroutine(MoveY(m_rectTransform, 0.25f, 0.8f));
        Coroutine fadeOut = StartCoroutine(FadeCanvasGroup(m_canvasGroup, 0.0f, 0.8f));

        // 모든 코루틴이 완료될 때까지 기다림
        yield return move;
        yield return fadeOut;

        m_isAction = false;
    }

    private IEnumerator MoveY(RectTransform rectTransform, float targetY, float duration)
    {
        Vector3 startPosition = rectTransform.localPosition;
        Vector3 endPosition = new Vector3(startPosition.x, targetY, startPosition.z);
        float time = 0;

        while (time < duration)
        {
            rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.localPosition = endPosition;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

}
