using System;
using MediaPlayer;
using UIKit;

namespace POCFlorence.iOS.Utilities
{
    public class OpenMenuGestureRecognizer : UIPanGestureRecognizer
    {
        public OpenMenuGestureRecognizer(Action<UIPanGestureRecognizer> callback,
            Func<UIGestureRecognizer, UITouch, bool> shouldReceiveTouch)
            : base(callback)
        {
            this.ShouldReceiveTouch += (sender, touch) =>
            {
                bool isMovingCell =
                    touch.View.ToString()
                        .IndexOf("UITableViewCellReorderControl", StringComparison.InvariantCultureIgnoreCase) > -1;
                if (touch.View is UISlider || touch.View is MPVolumeView || isMovingCell)
                    return false;
                return shouldReceiveTouch(sender, touch);
            };
        }
    }
}