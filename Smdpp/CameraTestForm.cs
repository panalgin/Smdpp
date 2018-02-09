using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace Smdpp
{
    public partial class CameraTestForm : Form
    {
        public VideoCaptureDevice Camera = null;
        public FilterInfoCollection UsbDevices = null;

        public CameraTestForm()
        {
            InitializeComponent();
        }

        private void CameraTestForm_Load(object sender, EventArgs e)
        {
            UsbDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            Camera = new VideoCaptureDevice(UsbDevices[1].MonikerString);
            Camera.NewFrame += Camera_NewFrame;
            Camera.Start();
        }


        private void Camera_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if (this.pictureBox1.Image != null)
                this.pictureBox1.Image.Dispose();

            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            this.pictureBox1.Image = bitmap;

            //this.pictureBox1.Image = eventArgs.Frame;
        }

        private async Task delayedWork(Bitmap map)
        {
            await Task.Delay(5000);

            map.Dispose();
        }

        //This could be a button click event handler or the like */
        private void StartAsyncTimedWork(Bitmap map)
        {
            Task ignoredAwaitableResult = this.delayedWork(map);
        }
    }
}
