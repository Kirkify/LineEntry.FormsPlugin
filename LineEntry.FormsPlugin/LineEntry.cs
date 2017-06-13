using Xamarin.Forms;

namespace LineEntry.FormsPlugin
{
	public class LineEntry : Entry
	{
		public static readonly BindableProperty BorderColorProperty =
		  BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(LineEntry), Color.Default);

		public Color BorderColor
		{
			get
			{
				return (Color)GetValue(BorderColorProperty);
			}
			set
			{
				SetValue(BorderColorProperty, value);
			}
		}

		public static readonly BindableProperty IconPaddingProperty =
		  BindableProperty.Create(nameof(IconPadding), typeof(bool), typeof(LineEntry), false);

		public bool IconPadding
		{
			get
			{
				return (bool)GetValue(IconPaddingProperty);
			}
			set
			{
				SetValue(IconPaddingProperty, value);
			}
		}
	}
}
