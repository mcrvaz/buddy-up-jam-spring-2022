using System.Collections;
using TMPro;
using UnityEngine;

public class BuyItemTextBehaviour : MonoBehaviour
{
    const string BUY_ITEM_FADE_IN = "BuyItemFadeIn";
    const string BUY_ITEM_FADE_OUT = "BuyItemFadeOut";

    const string TEXT = "Press E to buy {0} for ${1}";

    [field: SerializeField] public Animation Animation { get; private set; }
    [field: SerializeField] public GameObject BuyItemTextContainer { get; private set; }
    [field: SerializeField] public TextMeshProUGUI BuyItemText { get; private set; }

    ShopBehaviour shopBehaviour;
    Coroutine textHideRoutine;

    void Awake ()
    {
        shopBehaviour = FindObjectOfType<ShopBehaviour>();
    }

    void Start ()
    {
        shopBehaviour.OnItemSaleActiveChanged += HandleItemSaleActive;
    }

    void HandleItemSaleActive (ItemSaleBehaviour itemSale, bool active)
    {
        BuyItemText.text = string.Format(TEXT, itemSale.Settings.Name, itemSale.Settings.Price);

        if (textHideRoutine != null)
            StopCoroutine(textHideRoutine);

        if (active)
        {
            BuyItemTextContainer.SetActive(true);
            Animation.Play(BUY_ITEM_FADE_IN);
        }
        else
        {
            textHideRoutine = StartCoroutine(TextHideRoutine());
        }
    }

    IEnumerator TextHideRoutine ()
    {
        yield return Animation.PlayAndWait(BUY_ITEM_FADE_OUT);
        BuyItemTextContainer.SetActive(false);
    }
}