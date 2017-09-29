﻿using System;
using Telerik.XamarinForms.Common;
using Telerik.XamarinForms.Input.DataForm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerEditPage : ContentPage
    {
        public CustomerEditPage()
        {
            InitializeComponent();

            //var positive = "CCFF00";
            //var negative = "FF004C";

            //var style = new DataFormEditorStyle
            //{
            //    Background = new Background
            //    {
            //        Fill = Color.FromHex("3D6978"),
            //        StrokeColor = Color.FromHex(positive),
            //        StrokeWidth = 2,
            //        StrokeLocation = Location.Bottom
            //    },
            //    HeaderFontSize = 17,
            //    HeaderForeground = Color.White,
            //    FeedbackFontSize = 13,
            //    PositiveFeedbackImage = ImageSource.FromFile("success.png"),
            //    NegativeFeedbackImage = ImageSource.FromFile("fail.png"),
            //    NegativeFeedbackForeground = Color.FromHex(negative),
            //    NegativeFeedbackBackground = new Background
            //    {
            //        Fill = Color.FromHex(30 + negative),
            //        StrokeColor = Color.FromHex(negative),
            //        StrokeWidth = 2,
            //        StrokeLocation = Location.All
            //    },
            //    Height = 70,
            //    FeedbackImageSize = new Size(10, 10),

            //};

            //DataForm.EditorStyle = style;
            //DataForm.BackgroundColor = Color.FromHex("345966");
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", DataForm.Source);
            await Navigation.PopToRootAsync();
        }
    }
}