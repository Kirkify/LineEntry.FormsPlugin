using CoreAnimation;
using CoreGraphics;
using LineEntry.FormsPlugin.iOS;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LineEntry.FormsPlugin.LineEntry), typeof(LineEntryRenderer))]
namespace LineEntry.FormsPlugin.iOS
{
	public class LineEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				// Remove the border
				Control.BorderStyle = UITextBorderStyle.None;

				// Add Padding if requested
				UpdatePadding();

				// Setup event listeners
				AddEventListeners();

				// Draw the line
				DrawLine();
			}
		}

		private void AddEventListeners()
		{
			Control.EditingDidBegin += OnEditingDidBegin;
			Control.EditingDidEnd += OnEditingDidEnd;
		}

		private void RemoveEventListeners()
		{
			Control.EditingDidBegin -= OnEditingDidBegin;
			Control.EditingDidEnd -= OnEditingDidEnd;
		}

		private void OnEditingDidEnd(object sender, EventArgs e)
		{
			DrawLine();
		}

		private void OnEditingDidBegin(object sender, EventArgs e)
		{
			DrawLine(true);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			// Width or Height of the element has changed so redraw the line
			if (e.PropertyName == LineEntry.WidthProperty.PropertyName ||
			  e.PropertyName == LineEntry.HeightProperty.PropertyName ||
			  e.PropertyName == LineEntry.BorderColorProperty.PropertyName)
			{
				DrawLine();
			}
			if (e.PropertyName == LineEntry.IconPaddingProperty.PropertyName)
			{
				UpdatePadding();
			}
		}

		void DrawLine(bool scaleLine = false)
		{
			var lineEntry = Element as LineEntry;

			if (lineEntry != null)
			{
				var borderLayer = new CALayer();
				// Set the line color
				borderLayer.BorderColor = lineEntry.BorderColor.ToCGColor();
				// Make sure the line is visible
				borderLayer.BorderWidth = 1f;

				// Create the line, when scaleLine is set to true; line doubles in size
				// The scaling happens when the user focuses on the Entry
				var frame = new CGRect(0, lineEntry.Height, lineEntry.Width, scaleLine ? 2f : 1f);

				// If the line hasnt been added as a sublayer yet
				if (Control.Layer.Sublayers == null)
				{
					borderLayer.Frame = frame;
					Control.Layer.AddSublayer(borderLayer);
				}
				else
				{
					Control.Layer.Sublayers[0].Frame = frame;
				}
			}
		}

		private void UpdatePadding()
		{
			var element = Element as LineEntry;

			if (element != null)
			{
				Control.LeftView = new UIView(new CGRect(0, 0, element.IconPadding ? 25 : 0, 0));
				Control.LeftViewMode = UITextFieldViewMode.Always;
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				RemoveEventListeners();
			}

			// Dispose Control
			base.Dispose(disposing);
		}

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static new void Init() { }
	}
}
