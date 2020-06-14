using UnityEngine;

public class UILevelCreation : MonoBehaviour
{
    [SerializeField]
    private UILevelData template; // We will clone this...
    [SerializeField]
    private LevelDatabase levelDatabase;
    [SerializeField]
    private RectTransform content;

    private void Start()
    {
        var levels = this.levelDatabase.Levels;

        var iterator = levels.GetEnumerator();

        while (iterator.MoveNext())
        {
            var levelData = iterator.Current;
            var clone = Instantiate(this.template.gameObject);
            clone.SetActive(true);

            clone.transform.SetParent(this.content);
            clone.transform.localScale = Vector3.one;

            var uiLevelData = clone.GetComponent<UILevelData>();

            uiLevelData.SetLevelData(levelData);
        }

    }
}
