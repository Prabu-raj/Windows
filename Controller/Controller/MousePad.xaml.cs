using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using Windows.Networking.Connectivity;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Threading;

namespace Controller
{
    public partial class SecondPage : PhoneApplicationPage
    {
        // private SocketClient client = null;
        private int pointerCount;

        public SecondPage()
        {
            InitializeComponent();

            MousePad.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(Drag_ManipulationDelta);
            Touch.FrameReported += Touch_FrameReported;

            //this.client = NavigationExtensions.GetLastNavigationData(this.NavigationService) as SocketClient;

        }

        void Drag_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

            if (pointerCount == 1)
            {
                MouseSignal signal = new MouseSignal(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y, MouseSignal.DRAG);
                App.client.Send(JsonConvert.SerializeObject(signal) + "#");
            }
            else if (pointerCount == 2)
            {
                App.client.Send(JsonConvert.SerializeObject(new MouseSignal(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y, MouseSignal.SCROLL)) + "#");
            }
        }

        void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            try
            {
                TouchPointCollection points1 = e.GetTouchPoints(this);
            }
            catch
            {
                return;
            }

            TouchPointCollection points = e.GetTouchPoints(this);
            if ((from p in points where p.Action != TouchAction.Up select p) != null)
                pointerCount = (from p in points where p.Action != TouchAction.Up select p).Count();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (e.Content.ToString().Equals("Controller.Option"))
            {
                App.client.Send(JsonConvert.SerializeObject(new MouseSignal(MouseSignal.END_SIMULATION)) + "#");
            }
        }

        private void MousePad_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.client.Send(JsonConvert.SerializeObject(new MouseSignal(MouseSignal.HOLD)) + "#");
        }

        private void MousePad_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.client.Send(JsonConvert.SerializeObject(new MouseSignal(MouseSignal.TAP)) + "#");
        }

        private void MousePad_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            App.client.Send(JsonConvert.SerializeObject(new MouseSignal(MouseSignal.DOUBLE_TAP)) + "#");
        }

    }
}