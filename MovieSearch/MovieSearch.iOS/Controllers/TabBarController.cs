using System;
using System.Collections.Generic;
using System.Text;
using MovieSearch.Model;

using UIKit;

namespace MovieSearch.iOS.Controllers
{
	public class TabBarController : UITabBarController
    {
       public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TabBar.BackgroundColor = UIColor.LightGray;
			this.TabBar.TintColor = UIColor.Blue;

			//index of the tabbar that is selected at the beginning
			this.SelectedIndex = 0;
		}
    }
}