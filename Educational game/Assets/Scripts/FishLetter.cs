using TMPro;
using UnityEngine;

public class FishLetter : MonoBehaviour
{
    public char letter;
    public TextMeshProUGUI letterText1;
    public TextMeshProUGUI letterText2;

    private bool isCaught = false;
    private Transform rodTip;

    public void SetLetter(char newLetter)
    {
        letter = newLetter;

        if (letterText1 != null)
            letterText1.text = letter.ToString();

        if (letterText2 != null)
            letterText2.text = letter.ToString();

        if (letterText1 == null && letterText2 == null)
        {
            TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = letter.ToString();
        }
    }

    private void OnMouseDown()
    {
        if (isCaught) return; // avoid double clicks

        // Collect letter
        WordPuzzleManager.Instance.CollectLetter(letter);

        // Setup rod reference
        rodTip = HookLine.Instance.rodTip;

        // Show hook line
        HookLine.Instance.ShowLine(transform.position);

        // Start reel-in
        StartCoroutine(ReelIn());
    }

    private System.Collections.IEnumerator ReelIn()
    {
        isCaught = true;

        Vector3 start = transform.position;
        Vector3 end = rodTip.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f; // reel-in speed
            transform.position = Vector3.Lerp(start, end, t);

            // Update line renderer dynamically
            HookLine.Instance.ShowLine(transform.position);

            yield return null;
        }

        // Hide line after reaching rod tip
        HookLine.Instance.HideLine();

        // Return fish to pool
        ObjectPooler.Instance.ReturnToPool(gameObject);

        isCaught = false;
    }
}
