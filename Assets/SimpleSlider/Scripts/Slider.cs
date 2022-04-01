using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleSlider.Scripts
{
	
	public class Slider : MonoBehaviour
	{
		[Header("Settings")]
		public List<Banner> Banners;

		[Header("UI")]
		public Transform BannerGrid;
		public Button BannerPrefab;

		public HorizontalScrollSnap HorizontalScrollSnap;
		

		public IEnumerator Start()
		{
			/*foreach (Transform child in BannerGrid)
				Destroy(child.gameObject);


			foreach (var banner in Banners)
			{
				var instance = Instantiate(BannerPrefab, BannerGrid);
				var button = instance.GetComponent<Button>();

				button.onClick.RemoveAllListeners();


				instance.GetComponent<Image>().sprite = banner.Sprite;
			}*/

			yield return null;

			HorizontalScrollSnap.Initialize();
		}
	}
}