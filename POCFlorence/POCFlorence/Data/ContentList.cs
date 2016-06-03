using System;
using System.Collections.Generic;

namespace POCFlorence
{
	public class ContentList
	{
		public ContentList ()
		{
		}
		public static List<PageContentModel> GetContentList(string channelName)
		{
			var contentList = new List<PageContentModel> ();
			switch(channelName)
			{
			case "Title1" : 
			contentList = new List<PageContentModel> ();
			contentList.Add (new PageContentModel { Title = "One", Body = "Something", ImageName = "hand.png" });
			contentList.Add (new PageContentModel { Title = "two", Body = "Something", ImageName = "hand.png" });
			contentList.Add (new PageContentModel { Title = "three", Body = "Something", ImageName = "hand.png" });

			break;
			case "Title2" : 
			contentList = new List<PageContentModel> ();
				contentList.Add (new PageContentModel { Title = "1", Body = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "2", Body = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "3", Body = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet..comes from a line in section 1.10.32.", ImageName = "hand.png" });

			break;
			case "Title3" : 
			contentList = new List<PageContentModel> ();
			contentList.Add (new PageContentModel { Title = "Menu item 3a", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 3b", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 3c", Body = "Something", ImageName = "hand.png" });

			break;
			case "Title4" : 
			contentList = new List<PageContentModel> ();
				contentList.Add (new PageContentModel { Title = "Menu item 4a", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 4b", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 4c", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 4d", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 4e", Body = "Something", ImageName = "hand.png" });
				contentList.Add (new PageContentModel { Title = "Menu item 4f", Body = "Something", ImageName = "hand.png" });

			break;

			}
			return contentList;
		}
	}
}

