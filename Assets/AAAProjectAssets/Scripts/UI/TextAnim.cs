using System.Collections;
using UnityEngine;
using TMPro;

public class TextAnim : MonoBehaviour
{
    private TMP_Text _text;
    public float waitTime = 0.01f;
    public int characterSkip = 10;
    TMP_TextInfo textInfo;

    void Awake()
    {
        _text = gameObject.GetComponent<TMP_Text>();
        _text.color = new Color(120, 120, 120, 0);
        textInfo = _text.textInfo;
    }

    public void ChangeText(string newText)
    {
        _text.text = newText;
        textInfo = _text.textInfo;
    }

    public void StartFading(float keepTime)
    {
        StartCoroutine(StartFade(keepTime));
    }
    private IEnumerator StartFade(float keepTime)
    {
        StartCoroutine(AnimateText());
        yield return new WaitForSeconds(keepTime);
        StartCoroutine(AnimateTextOut());
    }

    private IEnumerator AnimateText()
    {
        _text.ForceMeshUpdate();
        TMP_CharacterInfo info;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            info = textInfo.characterInfo[i];
            if(info.isVisible)
                StartCoroutine(FadeInLetters(info));

            yield return new WaitForSeconds(waitTime);
        }

    }

    private IEnumerator FadeInLetters(TMP_CharacterInfo info)
    {
        for (int i = 0; i <= 255; i += characterSkip)
        {
            int meshIndex = textInfo.characterInfo[info.index].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[info.index].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0].a = (byte)i;
            vertexColors[vertexIndex + 1].a = (byte)i;
            vertexColors[vertexIndex + 2].a = (byte)i;
            vertexColors[vertexIndex + 3].a = (byte)i;
            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator AnimateTextOut()
    {
        //_text.ForceMeshUpdate();
        TMP_CharacterInfo info;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            info = textInfo.characterInfo[i];
            if (info.isVisible)
                StartCoroutine(FadeOutLetters(info));

            yield return new WaitForSeconds(waitTime);
        }

    }


    private IEnumerator FadeOutLetters(TMP_CharacterInfo info)
    {
        for (int i = 255; i >= 0; i -= characterSkip)
        {
            int meshIndex = textInfo.characterInfo[info.index].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[info.index].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0].a = (byte)i;
            vertexColors[vertexIndex + 1].a = (byte)i;
            vertexColors[vertexIndex + 2].a = (byte)i;
            vertexColors[vertexIndex + 3].a = (byte)i;
            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = characterSkip; i >= 0; i--)
        {
            int meshIndex = textInfo.characterInfo[info.index].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[info.index].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0].a = (byte)i;
            vertexColors[vertexIndex + 1].a = (byte)i;
            vertexColors[vertexIndex + 2].a = (byte)i;
            vertexColors[vertexIndex + 3].a = (byte)i;
            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
