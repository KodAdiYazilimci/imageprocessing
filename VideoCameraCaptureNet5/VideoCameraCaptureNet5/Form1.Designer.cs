
namespace VideoCameraCaptureNet5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numThread = new System.Windows.Forms.NumericUpDown();
            this.numDistance = new System.Windows.Forms.NumericUpDown();
            this.numTolerance = new System.Windows.Forms.NumericUpDown();
            this.pbVideo = new System.Windows.Forms.PictureBox();
            this.rdObjectDrawing = new System.Windows.Forms.RadioButton();
            this.rdMovementDetector = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera";
            // 
            // cmbCamera
            // 
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Location = new System.Drawing.Point(67, 10);
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.Size = new System.Drawing.Size(236, 23);
            this.cmbCamera.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(309, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Resolution:";
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(89, 49);
            this.numWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(79, 23);
            this.numWidth.TabIndex = 4;
            this.numWidth.Value = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            // 
            // numHeight
            // 
            this.numHeight.Location = new System.Drawing.Point(193, 49);
            this.numHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(79, 23);
            this.numHeight.TabIndex = 5;
            this.numHeight.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "x";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Thread Count:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(404, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Pixel Distance:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(404, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Color Tolerance:";
            // 
            // numThread
            // 
            this.numThread.Location = new System.Drawing.Point(512, 1);
            this.numThread.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numThread.Name = "numThread";
            this.numThread.Size = new System.Drawing.Size(79, 23);
            this.numThread.TabIndex = 10;
            this.numThread.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // numDistance
            // 
            this.numDistance.Location = new System.Drawing.Point(512, 28);
            this.numDistance.Name = "numDistance";
            this.numDistance.Size = new System.Drawing.Size(79, 23);
            this.numDistance.TabIndex = 11;
            this.numDistance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numTolerance
            // 
            this.numTolerance.Location = new System.Drawing.Point(512, 55);
            this.numTolerance.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numTolerance.Name = "numTolerance";
            this.numTolerance.Size = new System.Drawing.Size(79, 23);
            this.numTolerance.TabIndex = 12;
            this.numTolerance.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // pbVideo
            // 
            this.pbVideo.Location = new System.Drawing.Point(12, 89);
            this.pbVideo.Name = "pbVideo";
            this.pbVideo.Size = new System.Drawing.Size(1024, 768);
            this.pbVideo.TabIndex = 13;
            this.pbVideo.TabStop = false;
            // 
            // rdObjectDrawing
            // 
            this.rdObjectDrawing.AutoSize = true;
            this.rdObjectDrawing.Checked = true;
            this.rdObjectDrawing.Location = new System.Drawing.Point(672, 13);
            this.rdObjectDrawing.Name = "rdObjectDrawing";
            this.rdObjectDrawing.Size = new System.Drawing.Size(107, 19);
            this.rdObjectDrawing.TabIndex = 14;
            this.rdObjectDrawing.TabStop = true;
            this.rdObjectDrawing.Text = "Object Drawing";
            this.rdObjectDrawing.UseVisualStyleBackColor = true;
            this.rdObjectDrawing.CheckedChanged += new System.EventHandler(this.rdObjectDrawing_CheckedChanged);
            // 
            // rdMovementDetector
            // 
            this.rdMovementDetector.AutoSize = true;
            this.rdMovementDetector.Location = new System.Drawing.Point(672, 38);
            this.rdMovementDetector.Name = "rdMovementDetector";
            this.rdMovementDetector.Size = new System.Drawing.Size(131, 19);
            this.rdMovementDetector.TabIndex = 15;
            this.rdMovementDetector.Text = "Movement Detector";
            this.rdMovementDetector.UseVisualStyleBackColor = true;
            this.rdMovementDetector.CheckedChanged += new System.EventHandler(this.rdMovementDetector_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 866);
            this.Controls.Add(this.rdMovementDetector);
            this.Controls.Add(this.rdObjectDrawing);
            this.Controls.Add(this.pbVideo);
            this.Controls.Add(this.numTolerance);
            this.Controls.Add(this.numDistance);
            this.Controls.Add(this.numThread);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numHeight);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cmbCamera);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTolerance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCamera;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numThread;
        private System.Windows.Forms.NumericUpDown numDistance;
        private System.Windows.Forms.NumericUpDown numTolerance;
        private System.Windows.Forms.PictureBox pbVideo;
        private System.Windows.Forms.RadioButton rdObjectDrawing;
        private System.Windows.Forms.RadioButton rdMovementDetector;
    }
}

