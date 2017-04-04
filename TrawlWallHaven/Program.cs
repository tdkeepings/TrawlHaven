using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Threading;

namespace TrawlWallHaven {
	class Program {

		static void Main(string[] args) {
			string rootDir = "C:/Dump/Wallhaven/macro/";
			string wallhavenWallPaperUrl = "https://wallpapers.wallhaven.cc/wallpapers/full/wallhaven-{0}.jpg";
			string wallhavenPages = "https://alpha.wallhaven.cc/search?q=macro&page={0}";
			HtmlWeb web = new HtmlWeb();

			for (int i = 1; i < 20; i++) {

				string page = String.Format(wallhavenPages, i);

				HtmlDocument doc = web.Load(page);

				if (doc != null) {
					var node = doc.DocumentNode;

					var links = node.SelectNodes("//a[@href][@class=\"preview\"]");

					if (links != null) {
						foreach (HtmlNode link in links) {

							Thread.Sleep(2000); // Feels bad man (v_v)

							string imageId = link.Attributes["href"].Value.Split('/').Last();
							string imgUrl = String.Format(wallhavenWallPaperUrl, imageId);


							if (!File.Exists(rootDir + imageId + ".jpg")) {
								using (WebClient client = new WebClient()) {
									try {
										client.DownloadFile(new Uri(imgUrl), rootDir + imageId + ".jpg");
									} catch (Exception ex) {
									
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
