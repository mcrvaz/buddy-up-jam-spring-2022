using System.Collections;
using TMPro;
using UnityEngine;
using VContainer;

public class BuyItemTextBehaviour : MonoBehaviour
{
    const string BUY_ITEM_FADE_IN = "BuyItemFadeIn";
    const string BUY_ITEM_FADE_OUT = "BuyItemFadeOut";

    const string TEXT = "Press E to buy {0} for ${1}";

    [field: SerializeField] public Animation Animation { get; private set; }
    [field: SerializeField] public GameObject BuyItemTextContainer { get; private set; }
    [field: SerializeField] public TextMeshProUGUI BuyItemText { get; private set; }

    [Inject]
    ShopBehaviour shopBehaviour;

    Coroutine textHideRoutine;
    int triggerCount;

    void Start ()
    {
        shopBehaviour.OnItemSaleActiveChanged += HandleItemSaleActive;
    }

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active)
    {
        if (active)
            triggerCount++;
        else
            triggerCount--;

        if (textHideRoutine != null)
            StopCoroutine(textHideRoutine);

        if (active)
        {
            BuyItemText.text = string.Format(TEXT, itemSale.Settings.Name, itemSale.Settings.Price);
            BuyItemTextContainer.SetActive(true);
            Animation.Stop();
            Animation.Play(BUY_ITEM_FADE_IN);
        }
        else
        {
            if (triggerCount > 0)
                return;
            BuyItemTextContainer.SetActive(false);
            textHideRoutine = StartCoroutine(TextHideRoutine());
        }
    }

    IEnumerator TextHideRoutine ()
    {
        yield return Animation.PlayAndWait(BUY_ITEM_FADE_OUT);
        BuyItemTextContainer.SetActive(false);
    }
}