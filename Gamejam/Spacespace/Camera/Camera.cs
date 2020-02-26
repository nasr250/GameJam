using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class Camera
{
    public enum CameraStatus
    {
        Inactive, Active, Tracking
    }
    public enum TrackingMode
    {
        red, green, yellow, white, blue
    }
    int width, height;
    private FilterInfoCollection CaptureDevices;
    private VideoCaptureDevice Webcam;
    public Texture2D frame { get; private set; }
    private static GraphicsDevice graphicsDevice;
    private ExtractChannel Extractor = new ExtractChannel();
    private ReplaceChannel Replacer;
    private EuclideanColorFiltering ColorFilter = new EuclideanColorFiltering();
    private BlobCounterBase BlobFilter = new BlobCounter();

    //Properties
    public Vector2 Position { get; set; }
    public List<FilterInfo> Devices { get; private set; }
    public Rectangle BlobRectangle { get; private set; }
    public CameraStatus cameraStatus { get; set; }
    public RGB FilterColor
    {
        get { return ColorFilter.CenterColor; }
    }
    public short FilterSensitivity
    {
        get { return ColorFilter.Radius; }
    }

    //Constructor needs a grahpicsdevice otherwise it can't make an XNA Texture2D object
    public Camera(GraphicsDevice graphics)
    {
        cameraStatus = CameraStatus.Inactive;
        graphicsDevice = graphics;
        SwitchTrackingMode(TrackingMode.red);
        CaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        Devices = new List<FilterInfo>();
        foreach (FilterInfo Device in CaptureDevices)
        {
            Devices.Add(Device);
        }
        Webcam = new VideoCaptureDevice();

        //Sets the properties of the blobfilter used for object tracking
        BlobFilter.FilterBlobs = true;
        BlobFilter.MinWidth = 15;
        BlobFilter.MinHeight = 15;
        BlobFilter.ObjectsOrder = ObjectsOrder.Size;

        if (Devices.Count >= 1)
            Webcam = new VideoCaptureDevice(Devices[0].MonikerString);

    }
    //Initializes values depending on Trackingmode and starts the video feed
    public void Initialize()
    {              
        //Starts the camera videofeed
        cameraStatus = CameraStatus.Active;

        Webcam.NewFrame += new NewFrameEventHandler(Webcam_NewFrame);
        Webcam.Start();

        while (!Webcam.IsRunning)
            Console.WriteLine("Waiting for camera startup");
    }

    //Method to easily switch the tracked color
    public void SwitchTrackingMode(TrackingMode trackingMode)
    {
        //Sets Colorfilter depending on the trackingmode
        if (trackingMode == TrackingMode.red)
        {
            ColorFilter.CenterColor = new RGB(180, 0, 40);
            ColorFilter.Radius = 80;
        }
        else if (trackingMode == TrackingMode.green)
        {
            ColorFilter.CenterColor = new RGB(30, 102, 100);
            ColorFilter.Radius = 100;
        }
        else if (trackingMode == TrackingMode.blue)
        {
            ColorFilter.CenterColor = new RGB(20, 45, 150);
            ColorFilter.Radius = 65;
        }
        else if (trackingMode == TrackingMode.yellow)
        {
            ColorFilter.CenterColor = new RGB(180, 180, 30);
            ColorFilter.Radius = 80;
            ;
        }
        else if (trackingMode == TrackingMode.white)
        {
            ColorFilter.CenterColor = new RGB(255, 255, 255);
            ColorFilter.Radius = 40;
        }

    }

    //Method that will swap webcam used
    public void SwitchWebcam(int WebcamIndex)
    {
        EndVideoFeed();
        Webcam = new VideoCaptureDevice(Devices[WebcamIndex].MonikerString);
        Initialize();
    }

    //Methods that will modify the value of the color filter
    public void ChangefilterRed(int NewValue)
    {
        ColorFilter.CenterColor.Red = (byte)NewValue;
    }
    public void ChangefilterBlue(int NewValue)
    {
        ColorFilter.CenterColor.Blue = (byte)NewValue;
    }
    public void ChangefilterGreen(int NewValue)
    {
        ColorFilter.CenterColor.Green = (byte)NewValue;
    }
    public void ChangefilterSensitivity(int NewValue)
    {
        ColorFilter.Radius = (short)NewValue;
    }

    //Method that will switch the camera mode
    public void SwitchCameraMode(CameraStatus cameraStatus)
    {
        this.cameraStatus = cameraStatus;
    }



    //AForge event that handles the camera video feed
    private void Webcam_NewFrame(object sender, NewFrameEventArgs eventArgs)
    {
        System.Drawing.Bitmap bitmapBlue, bitmapRed;
        //eventArgs.Frame.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipY);

        //Filters the color of the video feed if it is tracking and checks for blobs on the screen
        if (cameraStatus == CameraStatus.Tracking)
        {
            ColorFilter.ApplyInPlace(eventArgs.Frame);
            BlobFilter.ProcessImage(eventArgs.Frame);
            Blob[] blobs = BlobFilter.GetObjectsInformation();
            if (blobs.Length > 0)
            {
                BlobRectangle = new Rectangle(blobs[0].Rectangle.X, blobs[0].Rectangle.Y, blobs[0].Rectangle.Width, blobs[0].Rectangle.Height);
            }
        }

        //Swaps the R & B channels so it can be displayed correctly on screen
        Extractor.Channel = RGB.R;
        bitmapBlue = Extractor.Apply(eventArgs.Frame);
        Extractor.Channel = RGB.B;
        bitmapRed = Extractor.Apply(eventArgs.Frame);
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
        width = eventArgs.Frame.Width;
        height = eventArgs.Frame.Height;
        //Turns the bitmap of the frame into a Texture2D object
        frame = XNATextureFromBitmap(eventArgs.Frame as System.Drawing.Bitmap);


        eventArgs.Frame.Dispose();

    }

    //Method to be called to get the position of a tracked option
    public Vector2 TrackObject(int screenwidth, int screenheight)
    {
        if (BlobRectangle == null)
            return Vector2.Zero;
        Vector2 pos = new Vector2(BlobRectangle.Center.X, BlobRectangle.Center.Y);
        Vector2 screen = new Vector2(screenwidth, screenheight);
        Vector2 res = new Vector2(width, height);

        pos /= res;
        pos *= screen;

        return pos;
    }

    //Basic method to end the video feed
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
    }

    //Draws the camera when it is active to see what is going on
    public void Draw(GameTime gametime, SpriteBatch spriteBatch)
    {
        if ((cameraStatus == CameraStatus.Active || cameraStatus == CameraStatus.Tracking) && frame != null)
            spriteBatch.Draw(frame, Position, Color.White);

    }


    //Source: http://www.aforgenet.com/articles/gratf_ar/
    public static Texture2D XNATextureFromBitmap(System.Drawing.Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        Texture2D texture = new Texture2D(graphicsDevice, width, height, false, SurfaceFormat.Color);

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
}
