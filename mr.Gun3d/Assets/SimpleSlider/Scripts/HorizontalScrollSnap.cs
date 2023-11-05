using System;
using Infrastructure.GunShop;
using Infrastructure.UI.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.SimpleSlider.Scripts
{
	[RequireComponent(typeof(ScrollRect))]
	public class HorizontalScrollSnap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public ScrollRect ScrollRect;
		public GameObject Pagination;
		public int SwipeThreshold = 50;
		public float SwipeTime = 0.5f;

		private Toggle[] _pageToggles;

		private bool _drag;
		private bool _lerp;
		private int _page;
		private float _dragTime;
		private ShopWindow _shopWindow;
		public void Initialize(ShopWindow shopWindow)
		{
			_shopWindow = shopWindow;
			ScrollRect.horizontalNormalizedPosition = 0;
			_pageToggles = Pagination.GetComponentsInChildren<Toggle>(true);
			UpdateGunShop(_page);
			enabled = true;
		}
		public void FixedUpdate()
		{
			if (!_lerp || _drag) return;
			if (Pagination)
			{
				var page = GetCurrentPage();

				if (!_pageToggles[page].isOn)
				{
					UpdateGunShop(page);
				}
			}

			var horizontalNormalizedPosition = (float) _page / (ScrollRect.content.childCount - 1);

			ScrollRect.horizontalNormalizedPosition = Mathf.Lerp(ScrollRect.horizontalNormalizedPosition, horizontalNormalizedPosition, 0.1f);

			if (Math.Abs(ScrollRect.horizontalNormalizedPosition - horizontalNormalizedPosition) < 0.001f)
			{
				//ScrollRect.horizontalNormalizedPosition = horizontalNormalizedPosition;
				_lerp = false;
			}
		}
		
		public void SlideNext()
		{
			if (GetCurrentPage() == _pageToggles.Length - 1)
			{
				return;
			}
			Slide(1);
		}
		
		public void SlidePrev()
		{
			if (GetCurrentPage() == 0)
			{
				return;
			}
			Slide(-1);
		}

		private void Slide(int direction)
		{
			direction = Math.Sign(direction);

			if (_page == 0 && direction == -1 || _page == ScrollRect.content.childCount - 1 && direction == 1) return;

			_lerp = true;
			_page += direction;
		}

		private int GetCurrentPage()
		{
			return Mathf.RoundToInt(ScrollRect.horizontalNormalizedPosition * (ScrollRect.content.childCount - 1));
		}

		private void UpdateGunShop(int page)
		{
			if (Pagination)
			{
				_pageToggles[page].isOn = true;
			}
			_shopWindow.SelectGunWindow(page);
			
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_drag = true;
			_dragTime = Time.time;
		}

		public void OnDrag(PointerEventData eventData)
		{
			var page = GetCurrentPage();

			if (page != _page)
			{
				_page = page;
				UpdateGunShop(page);
			}
		}
		
		public void OnEndDrag(PointerEventData eventData)
		{
			var delta = eventData.pressPosition.x - eventData.position.x;

			if (Mathf.Abs(delta) > SwipeThreshold && Time.time - _dragTime < SwipeTime)
			{
				var direction = Math.Sign(delta);

				Slide(direction);
			}

			_drag = false;
			_lerp = true;
		}
	}
}