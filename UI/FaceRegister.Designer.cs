namespace UI
{
    partial class FaceRegister
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceRegister));
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIDCard = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtYan = new System.Windows.Forms.TextBox();
            this.videPlayer = new AForge.Controls.VideoSourcePlayer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Paneljps = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.picText = new System.Windows.Forms.PictureBox();
            this.btnAgree = new System.Windows.Forms.Button();
            this.picIsShow = new System.Windows.Forms.PictureBox();
            this.picRegister = new System.Windows.Forms.PictureBox();
            this.link2 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnNoAgree = new System.Windows.Forms.Button();
            this.link3 = new System.Windows.Forms.LinkLabel();
            this.Paneljps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIsShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRegister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.link2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtName.Location = new System.Drawing.Point(51, 267);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(840, 68);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "请输入姓名";
            this.txtName.Click += new System.EventHandler(this.txtName_Click);
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtIDCard
            // 
            this.txtIDCard.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIDCard.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtIDCard.Location = new System.Drawing.Point(51, 408);
            this.txtIDCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIDCard.Multiline = true;
            this.txtIDCard.Name = "txtIDCard";
            this.txtIDCard.Size = new System.Drawing.Size(840, 68);
            this.txtIDCard.TabIndex = 1;
            this.txtIDCard.Text = "请输入身份证号";
            this.txtIDCard.Click += new System.EventHandler(this.txtIDCard_Click);
            this.txtIDCard.Enter += new System.EventHandler(this.txtIDCard_Enter);
            this.txtIDCard.Leave += new System.EventHandler(this.txtIDCard_Leave);
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAddress.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtAddress.Location = new System.Drawing.Point(51, 542);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(840, 68);
            this.txtAddress.TabIndex = 2;
            this.txtAddress.Text = "请输入地址";
            this.txtAddress.Click += new System.EventHandler(this.txtAddress_Click);
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPhone.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtPhone.Location = new System.Drawing.Point(51, 668);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPhone.Multiline = true;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(840, 68);
            this.txtPhone.TabIndex = 3;
            this.txtPhone.Text = "请输入手机号";
            this.txtPhone.Click += new System.EventHandler(this.txtPhone_Click);
            this.txtPhone.Enter += new System.EventHandler(this.txtPhone_Enter);
            this.txtPhone.Leave += new System.EventHandler(this.txtPhone_Leave);
            // 
            // txtYan
            // 
            this.txtYan.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtYan.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.txtYan.Location = new System.Drawing.Point(51, 806);
            this.txtYan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtYan.Multiline = true;
            this.txtYan.Name = "txtYan";
            this.txtYan.Size = new System.Drawing.Size(840, 72);
            this.txtYan.TabIndex = 4;
            this.txtYan.Text = "请输入验证码";
            this.txtYan.Click += new System.EventHandler(this.txtYan_Click);
            this.txtYan.Enter += new System.EventHandler(this.txtYan_Enter);
            this.txtYan.Leave += new System.EventHandler(this.txtYan_Leave);
            // 
            // videPlayer
            // 
            this.videPlayer.Location = new System.Drawing.Point(134, 992);
            this.videPlayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.videPlayer.Name = "videPlayer";
            this.videPlayer.Size = new System.Drawing.Size(890, 531);
            this.videPlayer.TabIndex = 15;
            this.videPlayer.VideoSource = null;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Paneljps
            // 
            this.Paneljps.Controls.Add(this.button5);
            this.Paneljps.Controls.Add(this.btnOk);
            this.Paneljps.Controls.Add(this.button1);
            this.Paneljps.Controls.Add(this.button11);
            this.Paneljps.Controls.Add(this.button2);
            this.Paneljps.Controls.Add(this.btndelete);
            this.Paneljps.Controls.Add(this.button3);
            this.Paneljps.Controls.Add(this.button7);
            this.Paneljps.Controls.Add(this.button6);
            this.Paneljps.Controls.Add(this.button8);
            this.Paneljps.Controls.Add(this.button4);
            this.Paneljps.Controls.Add(this.button9);
            this.Paneljps.Location = new System.Drawing.Point(72, 992);
            this.Paneljps.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Paneljps.Name = "Paneljps";
            this.Paneljps.Size = new System.Drawing.Size(1046, 651);
            this.Paneljps.TabIndex = 42;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.Transparent;
            this.button5.Location = new System.Drawing.Point(398, 196);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(225, 135);
            this.button5.TabIndex = 33;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOk.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOk.BackgroundImage")));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.Transparent;
            this.btnOk.Location = new System.Drawing.Point(772, 510);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(225, 135);
            this.btnOk.TabIndex = 41;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(52, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 135);
            this.button1.TabIndex = 29;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button11.BackgroundImage")));
            this.button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.ForeColor = System.Drawing.Color.Transparent;
            this.button11.Location = new System.Drawing.Point(398, 510);
            this.button11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(225, 135);
            this.button11.TabIndex = 38;
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(52, 196);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(225, 135);
            this.button2.TabIndex = 32;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btndelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btndelete.BackgroundImage")));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.ForeColor = System.Drawing.Color.Transparent;
            this.btndelete.Location = new System.Drawing.Point(52, 510);
            this.btndelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(225, 135);
            this.btndelete.TabIndex = 39;
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(52, 345);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(225, 135);
            this.button3.TabIndex = 35;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.Transparent;
            this.button7.Location = new System.Drawing.Point(772, 362);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(225, 135);
            this.button7.TabIndex = 37;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.Transparent;
            this.button6.Location = new System.Drawing.Point(398, 28);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(225, 135);
            this.button6.TabIndex = 30;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.Transparent;
            this.button8.Location = new System.Drawing.Point(772, 196);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(225, 135);
            this.button8.TabIndex = 34;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(398, 362);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(225, 135);
            this.button4.TabIndex = 36;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button9.BackgroundImage")));
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.Transparent;
            this.button9.Location = new System.Drawing.Point(772, 28);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(225, 135);
            this.button9.TabIndex = 31;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // timer4
            // 
            this.timer4.Interval = 3000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // picText
            // 
            this.picText.BackgroundImage = global::UI.Properties.Resources.服务协议;
            this.picText.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picText.Location = new System.Drawing.Point(165, 1594);
            this.picText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picText.Name = "picText";
            this.picText.Size = new System.Drawing.Size(726, 84);
            this.picText.TabIndex = 45;
            this.picText.TabStop = false;
            this.picText.Click += new System.EventHandler(this.picText_Click);
            // 
            // btnAgree
            // 
            this.btnAgree.AllowDrop = true;
            this.btnAgree.BackgroundImage = global::UI.Properties.Resources.icon_checked_mark;
            this.btnAgree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgree.Location = new System.Drawing.Point(51, 1594);
            this.btnAgree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAgree.Name = "btnAgree";
            this.btnAgree.Size = new System.Drawing.Size(88, 92);
            this.btnAgree.TabIndex = 43;
            this.btnAgree.UseVisualStyleBackColor = true;
            this.btnAgree.Click += new System.EventHandler(this.btnAgree_Click);
            // 
            // picIsShow
            // 
            this.picIsShow.BackgroundImage = global::UI.Properties.Resources.添加人像;
            this.picIsShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIsShow.Location = new System.Drawing.Point(134, 992);
            this.picIsShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picIsShow.Name = "picIsShow";
            this.picIsShow.Size = new System.Drawing.Size(890, 531);
            this.picIsShow.TabIndex = 16;
            this.picIsShow.TabStop = false;
            this.picIsShow.Click += new System.EventHandler(this.picIsShow_Click);
            // 
            // picRegister
            // 
            this.picRegister.BackgroundImage = global::UI.Properties.Resources.注册icon;
            this.picRegister.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picRegister.Location = new System.Drawing.Point(51, 1713);
            this.picRegister.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picRegister.Name = "picRegister";
            this.picRegister.Size = new System.Drawing.Size(1078, 164);
            this.picRegister.TabIndex = 10;
            this.picRegister.TabStop = false;
            this.picRegister.Click += new System.EventHandler(this.picRegister_Click);
            // 
            // link2
            // 
            this.link2.BackgroundImage = global::UI.Properties.Resources.验证码;
            this.link2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.link2.Location = new System.Drawing.Point(936, 806);
            this.link2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.link2.Name = "link2";
            this.link2.Size = new System.Drawing.Size(210, 56);
            this.link2.TabIndex = 5;
            this.link2.TabStop = false;
            this.link2.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::UI.Properties.Resources._9_1注册;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(18, 18);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1158, 1954);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnNoAgree
            // 
            this.btnNoAgree.AllowDrop = true;
            this.btnNoAgree.BackgroundImage = global::UI.Properties.Resources.icon_Unchecked;
            this.btnNoAgree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNoAgree.Location = new System.Drawing.Point(51, 1594);
            this.btnNoAgree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnNoAgree.Name = "btnNoAgree";
            this.btnNoAgree.Size = new System.Drawing.Size(88, 92);
            this.btnNoAgree.TabIndex = 44;
            this.btnNoAgree.UseVisualStyleBackColor = true;
            this.btnNoAgree.Click += new System.EventHandler(this.btnNoAgree_Click);
            // 
            // link3
            // 
            this.link3.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.link3.Location = new System.Drawing.Point(928, 806);
            this.link3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.link3.Name = "link3";
            this.link3.Size = new System.Drawing.Size(230, 56);
            this.link3.TabIndex = 46;
            this.link3.TabStop = true;
            this.link3.Text = "获取验证码";
            this.link3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link3_LinkClicked);
            // 
            // FaceRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1312, 898);
            this.Controls.Add(this.link3);
            this.Controls.Add(this.picText);
            this.Controls.Add(this.btnAgree);
            this.Controls.Add(this.Paneljps);
            this.Controls.Add(this.picIsShow);
            this.Controls.Add(this.videPlayer);
            this.Controls.Add(this.picRegister);
            this.Controls.Add(this.link2);
            this.Controls.Add(this.txtYan);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtIDCard);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnNoAgree);
            this.Controls.Add(this.pictureBox2);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FaceRegister";
            this.Text = "FaceRegister";
            this.Load += new System.EventHandler(this.FaceRegister_Load);
            this.Shown += new System.EventHandler(this.FaceRegister_Shown);
            this.Paneljps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIsShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRegister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.link2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIDCard;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtYan;
        private System.Windows.Forms.PictureBox link2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox picRegister;
        private AForge.Controls.VideoSourcePlayer videPlayer;
        private System.Windows.Forms.PictureBox picIsShow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel Paneljps;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Button btnAgree;
        private System.Windows.Forms.Button btnNoAgree;
        private System.Windows.Forms.PictureBox picText;
        private System.Windows.Forms.LinkLabel link3;
    }
}