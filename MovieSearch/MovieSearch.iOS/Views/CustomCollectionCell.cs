using System;
using System.Collections.Generic;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MovieSearch.iOS.Views
{
	public class CustomCollectionCell : UICollectionViewCell
    {
        private UILabel _titleLabel;
        private UIImageView _imageView;

		[Export("initWithFrame:")]

		public CustomCollectionCell(CGRect frame) : base(frame)
		{
            this._imageView = new UIImageView();

			this._titleLabel = new UILabel()
			{
				BackgroundColor = UIColor.White,
				TextAlignment = UITextAlignment.Left,
                Font = UIFont.FromName("Marion", 22f),
                TextColor = UIColor.FromRGB(60, 60, 60),
            };

			            
            this.ContentView.AddSubviews(new UIView[] {this._imageView, this._titleLabel});
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this._imageView.Frame = new CGRect(0, 0, 
			                                   this.ContentView.Bounds.Width - 20,
			                                   this.ContentView.Bounds.Height - 20);
			this._titleLabel.Frame = new CGRect(0, this.ContentView.Bounds.Height - 20,
			                                    this.ContentView.Bounds.Width, 
			                                    20);
        }

		public void UpdateCell(string image, string title, List<string> cast)
        {
            this._imageView.Image = UIImage.FromFile(image); //TODO: GET IMAGE FROM API
            this._titleLabel.Text = title;
        }
    }
}
