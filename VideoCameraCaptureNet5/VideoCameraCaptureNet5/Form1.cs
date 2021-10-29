using AForge.Video;
using AForge.Video.DirectShow;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoCameraCaptureNet5
{
    public partial class Form1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo filterInfo in filterInfoCollection)
            {
                cmbCamera.Items.Add(filterInfo.Name);
            }

            if (cmbCamera.Items.Count > 0)
            {
                cmbCamera.SelectedIndex = 0;
            }

            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cmbCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }
        bool running = false;

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (running)
            {
                return;
            }

            running = true;

            try
            {
                Bitmap image = (Bitmap)eventArgs.Frame.Clone();

                image = new Bitmap(ResizeImage(image, (int)numWidth.Value, (int)numHeight.Value));

                if (rdObjectDrawing.Checked)
                {
                    Bitmap result = DrawObjects(image);

                    if (result != null)
                    {
                        Image resized = ResizeImage(result, pbVideo.Width, pbVideo.Height);

                        pbVideo.Image = resized;
                        pbVideo.Width = resized.Width;
                        pbVideo.Height = resized.Height;
                    }
                }
                else if (rdMovementDetector.Checked)
                {
                    Bitmap changedImage = DrawChanges(image);

                    Image resized = ResizeImage(changedImage, pbVideo.Width, pbVideo.Height);

                    pbVideo.Image = resized;
                    pbVideo.Width = resized.Width;
                    pbVideo.Height = resized.Height;

                    currentBitmap = image;
                }
            }
            catch (Exception ex)
            {

            }

            running = false;
        }

        private Bitmap currentBitmap = null;
        private Bitmap DrawChanges(Bitmap inputBitmap)
        {
            if (currentBitmap != null)
            {
                Color markColor = Color.Red;
                //int diffrenceDistance = (int)numDistance.Value;
                //int nearestColorTolerence = (int)numTolerance.Value;

                int taskCount = (int)numThread.Value;

                Task<ImageItem>[] tasks = new Task<ImageItem>[taskCount * taskCount];
                int widthParts = inputBitmap.Width / taskCount;
                int heightParts = inputBitmap.Height / taskCount;

                int topIndex = 0;
                int taskIndex = 0;

                //Bitmap outBitmap = inputBitmap.Clone() as Bitmap;

                for (int i = 0; i < taskCount; i++)
                {
                    int leftIndex = 0;

                    for (int j = 0; j < taskCount; j++)
                    {

                        Task<Bitmap> inputCroppedTask = Task.Run(delegate
                         {
                             Bitmap inputCropped = new Bitmap(widthParts, heightParts);
                             using (Graphics graphics = Graphics.FromImage(inputCropped))
                             {
                                 graphics.DrawImage(
                                     inputBitmap,
                                     new Rectangle(0, 0, widthParts, heightParts),
                                     new Rectangle(leftIndex, topIndex, widthParts, heightParts),
                                     GraphicsUnit.Pixel);
                             }
                             return inputCropped;
                         });

                        Task<Bitmap> diffCroppedTask = Task.Run(delegate
                        {
                            Bitmap diffCropped = new Bitmap(widthParts, heightParts);
                            using (Graphics graphics = Graphics.FromImage(diffCropped))
                            {
                                graphics.DrawImage(
                                    currentBitmap,
                                    new Rectangle(0, 0, widthParts, heightParts),
                                    new Rectangle(leftIndex, topIndex, widthParts, heightParts),
                                    GraphicsUnit.Pixel);
                            }
                            return diffCropped;
                        });

                        Task.WaitAll(inputCroppedTask, diffCroppedTask);

                        leftIndex += widthParts;

                        tasks[taskIndex] = DrawChange(taskIndex, inputCroppedTask.Result, diffCroppedTask.Result, markColor);

                        taskIndex++;
                    }

                    topIndex += heightParts;
                }

                Task.WaitAll(tasks);

                return MergeImages(inputBitmap.Width, inputBitmap.Height, tasks);
            }
            else
                return inputBitmap;
        }

        private Task<ImageItem> DrawChange(int index, Bitmap image1, Bitmap image2, Color markColor)
        {
            return Task.Run(() =>
            {
                Bitmap outBitmap = new Bitmap(image1.Width, image2.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color currentColor = image1.GetPixel(x, y);
                        Color otherColor = image2.GetPixel(x, y);

                        if (!currentColor.IsEmpty)
                        {
                            if (!IsNearOrSameColor(currentColor, otherColor, (int)numTolerance.Value))
                            {
                                outBitmap.SetPixel(x, y, markColor);
                            }
                            else
                            {
                                outBitmap.SetPixel(x, y, currentColor);
                            }
                        }
                    }
                }

                return new ImageItem() { Bitmap = outBitmap, Index = index };
            });
        }

        private Bitmap DrawObjects(Bitmap image)
        {
            if (image != null)
            {
                Color markColor = Color.Red;
                int diffrenceDistance = (int)numDistance.Value;
                int nearestColorTolerence = (int)numTolerance.Value;

                int taskCount = (int)numThread.Value;

                Task<ImageItem>[] tasks = new Task<ImageItem>[taskCount * taskCount];
                int widthParts = image.Width / taskCount;
                int heightParts = image.Height / taskCount;

                int topIndex = 0;
                int taskIndex = 0;

                if (taskCount > 1)
                {
                    for (int i = 0; i < taskCount; i++)
                    {
                        int leftIndex = 0;

                        for (int j = 0; j < taskCount; j++)
                        {
                            Bitmap cropped = new Bitmap(widthParts, heightParts);

                            using (Graphics graphics = Graphics.FromImage(cropped))
                            {
                                graphics.DrawImage(
                                    image,
                                    new Rectangle(0, 0, widthParts, heightParts),
                                    new Rectangle(leftIndex, topIndex, widthParts, heightParts),
                                    GraphicsUnit.Pixel);
                            }

                            leftIndex += widthParts;

                            tasks[taskIndex] = DrawObject(taskIndex, cropped, markColor, diffrenceDistance, nearestColorTolerence);

                            taskIndex++;
                        }

                        topIndex += heightParts;
                    }

                    Task.WaitAll(tasks);

                    return MergeImages(image.Width, image.Height, tasks);
                }
                else
                {
                    Task<ImageItem> task = DrawObject(0, image, markColor, diffrenceDistance, nearestColorTolerence);

                    task.Wait();

                    return task.Result.Bitmap;
                }
            }

            return null;
        }

        private Bitmap MergeImages(int width, int height, Task<ImageItem>[] tasks)
        {
            Bitmap outputBitmap = new Bitmap(width, height);

            int xPosition = 0;
            int yPosition = 0;

            foreach (var task in tasks.OrderBy(x => x.Result.Index).ToList())
            {
                Bitmap bitmap = task.Result.Bitmap;

                using (Graphics graphics = Graphics.FromImage(outputBitmap))
                {
                    graphics.DrawImage(
                        bitmap,
                        new Rectangle(xPosition, yPosition, bitmap.Width, bitmap.Height),
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        GraphicsUnit.Pixel);
                }

                xPosition += bitmap.Width;

                if (xPosition >= width)
                {
                    xPosition = 0;
                    yPosition += bitmap.Height;
                }
            }

            return outputBitmap;
        }

        private Task<ImageItem> DrawObject(int index, Bitmap inputBitmap, Color markColor, int diffrenceDistance, int nearestColorTolerence)
        {
            #region unsafe
            //return Task.Run(() =>
            //{
            //    unsafe
            //    {
            //        Bitmap outBitmap = inputBitmap.Clone() as Bitmap;

            //        BitmapData outputBitmapData = outBitmap.LockBits(new Rectangle(0, 0, outBitmap.Width, outBitmap.Height), ImageLockMode.ReadWrite, outBitmap.PixelFormat);

            //        BitmapData inputBitmapData = inputBitmap.LockBits(new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height), ImageLockMode.ReadOnly, inputBitmap.PixelFormat);

            //        int bytesPerPixel = Image.GetPixelFormatSize(inputBitmap.PixelFormat) / 8;
            //        int heightInPixels = inputBitmapData.Height;
            //        int widthInBytes = inputBitmapData.Width * bytesPerPixel;
            //        byte* PtrFirstPixel = (byte*)inputBitmapData.Scan0;

            //        for (int y = 0; y < heightInPixels; y++)
            //        {
            //            byte* currentLine = PtrFirstPixel + (y * inputBitmapData.Stride);
            //            for (int x = 0; x < widthInBytes; x += bytesPerPixel)
            //            {
            //                int blue = currentLine[x];
            //                int green = currentLine[x + 1];
            //                int red = currentLine[x + 2];
            //                int alpha = currentLine[x + 3];

            //                Color currentColor = Color.FromArgb(alpha, red, green, blue);

            //                if (!currentColor.IsEmpty)
            //                {
            //                    byte* beforeLine = PtrFirstPixel + (y * (inputBitmapData.Stride)) - 2;
            //                    byte* nextLine = PtrFirstPixel + (y * (inputBitmapData.Stride)) + 2;

            //                    Color topColor = Color.FromArgb(beforeLine[x + 3], beforeLine[x + 1], beforeLine[x + 1], beforeLine[x + 2]);
            //                    Color leftColor = Color.FromArgb(currentLine[x + 3], currentLine[x + 1], currentLine[x], currentLine[x - 1]);
            //                    Color bottomColor = Color.FromArgb(nextLine[x + 3], nextLine[x + 1], nextLine[x + 1], nextLine[x + 2]);
            //                    Color rightColor = Color.FromArgb(currentLine[x + 3], currentLine[x + 3], currentLine[x + 2], currentLine[x + 1]);

            //                    byte* outputPtrFirstPixel = (byte*)outputBitmapData.Scan0;
            //                    byte* outputCurrentLine = outputPtrFirstPixel + (y * outputBitmapData.Stride);

            //                    if (!IsNearOrSameColor(leftColor, currentColor, nearestColorTolerence)
            //                        ||
            //                        !IsNearOrSameColor(rightColor, currentColor, nearestColorTolerence)
            //                        ||
            //                        !IsNearOrSameColor(topColor, currentColor, nearestColorTolerence)
            //                        ||
            //                        !IsNearOrSameColor(bottomColor, currentColor, nearestColorTolerence))
            //                    {
            //                        outputCurrentLine[x] = markColor.B;
            //                        outputCurrentLine[x + 1] = markColor.G;
            //                        outputCurrentLine[x + 2] = markColor.R;
            //                        outputCurrentLine[x + 3] = markColor.A;
            //                    }
            //                    else
            //                    {
            //                        outputCurrentLine[x] = (byte)blue;
            //                        outputCurrentLine[x + 1] = (byte)green;
            //                        outputCurrentLine[x + 2] = (byte)red;
            //                        outputCurrentLine[x + 3] = (byte)alpha;
            //                    }
            //                }
            //            }
            //        }

            //        inputBitmap.UnlockBits(inputBitmapData);

            //        outBitmap.UnlockBits(outputBitmapData);

            //        return new ImageItem() { Bitmap = outBitmap, Index = index };
            //    }
            //});
            #endregion

            return Task.Run(() =>
            {
                Bitmap outBitmap = inputBitmap.Clone() as Bitmap;

                for (int x = 0; x < inputBitmap.Width; x++)
                {
                    for (int y = 0; y < inputBitmap.Height; y++)
                    {
                        Color currentColor = inputBitmap.GetPixel(x, y);

                        if (!currentColor.IsEmpty)
                        {
                            if (x > diffrenceDistance && y > diffrenceDistance && x < inputBitmap.Width - diffrenceDistance && y < inputBitmap.Height - diffrenceDistance)
                            {
                                Color leftColor = inputBitmap.GetPixel(x - diffrenceDistance, y);
                                Color topColor = inputBitmap.GetPixel(x, y + diffrenceDistance);
                                Color rightColor = inputBitmap.GetPixel(x + diffrenceDistance, y);
                                Color bottomColor = inputBitmap.GetPixel(x, y - diffrenceDistance);

                                if (!IsNearOrSameColor(leftColor, currentColor, nearestColorTolerence)
                                    ||
                                    !IsNearOrSameColor(rightColor, currentColor, nearestColorTolerence)
                                    ||
                                    !IsNearOrSameColor(topColor, currentColor, nearestColorTolerence)
                                    ||
                                    !IsNearOrSameColor(bottomColor, currentColor, nearestColorTolerence))
                                {
                                    outBitmap.SetPixel(x, y, markColor);
                                }
                                else
                                {
                                    outBitmap.SetPixel(x, y, currentColor);
                                }
                            }
                            else
                            {
                                outBitmap.SetPixel(x, y, currentColor);
                            }
                        }
                    }
                }

                return new ImageItem() { Bitmap = outBitmap, Index = index };
            });
        }

        public static Image ResizeImage(Image originalImage, int w, int h)
        {
            //Original Image attributes
            int originalWidth = originalImage.Width;
            int originalHeight = originalImage.Height;

            // Figure out the ratio
            double ratioX = (double)w / (double)originalWidth;
            double ratioY = (double)h / (double)originalHeight;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            Image thumbnail = new Bitmap(newWidth, newHeight);
            Graphics graphic = Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            graphic.Clear(Color.Transparent);
            graphic.DrawImage(originalImage, 0, 0, newWidth, newHeight);

            return thumbnail;
        }

        private bool IsNearOrSameColor(Color color, Color otherColor, int tolerence)
        {
            return
                Math.Abs((otherColor.A + otherColor.R + otherColor.G + otherColor.B) - (color.A + color.R + color.G + color.B)) <= tolerence;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void rdObjectDrawing_CheckedChanged(object sender, EventArgs e)
        {
            rdMovementDetector.Checked = !rdObjectDrawing.Checked;
        }

        private void rdMovementDetector_CheckedChanged(object sender, EventArgs e)
        {
            rdObjectDrawing.Checked = !rdMovementDetector.Checked;
        }
    }

    public class ImageItem
    {
        public int Index { get; set; }
        public Bitmap Bitmap { get; set; }
    }
}