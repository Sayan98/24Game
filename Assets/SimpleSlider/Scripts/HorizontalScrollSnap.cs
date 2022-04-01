using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/*namespace Assets.SimpleSlider.Scripts
{
	
	[RequireComponent(typeof(ScrollRect))]*/
	public class HorizontalScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler
	{
		public ScrollRect ScrollRect;
		public int SwipeThreshold = 50;
		public float SwipeTime = 0.5f;

		public bool _drag;
		public bool _lerp;
		public int _page;
		public float _dragTime;

		public void Initialize() {

			ScrollRect.horizontalNormalizedPosition = 0;
			enabled = true;
		}

		private void Update() {

			if (!_lerp || _drag) return;
			
			var horizontalNormalizedPosition = (float) _page / (ScrollRect.content.childCount - 1);
			Debug.Log("hor" + horizontalNormalizedPosition);

			if(horizontalNormalizedPosition == 0.5f)
				horizontalNormalizedPosition = 0;
			else if(horizontalNormalizedPosition == 0f)			
					horizontalNormalizedPosition = 1980;
			else if(horizontalNormalizedPosition == 1f)
					horizontalNormalizedPosition = -1980;
			else if(horizontalNormalizedPosition == 0.75f)
					horizontalNormalizedPosition = -990;
			else if(horizontalNormalizedPosition == 0.25f)
					horizontalNormalizedPosition = 990;

			var pos = gri.anchoredPosition;
			pos.x = Mathf.Lerp(gri.anchoredPosition.x, horizontalNormalizedPosition, 10 * Time.deltaTime);
			gri.anchoredPosition = pos;

			if(_lerp)
				UINavigationInput((int)horizontalNormalizedPosition);

			if (Math.Abs(gri.anchoredPosition.x - horizontalNormalizedPosition) < 5f) {
				
				Debug.Log("lerping"+ horizontalNormalizedPosition);

				var xx = gri.anchoredPosition;
				xx.x = horizontalNormalizedPosition;
				gri.anchoredPosition = xx;
				
				_lerp = false;
			}
		}

		public RectTransform gri;

		private void Slide(int direction) {
			
			direction = Math.Sign(direction);

			if (_page == 0 && direction == -1 || _page == ScrollRect.content.childCount - 1 && direction == 1) return;

			_lerp = true;
			_page += direction;
		}


		public void OnBeginDrag(PointerEventData eventData) {
				Debug.Log("begin");
			_drag = true;
			_dragTime = Time.time;
		}

		
		public void OnEndDrag(PointerEventData eventData) {
			Debug.Log("end");
			var delta = eventData.pressPosition.x - eventData.position.x;
			if (Mathf.Abs(delta) > SwipeThreshold && Time.time - _dragTime < SwipeTime) {

				var direction = Math.Sign(delta);
				Slide(direction);
			}

			_drag = false;
			_lerp = true;
		}


		#region Navigation
		public Image _navBG;
		public Text _navText;
		public Image[] _navButtonRect = new Image[5];
    	public void UINavigationInput(int i) {
			
			var color = new Color[5];
			color[0] = new Color(0, 0.9019608f, 0.4627451f, 0.1f);
			color[1] = new Color(1, 0.9333333f, 0.345098f,  0.1f);
			color[2] = new Color(0.2980392f, 0.6862745f, 0.3137255f, 0.1f);
			color[3] = new Color(0, 0.7372549f, 0.8313726f, 0.1f);
			color[4] = new Color(0.9568627f, 0.2627451f, 0.2117647f, 0.1f);

			if(!_lerp) {
			
				var xx = gri.anchoredPosition;
				xx.x = i;
				gri.anchoredPosition = xx;
			}
			
			var pos = _navBG.rectTransform.anchoredPosition;
			switch (i) {
				
				case 1980 : pos.x = -370;
							_page = 0;
							_navText.text = "Home";
							_navText.color = new Color(color[_page].r, color[_page].g, color[_page].b, 1);
							_navBG.color = color[_page];
							_navButtonRect[0].rectTransform.DOAnchorPosX(-440, 0.25f);
							_navButtonRect[1].rectTransform.DOAnchorPosX(-200, 0.25f);
							_navButtonRect[2].rectTransform.DOAnchorPosX(0, 0.25f);
							_navButtonRect[3].rectTransform.DOAnchorPosX(200, 0.25f);
							_navButtonRect[4].rectTransform.DOAnchorPosX(400, 0.25f);
				break;

				case 990 :	pos.x = -170;
							_page = 1;
							_navText.text = "Stats";
							_navText.color = new Color(color[_page].r, color[_page].g, color[_page].b, 1);
							_navBG.color = color[_page];
							_navButtonRect[0].rectTransform.DOAnchorPosX(-400, 0.25f);
							_navButtonRect[1].rectTransform.DOAnchorPosX(-240, 0.25f);
							_navButtonRect[2].rectTransform.DOAnchorPosX(0, 0.25f);
							_navButtonRect[3].rectTransform.DOAnchorPosX(200, 0.25f);
							_navButtonRect[4].rectTransform.DOAnchorPosX(400, 0.25f);
				break;

				case 0 : 	pos.x = 30;
							_page = 2;
							_navText.text = "Store";
							_navText.color = new Color(color[_page].r, color[_page].g, color[_page].b, 1);
							_navBG.color = color[_page];
							_navButtonRect[0].rectTransform.DOAnchorPosX(-400, 0.25f);
							_navButtonRect[1].rectTransform.DOAnchorPosX(-200, 0.25f);
							_navButtonRect[2].rectTransform.DOAnchorPosX(-40, 0.25f);
							_navButtonRect[3].rectTransform.DOAnchorPosX(200, 0.25f);
							_navButtonRect[4].rectTransform.DOAnchorPosX(400, 0.25f);
				break;

				case -990 : pos.x = 230;
							_page = 3;
							_navText.text = "Rank";
							_navText.color = new Color(color[_page].r, color[_page].g, color[_page].b, 1);
							_navBG.color = color[_page];
							_navButtonRect[0].rectTransform.DOAnchorPosX(-400, 0.25f);
							_navButtonRect[1].rectTransform.DOAnchorPosX(-200, 0.25f);
							_navButtonRect[2].rectTransform.DOAnchorPosX(0, 0.25f);
							_navButtonRect[3].rectTransform.DOAnchorPosX(160, 0.25f);
							_navButtonRect[4].rectTransform.DOAnchorPosX(400, 0.25f);
				break;

				case -1980 : pos.x = 370;
							_page = 4;
							_navText.text = "Settings";
							_navText.color = new Color(color[_page].r, color[_page].g, color[_page].b, 1);
							_navBG.color = color[_page];
							_navButtonRect[0].rectTransform.DOAnchorPosX(-400, 0.25f);
							_navButtonRect[1].rectTransform.DOAnchorPosX(-200, 0.25f);
							_navButtonRect[2].rectTransform.DOAnchorPosX(0, 0.25f);
							_navButtonRect[3].rectTransform.DOAnchorPosX(200, 0.25f);
							_navButtonRect[4].rectTransform.DOAnchorPosX(290, 0.25f);
				break;
			
			}

			_navBG.rectTransform.anchoredPosition = pos;
    	}
    	#endregion

	}
//}