using Android.Graphics;
using LineEntry.FormsPlugin.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LineEntry.FormsPlugin.LineEntry), typeof(LineEntryRenderer))]
namespace LineEntry.FormsPlugin.Droid
{
	public class LineEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				var defaultColorFilter = Control.Background.ColorFilter;
				var element = Element as LineEntry;
				Control.Background.SetColorFilter(element.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
				Control.SetPadding(Control.PaddingLeft + (element.IconPadding ? 68 : 0), Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);

			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var element = Element as LineEntry;
			if (e.PropertyName == LineEntry.BorderColorProperty.PropertyName)
			{
				Control.Background.SetColorFilter(element.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
			}
			if (e.PropertyName == LineEntry.IconPaddingProperty.PropertyName)
			{
				Control.SetPadding(Control.PaddingLeft + (element.IconPadding ? 68 : 0), Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
			}
		}

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init() { }
	}
}
