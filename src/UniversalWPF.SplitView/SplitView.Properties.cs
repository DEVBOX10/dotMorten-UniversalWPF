﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace UniversalWPF
{
    public partial class SplitView
    {
		/// <summary>
		/// Gets or sets the width of the SplitView pane in its compact display mode.
		/// </summary>
		/// <value>
		/// The width of the pane in it's compact display mode. The default is 48 device-independent
		/// pixel (DIP) (defined by the SplitViewCompactPaneThemeLength resource).
		/// </value>
		public double CompactPaneLength
		{
			get { return (double)GetValue(CompactPaneLengthProperty); }
			set { SetValue(CompactPaneLengthProperty, value); }
		}

		/// <summary>
		/// Identifies the CompactPaneLength dependency property.
		/// </summary>
		/// <value>
		/// The identifier for the CompactPaneLength dependency property.
		/// </value>
		public static readonly DependencyProperty CompactPaneLengthProperty =
			DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(SplitView), new PropertyMetadata(0d, OnCompactPaneLengthPropertyChanged));

		private static void OnCompactPaneLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var splitview = ((SplitView)d);
			splitview.UpdateTemplateSettings();
		}

		//
		// Summary:
		//     Gets or sets the contents of the main panel of a SplitView.
		//
		// Returns:
		//     The contents of the main panel of a SplitView. The default is null.
		public UIElement Content
		{
			get { return (UIElement)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		// Summary:
		//     Identifies the Content dependency property.
		// Returns:
		//     The identifier for the Content dependency property.
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(UIElement), typeof(SplitView), new PropertyMetadata(null));

		//
		// Summary:
		//     Gets of sets a value that specifies how the pane and content areas of a SplitView
		//     are shown.
		//
		// Returns:
		//     A value of the enumeration that specifies how the pane and content areas of a
		//     SplitView are shown. The default is Overlay.
		public SplitViewDisplayMode DisplayMode
		{
			get { return (SplitViewDisplayMode)GetValue(DisplayModeProperty); }
			set { SetValue(DisplayModeProperty, value); }
		}

		// Summary:
		//     Identifies the DisplayMode dependency property.
		//
		// Returns:
		//     The identifier for the DisplayMode dependency property.
		public static readonly DependencyProperty DisplayModeProperty =
			DependencyProperty.Register("DisplayMode", typeof(SplitViewDisplayMode), typeof(SplitView), new PropertyMetadata(SplitViewDisplayMode.Overlay, OnDisplayModePropertyChanged));

		private static void OnDisplayModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((SplitView)d).UpdateDisplayMode((SplitViewDisplayMode)e.OldValue, (SplitViewDisplayMode)e.NewValue);
		}

		//
		// Summary:
		//     Gets or sets a value that specifies whether the SplitView pane is expanded to
		//     its full width.
		//
		// Returns:
		//     true if the pane is expanded to its full width; otherwise, false. The default
		//     is true.
		public bool IsPaneOpen
		{
			get { return (bool)GetValue(IsPaneOpenProperty); }
			set { SetValue(IsPaneOpenProperty, value); }
		}

		// Summary:
		//     Identifies the IsPaneOpen dependency property.
		//
		// Returns:
		//     The identifier for the IsPaneOpen dependency property.
		public static readonly DependencyProperty IsPaneOpenProperty =
			DependencyProperty.Register("IsPaneOpen", typeof(bool), typeof(SplitView), new PropertyMetadata(false, OnIsPaneOpenPropertyChanged));

		private static void OnIsPaneOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
				((SplitView)d).OpenPane();
			else
				((SplitView)d).ClosePane();
		}

		// Summary:
		//     Gets or sets the width of the SplitView pane when it's fully expanded.
		//
		// Returns:
		//     The width of the SplitView pane when it's fully expanded. The default is 320
		//     device-independent pixel (DIP).
		public double OpenPaneLength
		{
			get { return (double)GetValue(OpenPaneLengthProperty); }
			set { SetValue(OpenPaneLengthProperty, value); }
		}

		// Summary:
		//     Identifies the OpenPaneLength dependency property.
		//
		// Returns:
		//     The identifier for the OpenPaneLength dependency property.
		public static readonly DependencyProperty OpenPaneLengthProperty =
			DependencyProperty.Register("OpenPaneLength", typeof(double), typeof(SplitView), new PropertyMetadata(0d, OnOpenPaneLengthPropertyChanged));

		private static void OnOpenPaneLengthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			double value = (double)e.NewValue;
			var splitview = ((SplitView)d);
			if (splitview.TemplateSettings != null)
			{
				splitview.TemplateSettings.OpenPaneLength = value;
				splitview.UpdateTemplateSettings();
			}
		}

		// Summary:
		//     Gets or sets the Brush to apply to the background of the Pane area of the control.
		//
		// Returns:
		//     The Brush to apply to the background of the Pane area of the control.
		public Brush PaneBackground
		{
			get { return (Brush)GetValue(PaneBackgroundProperty); }
			set { SetValue(PaneBackgroundProperty, value); }
		}

		// Summary:
		//     Identifies the PaneBackground dependency property.
		//
		// Returns:
		//     The identifier for the PaneBackground dependency property.
		public static readonly DependencyProperty PaneBackgroundProperty =
			DependencyProperty.Register("PaneBackground", typeof(Brush), typeof(SplitView), new PropertyMetadata(null));

		// Summary:
		//     Gets or sets a value that specifies whether the pane is shown on the right or
		//     left side of the SplitView.
		//
		// Returns:
		//     A value of the enumeration that specifies whether the pane is shown on the right
		//     or left side of the SplitView. The default is Left.
		public SplitViewPanePlacement PanePlacement
		{
			get { return (SplitViewPanePlacement)GetValue(PanePlacementProperty); }
			set { SetValue(PanePlacementProperty, value); }
		}

		//
		// Summary:
		//     Identifies the PanePlacement dependency property.
		//
		// Returns:
		//     The identifier for the PanePlacement dependency property.
		public static readonly DependencyProperty PanePlacementProperty =
			DependencyProperty.Register("PanePlacement", typeof(SplitViewPanePlacement), typeof(SplitView), new PropertyMetadata(SplitViewPanePlacement.Left, OnPanePlacementPropertyChanged));

		private static void OnPanePlacementPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((SplitView)d).UpdatePanePlacement();
		}
		
		//
		// Summary:
		//     Gets or sets the contents of the pane of a SplitView.
		//
		// Returns:
		//     The contents of the pane of a SplitView. The default is null.
		public object Pane
		{
			get { return (object)GetValue(PaneProperty); }
			set { SetValue(PaneProperty, value); }
		}

		/// <summary>
		///  Identifies the <see cref="Pane"/> dependency property.
		/// </summary>
		/// <value>The identifier for the Pane dependency property.</value>
		public static readonly DependencyProperty PaneProperty =
			DependencyProperty.Register("Pane", typeof(object), typeof(SplitView), new PropertyMetadata(null));
		
		/// <summary>
		/// Gets an object that provides calculated values that can be referenced as TemplateBinding
		/// sources when defining templates for a SplitView control.
		/// </summary>
		/// <value>An object that provides calculated values for templates.</value>
		public SplitViewTemplateSettings TemplateSettings
		{
			get { return (SplitViewTemplateSettings)GetValue(TemplateSettingsProperty); }
			private set { SetValue(TemplateSettingsProperty, value); }
		}

		/// <summary>
		///  Identifies the <see cref="TemplateSettings"/> dependency property.
		/// </summary>
		/// <value>The identifier for the TemplateSettings dependency property.</value>
		public static readonly DependencyProperty TemplateSettingsProperty =
			DependencyProperty.Register("TemplateSettings", typeof(SplitViewTemplateSettings), typeof(SplitView), new PropertyMetadata(null));

	}
}
