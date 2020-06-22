namespace UI
{
    partial class ManualInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualInput));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textCode = new System.Windows.Forms.TextBox();
            this.textPhone = new System.Windows.Forms.TextBox();
            this.textIDno = new System.Windows.Forms.TextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnTwoText = new System.Windows.Forms.Button();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1203, 729);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(297, 207);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(313, 216);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.textCode);
            this.panel1.Controls.Add(this.textPhone);
            this.panel1.Controls.Add(this.textIDno);
            this.panel1.Controls.Add(this.textName);
            this.panel1.Location = new System.Drawing.Point(616, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 186);
            this.panel1.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(192, 150);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "获取验证码";
            this.linkLabel1.Click += new System.EventHandler(this.linkLabel1_Click);
            // 
            // textCode
            // 
            this.textCode.Font = new System.Drawing.Font("宋体", 12F);
            this.textCode.Location = new System.Drawing.Point(88, 141);
            this.textCode.Multiline = true;
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(85, 25);
            this.textCode.TabIndex = 3;
            // 
            // textPhone
            // 
            this.textPhone.Font = new System.Drawing.Font("宋体", 12F);
            this.textPhone.Location = new System.Drawing.Point(88, 96);
            this.textPhone.Multiline = true;
            this.textPhone.Name = "textPhone";
            this.textPhone.Size = new System.Drawing.Size(185, 25);
            this.textPhone.TabIndex = 2;
            // 
            // textIDno
            // 
            this.textIDno.Font = new System.Drawing.Font("宋体", 12F);
            this.textIDno.Location = new System.Drawing.Point(88, 49);
            this.textIDno.Multiline = true;
            this.textIDno.Name = "textIDno";
            this.textIDno.Size = new System.Drawing.Size(185, 25);
            this.textIDno.TabIndex = 1;
            // 
            // textName
            // 
            this.textName.Enabled = false;
            this.textName.Font = new System.Drawing.Font("宋体", 12F);
            this.textName.Location = new System.Drawing.Point(88, 5);
            this.textName.Multiline = true;
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(185, 25);
            this.textName.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(548, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 50);
            this.button1.TabIndex = 10;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // btnTwoText
            // 
            this.btnTwoText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTwoText.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTwoText.BackgroundImage")));
            this.btnTwoText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTwoText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwoText.ForeColor = System.Drawing.Color.Transparent;
            this.btnTwoText.Location = new System.Drawing.Point(548, 474);
            this.btnTwoText.Name = "btnTwoText";
            this.btnTwoText.Size = new System.Drawing.Size(150, 50);
            this.btnTwoText.TabIndex = 11;
            this.btnTwoText.UseVisualStyleBackColor = false;
            this.btnTwoText.Click += new System.EventHandler(this.btnTwoText_Click);
            // 
            // skinEngine1
            // 
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            // 
            // ManualInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 729);
            this.Controls.Add(this.btnTwoText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ManualInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人工验证";
            this.Load += new System.EventHandler(this.ManualInput_Load);
            this.Shown += new System.EventHandler(this.ManualInput_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.TextBox textPhone;
        private System.Windows.Forms.TextBox textIDno;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnTwoText;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
    }
}