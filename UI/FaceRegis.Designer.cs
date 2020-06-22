namespace UI
{
    partial class FaceRegis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaceRegis));
            this.videPlayer = new AForge.Controls.VideoSourcePlayer();
            this.textCode = new System.Windows.Forms.TextBox();
            this.txtOnePhone = new System.Windows.Forms.TextBox();
            this.txtOneIDNum = new System.Windows.Forms.TextBox();
            this.txtOneName = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbltime = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lbltime1 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.Paneljp = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnook = new System.Windows.Forms.Button();
            this.button0 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.wsFaces1 = new UI.Test.WSFaces();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.Paneljp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // videPlayer
            // 
            this.videPlayer.Location = new System.Drawing.Point(102, 132);
            this.videPlayer.Name = "videPlayer";
            this.videPlayer.Size = new System.Drawing.Size(455, 317);
            this.videPlayer.TabIndex = 14;
            this.videPlayer.VideoSource = null;
            // 
            // textCode
            // 
            this.textCode.Font = new System.Drawing.Font("宋体", 15F);
            this.textCode.Location = new System.Drawing.Point(788, 384);
            this.textCode.Name = "textCode";
            this.textCode.Size = new System.Drawing.Size(245, 30);
            this.textCode.TabIndex = 10;
            this.textCode.Click += new System.EventHandler(this.textCode_Click);
            this.textCode.Enter += new System.EventHandler(this.textCode_Enter);
            // 
            // txtOnePhone
            // 
            this.txtOnePhone.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOnePhone.Location = new System.Drawing.Point(788, 336);
            this.txtOnePhone.Name = "txtOnePhone";
            this.txtOnePhone.Size = new System.Drawing.Size(245, 30);
            this.txtOnePhone.TabIndex = 1;
            this.txtOnePhone.Click += new System.EventHandler(this.txtOnePhone_Click);
            this.txtOnePhone.Enter += new System.EventHandler(this.txtOnePhone_Enter);
            // 
            // txtOneIDNum
            // 
            this.txtOneIDNum.BackColor = System.Drawing.SystemColors.Control;
            this.txtOneIDNum.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOneIDNum.ForeColor = System.Drawing.Color.Red;
            this.txtOneIDNum.Location = new System.Drawing.Point(788, 285);
            this.txtOneIDNum.Name = "txtOneIDNum";
            this.txtOneIDNum.ReadOnly = true;
            this.txtOneIDNum.Size = new System.Drawing.Size(245, 26);
            this.txtOneIDNum.TabIndex = 8;
            this.txtOneIDNum.Text = "请等待片刻读取身份信息";
            this.txtOneIDNum.TextChanged += new System.EventHandler(this.txtOneIDNum_TextChanged);
            // 
            // txtOneName
            // 
            this.txtOneName.BackColor = System.Drawing.SystemColors.Control;
            this.txtOneName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOneName.ForeColor = System.Drawing.Color.Red;
            this.txtOneName.Location = new System.Drawing.Point(788, 240);
            this.txtOneName.Name = "txtOneName";
            this.txtOneName.ReadOnly = true;
            this.txtOneName.Size = new System.Drawing.Size(245, 26);
            this.txtOneName.TabIndex = 7;
            this.txtOneName.Text = "请将您的身份证放置于读卡区域";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbltime
            // 
            this.lbltime.AutoSize = true;
            this.lbltime.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbltime.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltime.Location = new System.Drawing.Point(1108, 47);
            this.lbltime.Name = "lbltime";
            this.lbltime.Size = new System.Drawing.Size(122, 44);
            this.lbltime.TabIndex = 25;
            this.lbltime.Text = "label1";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick_1);
            // 
            // lbltime1
            // 
            this.lbltime1.AutoSize = true;
            this.lbltime1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbltime1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbltime1.Location = new System.Drawing.Point(984, 66);
            this.lbltime1.Name = "lbltime1";
            this.lbltime1.Size = new System.Drawing.Size(64, 25);
            this.lbltime1.TabIndex = 26;
            this.lbltime1.Text = "label1";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.linkLabel3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel3.LinkColor = System.Drawing.Color.Red;
            this.linkLabel3.Location = new System.Drawing.Point(1079, 389);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(107, 25);
            this.linkLabel3.TabIndex = 50;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "获取验证码";
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 3000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(1067, 222);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(100, 135);
            this.pictureBox7.TabIndex = 56;
            this.pictureBox7.TabStop = false;
            // 
            // Paneljp
            // 
            this.Paneljp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Paneljp.BackgroundImage")));
            this.Paneljp.Controls.Add(this.button2);
            this.Paneljp.Controls.Add(this.button1);
            this.Paneljp.Controls.Add(this.btnook);
            this.Paneljp.Controls.Add(this.button0);
            this.Paneljp.Controls.Add(this.button9);
            this.Paneljp.Controls.Add(this.button8);
            this.Paneljp.Controls.Add(this.button7);
            this.Paneljp.Controls.Add(this.button6);
            this.Paneljp.Controls.Add(this.btndelete);
            this.Paneljp.Controls.Add(this.button5);
            this.Paneljp.Controls.Add(this.button4);
            this.Paneljp.Controls.Add(this.button3);
            this.Paneljp.Location = new System.Drawing.Point(631, 455);
            this.Paneljp.Name = "Paneljp";
            this.Paneljp.Size = new System.Drawing.Size(602, 200);
            this.Paneljp.TabIndex = 46;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(119, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 70);
            this.button2.TabIndex = 30;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(22, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 70);
            this.button1.TabIndex = 29;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnook
            // 
            this.btnook.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnook.ForeColor = System.Drawing.Color.Transparent;
            this.btnook.Image = global::UI.Properties.Resources.icon_qr;
            this.btnook.Location = new System.Drawing.Point(505, 111);
            this.btnook.Name = "btnook";
            this.btnook.Size = new System.Drawing.Size(80, 70);
            this.btnook.TabIndex = 42;
            this.btnook.UseVisualStyleBackColor = false;
            this.btnook.Click += new System.EventHandler(this.btnook_Click);
            // 
            // button0
            // 
            this.button0.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button0.ForeColor = System.Drawing.Color.Transparent;
            this.button0.Image = global::UI.Properties.Resources.icon_00;
            this.button0.Location = new System.Drawing.Point(410, 111);
            this.button0.Name = "button0";
            this.button0.Size = new System.Drawing.Size(80, 70);
            this.button0.TabIndex = 38;
            this.button0.UseVisualStyleBackColor = false;
            this.button0.Click += new System.EventHandler(this.button0_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.Transparent;
            this.button9.Image = global::UI.Properties.Resources.icon_09;
            this.button9.Location = new System.Drawing.Point(312, 111);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(80, 70);
            this.button9.TabIndex = 37;
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.Color.Transparent;
            this.button8.Image = global::UI.Properties.Resources.icon_08;
            this.button8.Location = new System.Drawing.Point(215, 111);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 70);
            this.button8.TabIndex = 36;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.Transparent;
            this.button7.Image = global::UI.Properties.Resources.icon_07;
            this.button7.Location = new System.Drawing.Point(119, 111);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(80, 70);
            this.button7.TabIndex = 35;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.Transparent;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Location = new System.Drawing.Point(22, 111);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(80, 70);
            this.button6.TabIndex = 34;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btndelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndelete.ForeColor = System.Drawing.Color.Transparent;
            this.btndelete.Image = ((System.Drawing.Image)(resources.GetObject("btndelete.Image")));
            this.btndelete.Location = new System.Drawing.Point(505, 21);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(80, 70);
            this.btndelete.TabIndex = 40;
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.Transparent;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(409, 21);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(80, 70);
            this.button5.TabIndex = 33;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(312, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 70);
            this.button4.TabIndex = 32;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(215, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 70);
            this.button3.TabIndex = 31;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Image = global::UI.Properties.Resources.姓身份证手机验证码;
            this.pictureBox6.Location = new System.Drawing.Point(682, 241);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(82, 167);
            this.pictureBox6.TabIndex = 27;
            this.pictureBox6.TabStop = false;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Image = global::UI.Properties.Resources.获取验证码;
            this.linkLabel2.Location = new System.Drawing.Point(1050, 375);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(150, 48);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            this.linkLabel2.Click += new System.EventHandler(this.linkLabel2_LinkClicked);
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.pictureBox5.Location = new System.Drawing.Point(631, 196);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(602, 253);
            this.pictureBox5.TabIndex = 24;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Image = global::UI.Properties.Resources.jpg_02;
            this.pictureBox4.Location = new System.Drawing.Point(631, 125);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(602, 52);
            this.pictureBox4.TabIndex = 22;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(151, 501);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(341, 54);
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.Transparent;
            this.btnOK.Image = global::UI.Properties.Resources.确认信息;
            this.btnOK.Location = new System.Drawing.Point(820, 563);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(204, 48);
            this.btnOK.TabIndex = 16;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1366, 768);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(511, 165);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(500, 284);
            this.pictureBox3.TabIndex = 20;
            this.pictureBox3.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(588, 41);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 12);
            this.linkLabel1.TabIndex = 58;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // wsFaces1
            // 
            this.wsFaces1.Url = "http://localhost:4862/WSFaces.asmx";
            this.wsFaces1.UseDefaultCredentials = true;
            // 
            // FaceRegis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 599);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.Paneljp);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.lbltime1);
            this.Controls.Add(this.lbltime);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.textCode);
            this.Controls.Add(this.txtOnePhone);
            this.Controls.Add(this.txtOneIDNum);
            this.Controls.Add(this.txtOneName);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.videPlayer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "FaceRegis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实名认证";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceRegis_FormClosing);
            this.Load += new System.EventHandler(this.FaceRegis_Load);
            this.Shown += new System.EventHandler(this.FaceRegis_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.Paneljp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private AForge.Controls.VideoSourcePlayer videPlayer;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.TextBox textCode;
        private System.Windows.Forms.TextBox txtOnePhone;
        private System.Windows.Forms.TextBox txtOneIDNum;
        private System.Windows.Forms.TextBox txtOneName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbltime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lbltime1;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button0;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnook;
        private System.Windows.Forms.Panel Paneljp;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Test.WSFaces wsFaces1;
    }
}