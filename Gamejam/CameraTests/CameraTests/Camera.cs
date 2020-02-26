using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using AForge.Video;
using AForge;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Imaging;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CameraTests
{
    class Camera
    {
        public enum CameraStatus
        {
            Inactive, Active, Tracking
        }
        public enum TrackingMode
        {
            red, green, yellow
        }
        public TrackingMode trackingMode;
        public CameraStatus cameraStatus;
        private FilterInfoCollection CaptureDevices;
        private VideoCaptureDevice Webcam;
        public List<FilterInfo> Devices = new List<FilterInfo>();
        private Texture2D frame;
        private GraphicsDevice graphicDevice;
        private ExtractChannel Extractor = new ExtractChannel();
        private ReplaceChannel Replacer;
        private EuclideanColorFiltering ColorFilter = new EuclideanColorFiltering();
        public Vector2 TrackedObject;
        private Color[] TargetColorRange = new Color[2];
        private Color[,] ColorArray;

        public Camera(GraphicsDevice graphics)
        {
            cameraStatus = CameraStatus.Inactive;
            trackingMode = TrackingMode.yellow;
            graphicDevice = graphics;
            CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevices)
            {
                Devices.Add(Device);
            }
            Webcam = new VideoCaptureDevice();
        }
        public void Initialize()
        {
            if (trackingMode == TrackingMode.red)
            {
                TargetColorRange[0] = new Color(200, 0, 0);
                TargetColorRange[1] = new Color(255, 100, 100);
                ColorFilter.CenterColor = new RGB(215, 30, 30);
                ColorFilter.Radius = 100;
            }
            else if(trackingMode == TrackingMode.green)
            {
                TargetColorRange[0] = new Color(0, 200, 0);
                TargetColorRange[1] = new Color(60, 255, 60);
                ColorFilter.CenterColor = new RGB(30, 230, 30);
                ColorFilter.Radius = 100;
            }
            else if(trackingMode == TrackingMode.yellow)
            {
                ColorFilter.CenterColor = new RGB(180, 180, 30);
                ColorFilter.Radius = 80;
;            }
            cameraStatus = CameraStatus.Active;
            if (Devices.Count == 1)
                Webcam = new VideoCaptureDevice(Devices[0].MonikerString);
            Webcam.NewFrame += new NewFrameEventHandler(Webcam_NewFrame);
            Webcam.Start();
        }

        private void Webcam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            ColorFilter.ApplyInPlace(eventArgs.Frame);
            Extractor.Channel = RGB.R;
            System.Drawing.Bitmap bitmapBlue = Extractor.Apply(eventArgs.Frame);
            Extractor.Channel = RGB.B;
            System.Drawing.Bitmap bitmapRed = Extractor.Apply(eventArgs.Frame);
            if (Replacer == null)
                Replacer = new ReplaceChannel(RGB.R, bitmapRed);
            else
            {
                Replacer.Channel = RGB.R;
                Replacer.ChannelImage = bitmapRed;
            }

            Replacer.ApplyInPlace(eventArgs.Frame);
            Replacer.Channel = RGB.B;
            Replacer.ChannelImage = bitmapBlue;
            Replacer.ApplyInPlace(eventArgs.Frame);
            bitmapBlue.Dispose();
            bitmapRed.Dispose();
            eventArgs.Frame.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipY);
            frame = XNATextureFromBitmap(eventArgs.Frame as System.Drawing.Bitmap, graphicDevice);
            eventArgs.Frame.Dispose();


        }
        public void EndVideoFeed()
        {
            if (Webcam.IsRunning)
            {
                Webcam.Stop();
                cameraStatus = CameraStatus.Inactive;
            }

        }
        public void Update(GameTime gameTime)
        {
            if (cameraStatus == CameraStatus.Tracking)
            {
                TrackedObject = TrackObject();
            }

        }
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if ((cameraStatus == CameraStatus.Active || cameraStatus == CameraStatus.Tracking) && frame != null)
                spriteBatch.Draw(frame, Vector2.Zero, Color.White);

        }
        /*
        private Vector2 TrackObject()
        {
            int minX = 0, minY = 0, maxX = 0, maxY = 0;
            bool firstPixel = true;
            Color[,] ColorArray = TextureTo2DArray(frame);
            for(int x = 0; x < frame.Width; x++)
                for(int y = 0; y < frame.Height; y++)
                {
                    if(ColorArray[x, y].R >= TargetColorRange[0].R && ColorArray[x, y].R <= TargetColorRange[1].R && 
                       ColorArray[x, y].B >= TargetColorRange[0].B && ColorArray[x, y].B <= TargetColorRange[1].B && 
                       ColorArray[x, y].G >= TargetColorRange[0].G && ColorArray[x, y].G <= TargetColorRange[1].G)
                    {
                        if (firstPixel)
                        {
                            minX = x; maxX = x; minY = y; maxY = y;
                            firstPixel = false;
                        }
                        else
                        {
                            if (x < minX)
                                minX = x;
                            if (x > maxX)
                                maxX = x;
                            if (y < minY)
                                minY = y;
                            if (y > maxY)
                                maxY = y;
                        }
                    }                                       
                }
            if (firstPixel && TrackedObject != null)
                return TrackedObject;
            Rectangle rectangle = new Rectangle(minX, minY, maxX, maxY);
            return new Vector2(rectangle.Center.X, rectangle.Center.Y);
        }
        */
        int i,minX, minY, maxX, maxY;
        public Rectangle rectangle;
        private Vector2 TrackObject()
        {           
            ColorArray = TextureTo2DArray(frame);
            for (int x = 0; x < frame.Width; x++)
                for (int y = 0; y < frame.Height; y++)
                {
                    if (ColorArray[x, y].R >= 30 &&
                        ColorArray[x, y].B >= 30 &&
                        ColorArray[x, y].G >= 30)
                    {
                        minX = x; maxX = x; minY = y; maxY = y;
                        i = 1;
                        CheckNeighbours(x, y);
                        rectangle = new Rectangle(minX, minY, maxX - minX, maxY - minY);
                        if (i > 3000)
                            return new Vector2(rectangle.Center.X, rectangle.Center.Y);
                    }
                }
            if (TrackedObject != null)
                return TrackedObject;
            return Vector2.Zero;
        }

        private void CheckNeighbours(int x, int y)
        {
            while(i < 4000)
            {
                if (x < 0 || x > frame.Width - 1 || y < 0 || y > frame.Height - 1)
                    return;
                if (ColorArray[x, y].R >= 30 &&
                    ColorArray[x, y].B >= 30 &&
                    ColorArray[x, y].G >= 30)
                {
                    if (x < minX)
                        minX = x;
                    if (x > maxX)
                        maxX = x;
                    if (y < minY)
                        minY = y;
                    if (y > maxY)
                        maxY = y;
                    i++;
                    ColorArray[x, y] = new Color(0, 0, 0);
                    CheckNeighbours(x + 1, y);
                    CheckNeighbours(x, y + 1);
                    CheckNeighbours(x - 1, y);
                    CheckNeighbours(x, y - 1);
                }
                else return;
            }
          
        }
        //Source: http://www.aforgenet.com/articles/gratf_ar/
        public static Texture2D XNATextureFromBitmap(System.Drawing.Bitmap bitmap, GraphicsDevice device)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            Texture2D texture = new Texture2D(device, width, height, false, SurfaceFormat.Color);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            int bufferSize = data.Height * data.Stride;
            // copy bitmap data into texture
            byte[] bytes = new byte[bufferSize];
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            texture.SetData(bytes);
            bitmap.UnlockBits(data);

            bitmap.Dispose();

            return texture;
        }
        //source https://stackoverflow.com/questions/23463008/how-can-i-read-the-color-of-a-specific-pixel-in-xna
        private Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = colors1D[x + y * texture.Width];
                }
            }
            return colors2D;
        }
    }
}
