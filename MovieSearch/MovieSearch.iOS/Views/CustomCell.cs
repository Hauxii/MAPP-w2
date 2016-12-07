using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MovieSearch.iOS.Views
{
    public class CustomCell : UITableViewCell
    {
        private UILabel _titleLabel, _castLabel;
        private UIImageView _imageView;

        public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this._imageView = new UIImageView();

            this._titleLabel = new UILabel()
            {
                Font = UIFont.FromName("Marion", 22f),
                TextColor = UIColor.FromRGB(60, 60, 60),
            };

            this._castLabel = new UILabel()
            {
                Font = UIFont.FromName("Marion-Italic", 15f), 
                TextColor = UIColor.FromRGB(130, 130, 130), 
            };
			            
            this.ContentView.AddSubviews(new UIView[] {this._imageView, this._titleLabel, this._castLabel});
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            this._imageView.Frame = new CGRect(5, 5, 30, 40);
            this._titleLabel.Frame = new CGRect(40, 5, this.ContentView.Bounds.Width - 60, 25);
            this._castLabel.Frame = new CGRect(40, 27, this.ContentView.Bounds.Width - 60, 20);
        }

		public void UpdateCell(string image, string title, List<string> cast)
        {
            this._imageView.Image = UIImage.FromFile(image);
            this._titleLabel.Text = title;
			for (int i = 0; i < cast.Count; i++)
			{
				this._castLabel.Text += cast[i];
				if (i + 1 != cast.Count)
				{
					this._castLabel.Text += ", ";
				}
			}

			this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}
