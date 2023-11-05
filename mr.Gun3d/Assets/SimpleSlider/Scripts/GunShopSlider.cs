using System.Collections;
using System.Collections.Generic;
using Infrastructure.UI.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

namespace Assets.SimpleSlider.Scripts
{
	/// <summary>
	/// Creates banners and paginator by given banner list.
	/// </summary>
	public class GunShopSlider : MonoBehaviour
	{
		[Header("Settings")]
		private List<GunSellWindow> _banners;
		public bool Elastic = true;
		[Header("UI")]
		public Transform BannerGrid;
		public Transform[] GunPlaces;
		public Transform PaginationGrid;
		public Toggle PagePrefab;
		public HorizontalScrollSnap HorizontalScrollSnap;

		public void OnValidate()
		{
			GetComponent<ScrollRect>().content.GetComponent<GridLayoutGroup>().cellSize = GetComponent<RectTransform>().sizeDelta;
		}
		public void Initialize(List<GunSellWindow> banners,ShopWindow shopWindow)
		{
			_banners = banners;

			foreach (Transform child in PaginationGrid)
			{
				Destroy(child.gameObject);
			}

			foreach (var banner in _banners)
			{
				if (_banners.Count > 1)
				{
					var toggle = Instantiate(PagePrefab, PaginationGrid);
					toggle.group = PaginationGrid.GetComponent<ToggleGroup>();
				}
			}


			StartCoroutine(Init(shopWindow));
		}

		private IEnumerator Init(ShopWindow shopWindow)
		{
			yield return null;
			HorizontalScrollSnap.Initialize(shopWindow);
			HorizontalScrollSnap.GetComponent<ScrollRect>().movementType = Elastic ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Clamped;
		}
		public Transform[] GetGridTransform() => GunPlaces;
	}
}