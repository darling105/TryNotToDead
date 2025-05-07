using UnityEngine;
using UnityEngine.UIElements;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.StopGame();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.ResumeGame();
    }

    public void Select(int i)
    {
        items[i].OnClick();
    }

    private void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }
        int[] random = new int[3];
        while (true)
        {
            random[0] = Random.Range(0, items.Length);
            random[1] = Random.Range(0, items.Length);
            random[2] = Random.Range(0, items.Length);

            if (random[0] != random[1] && random[1] != random[2] && random[0] != random[2])
                break;
        }

        for (int i = 0; i < random.Length; i++)
        {
            Item randomItem = items[random[i]];

            if (randomItem.level == randomItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                randomItem.gameObject.SetActive(true);
            }
        }
    }
}
