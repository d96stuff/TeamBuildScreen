using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace TeamBuildScreen.Core.Views
{
    public class ScalingTextBlock : ContentControl
    {
        /// <summary>
        /// A TextBlock that is set as the control's content and is ultimately the control 
        /// that displays our text
        /// </summary>
        private TextBlock textBlock;

        /// <summary>
        /// Gets or sets the Text DependencyProperty. This is the text that will be displayed.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ScalingTextBlock),
            new PropertyMetadata(null, new PropertyChangedCallback(OnTextChanged)));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScalingTextBlock)d).OnTextChanged(e);
        }

        protected virtual void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            this.textBlock.Text = this.Text;
        }

        /// <summary>
        /// Initializes a new instance of the DynamicTextBlock class
        /// </summary>
        public ScalingTextBlock()
        {
            this.textBlock = new TextBlock();
            this.textBlock.VerticalAlignment = VerticalAlignment.Center;
            this.textBlock.TextTrimming = TextTrimming.CharacterEllipsis;
            this.Content = this.textBlock;

            this.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }

        /// <summary>
        /// Sets the FontSize of the internal TextBlock to 80% of the available height.
        /// </summary>
        /// <param name="constraint">The available size.</param>
        /// <returns>The base implementation of MeasureOverride.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            // set FontSize to 80% of the available height
            this.textBlock.FontSize = constraint.Height * 0.8;

            return base.MeasureOverride(constraint);
        }
    }
}