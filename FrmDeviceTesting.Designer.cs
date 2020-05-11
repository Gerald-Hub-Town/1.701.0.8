namespace LCRTest
{
    partial class FrmDeviceTesting
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.av = new System.Windows.Forms.Panel();
            this.cbAutoMode = new System.Windows.Forms.CheckBox();
            this.aw = new System.Windows.Forms.Panel();
            this.x = new System.Windows.Forms.Label();
            this.v = new System.Windows.Forms.Label();
            this.y = new System.Windows.Forms.Label();
            this.u = new System.Windows.Forms.Label();
            this.z = new System.Windows.Forms.Label();
            this.aa = new System.Windows.Forms.Label();
            this.w = new System.Windows.Forms.Label();
            this.m = new System.Windows.Forms.ComboBox();
            this.n = new System.Windows.Forms.ComboBox();
            this.l = new System.Windows.Forms.CheckBox();
            this.o = new System.Windows.Forms.Label();
            this.k = new System.Windows.Forms.CheckBox();
            this.p = new System.Windows.Forms.Label();
            this.q = new System.Windows.Forms.Label();
            this.i = new System.Windows.Forms.ComboBox();
            this.r = new System.Windows.Forms.Label();
            this.h = new System.Windows.Forms.ComboBox();
            this.s = new System.Windows.Forms.Label();
            this.g = new System.Windows.Forms.ComboBox();
            this.j = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUpperTo = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txtLC = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txtCPN = new System.Windows.Forms.TextBox();
            this.txtMakeValue = new System.Windows.Forms.TextBox();
            this.txtVendor = new System.Windows.Forms.TextBox();
            this.txtDC = new System.Windows.Forms.TextBox();
            this.txtLowerTo = new System.Windows.Forms.TextBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.txtDESC = new System.Windows.Forms.TextBox();
            this.txtreelid = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ab = new System.IO.Ports.SerialPort(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.av.SuspendLayout();
            this.aw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(740, 634);
            this.splitContainer2.SplitterDistance = 165;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.av);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 634);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "串口设置";
            // 
            // av
            // 
            this.av.Controls.Add(this.cbAutoMode);
            this.av.Controls.Add(this.aw);
            this.av.Controls.Add(this.m);
            this.av.Controls.Add(this.n);
            this.av.Controls.Add(this.l);
            this.av.Controls.Add(this.o);
            this.av.Controls.Add(this.k);
            this.av.Controls.Add(this.p);
            this.av.Controls.Add(this.q);
            this.av.Controls.Add(this.i);
            this.av.Controls.Add(this.r);
            this.av.Controls.Add(this.h);
            this.av.Controls.Add(this.s);
            this.av.Controls.Add(this.g);
            this.av.Controls.Add(this.j);
            this.av.Dock = System.Windows.Forms.DockStyle.Fill;
            this.av.Location = new System.Drawing.Point(3, 17);
            this.av.Name = "av";
            this.av.Size = new System.Drawing.Size(159, 614);
            this.av.TabIndex = 16;
            // 
            // cbAutoMode
            // 
            this.cbAutoMode.AutoSize = true;
            this.cbAutoMode.Location = new System.Drawing.Point(18, 326);
            this.cbAutoMode.Name = "cbAutoMode";
            this.cbAutoMode.Size = new System.Drawing.Size(72, 16);
            this.cbAutoMode.TabIndex = 15;
            this.cbAutoMode.Text = "处理数据";
            this.cbAutoMode.UseVisualStyleBackColor = true;
            // 
            // aw
            // 
            this.aw.Controls.Add(this.x);
            this.aw.Controls.Add(this.v);
            this.aw.Controls.Add(this.y);
            this.aw.Controls.Add(this.u);
            this.aw.Controls.Add(this.z);
            this.aw.Controls.Add(this.aa);
            this.aw.Controls.Add(this.w);
            this.aw.Location = new System.Drawing.Point(11, 198);
            this.aw.Name = "aw";
            this.aw.Size = new System.Drawing.Size(130, 90);
            this.aw.TabIndex = 14;
            // 
            // x
            // 
            this.x.AutoSize = true;
            this.x.Location = new System.Drawing.Point(26, 43);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(59, 12);
            this.x.TabIndex = 3;
            this.x.Text = "发送计数:";
            // 
            // v
            // 
            this.v.AutoSize = true;
            this.v.Location = new System.Drawing.Point(88, 43);
            this.v.Name = "v";
            this.v.Size = new System.Drawing.Size(11, 12);
            this.v.TabIndex = 5;
            this.v.Text = "0";
            // 
            // y
            // 
            this.y.AutoSize = true;
            this.y.Location = new System.Drawing.Point(54, 22);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(29, 12);
            this.y.TabIndex = 2;
            this.y.Text = "9600";
            // 
            // u
            // 
            this.u.AutoSize = true;
            this.u.Location = new System.Drawing.Point(88, 65);
            this.u.Name = "u";
            this.u.Size = new System.Drawing.Size(11, 12);
            this.u.TabIndex = 6;
            this.u.Text = "0";
            // 
            // z
            // 
            this.z.AutoSize = true;
            this.z.Location = new System.Drawing.Point(42, 6);
            this.z.Name = "z";
            this.z.Size = new System.Drawing.Size(65, 12);
            this.z.TabIndex = 1;
            this.z.Text = "COM1已关闭";
            // 
            // aa
            // 
            this.aa.AutoSize = true;
            this.aa.ForeColor = System.Drawing.Color.DarkGray;
            this.aa.Location = new System.Drawing.Point(16, 6);
            this.aa.Name = "aa";
            this.aa.Size = new System.Drawing.Size(17, 12);
            this.aa.TabIndex = 7;
            this.aa.Text = "●";
            // 
            // w
            // 
            this.w.AutoSize = true;
            this.w.Location = new System.Drawing.Point(26, 65);
            this.w.Name = "w";
            this.w.Size = new System.Drawing.Size(59, 12);
            this.w.TabIndex = 4;
            this.w.Text = "接收计数:";
            // 
            // m
            // 
            this.m.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m.FormattingEnabled = true;
            this.m.Items.AddRange(new object[] {
            "0",
            "1",
            "1.5",
            "2"});
            this.m.Location = new System.Drawing.Point(59, 69);
            this.m.Name = "m";
            this.m.Size = new System.Drawing.Size(60, 20);
            this.m.TabIndex = 9;
            this.m.SelectedIndexChanged += new System.EventHandler(this.m_SelectedIndexChanged);
            // 
            // n
            // 
            this.n.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.n.FormattingEnabled = true;
            this.n.Items.AddRange(new object[] {
            "COM1",
            "COM2"});
            this.n.Location = new System.Drawing.Point(59, 3);
            this.n.Name = "n";
            this.n.Size = new System.Drawing.Size(60, 20);
            this.n.TabIndex = 8;
            this.n.SelectedIndexChanged += new System.EventHandler(this.n_SelectedIndexChanged);
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Location = new System.Drawing.Point(25, 119);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(42, 16);
            this.l.TabIndex = 6;
            this.l.Text = "DTR";
            this.l.UseVisualStyleBackColor = true;
            this.l.CheckedChanged += new System.EventHandler(this.l_CheckedChanged);
            // 
            // o
            // 
            this.o.AutoSize = true;
            this.o.Location = new System.Drawing.Point(9, 94);
            this.o.Name = "o";
            this.o.Size = new System.Drawing.Size(47, 12);
            this.o.TabIndex = 4;
            this.o.Text = "校验位:";
            // 
            // k
            // 
            this.k.Location = new System.Drawing.Point(73, 119);
            this.k.Name = "k";
            this.k.Size = new System.Drawing.Size(42, 16);
            this.k.TabIndex = 7;
            this.k.Text = "RTS";
            this.k.UseVisualStyleBackColor = true;
            this.k.CheckedChanged += new System.EventHandler(this.k_CheckedChanged);
            // 
            // p
            // 
            this.p.AutoSize = true;
            this.p.Location = new System.Drawing.Point(9, 72);
            this.p.Name = "p";
            this.p.Size = new System.Drawing.Size(47, 12);
            this.p.TabIndex = 3;
            this.p.Text = "停止位:";
            // 
            // q
            // 
            this.q.AutoSize = true;
            this.q.Location = new System.Drawing.Point(9, 50);
            this.q.Name = "q";
            this.q.Size = new System.Drawing.Size(47, 12);
            this.q.TabIndex = 2;
            this.q.Text = "数据位:";
            // 
            // i
            // 
            this.i.DisplayMember = "4";
            this.i.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.i.FormattingEnabled = true;
            this.i.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.i.Location = new System.Drawing.Point(59, 91);
            this.i.Name = "i";
            this.i.Size = new System.Drawing.Size(60, 20);
            this.i.TabIndex = 12;
            this.i.SelectedIndexChanged += new System.EventHandler(this.i_SelectedIndexChanged);
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Location = new System.Drawing.Point(9, 28);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(47, 12);
            this.r.TabIndex = 1;
            this.r.Text = "波特率:";
            // 
            // h
            // 
            this.h.FormattingEnabled = true;
            this.h.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "230400",
            "256000"});
            this.h.Location = new System.Drawing.Point(59, 25);
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(60, 20);
            this.h.TabIndex = 10;
            this.h.SelectedIndexChanged += new System.EventHandler(this.h_SelectedIndexChanged);
            // 
            // s
            // 
            this.s.Location = new System.Drawing.Point(9, 6);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(47, 12);
            this.s.TabIndex = 0;
            this.s.Text = "串口号:";
            // 
            // g
            // 
            this.g.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.g.FormattingEnabled = true;
            this.g.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.g.Location = new System.Drawing.Point(59, 47);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(60, 20);
            this.g.TabIndex = 13;
            this.g.SelectedIndexChanged += new System.EventHandler(this.g_SelectedIndexChanged);
            // 
            // j
            // 
            this.j.Location = new System.Drawing.Point(28, 141);
            this.j.Name = "j";
            this.j.Size = new System.Drawing.Size(75, 23);
            this.j.TabIndex = 2;
            this.j.Text = "打开串口";
            this.j.UseVisualStyleBackColor = true;
            this.j.Click += new System.EventHandler(this.j_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer3.Size = new System.Drawing.Size(571, 634);
            this.splitContainer3.SplitterDistance = 141;
            this.splitContainer3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUpperTo);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.txtLC);
            this.groupBox2.Controls.Add(this.txtUnit);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.txtCPN);
            this.groupBox2.Controls.Add(this.txtMakeValue);
            this.groupBox2.Controls.Add(this.txtVendor);
            this.groupBox2.Controls.Add(this.txtDC);
            this.groupBox2.Controls.Add(this.txtLowerTo);
            this.groupBox2.Controls.Add(this.txtQTY);
            this.groupBox2.Controls.Add(this.txtDESC);
            this.groupBox2.Controls.Add(this.txtreelid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 141);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据发送";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 42;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "label2";
            // 
            // txtUpperTo
            // 
            this.txtUpperTo.BackColor = System.Drawing.Color.Gray;
            this.txtUpperTo.ForeColor = System.Drawing.Color.Yellow;
            this.txtUpperTo.Location = new System.Drawing.Point(324, 28);
            this.txtUpperTo.Name = "txtUpperTo";
            this.txtUpperTo.ReadOnly = true;
            this.txtUpperTo.Size = new System.Drawing.Size(91, 21);
            this.txtUpperTo.TabIndex = 40;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(714, 28);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(157, 21);
            this.textBox5.TabIndex = 16;
            // 
            // txtLC
            // 
            this.txtLC.BackColor = System.Drawing.Color.Gray;
            this.txtLC.ForeColor = System.Drawing.Color.Yellow;
            this.txtLC.Location = new System.Drawing.Point(62, 117);
            this.txtLC.Name = "txtLC";
            this.txtLC.ReadOnly = true;
            this.txtLC.Size = new System.Drawing.Size(157, 21);
            this.txtLC.TabIndex = 36;
            // 
            // txtUnit
            // 
            this.txtUnit.BackColor = System.Drawing.Color.Gray;
            this.txtUnit.ForeColor = System.Drawing.Color.Yellow;
            this.txtUnit.Location = new System.Drawing.Point(432, 28);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(97, 21);
            this.txtUnit.TabIndex = 38;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(244, 60);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(171, 21);
            this.textBox7.TabIndex = 18;
            // 
            // txtCPN
            // 
            this.txtCPN.Location = new System.Drawing.Point(62, 58);
            this.txtCPN.Name = "txtCPN";
            this.txtCPN.Size = new System.Drawing.Size(157, 21);
            this.txtCPN.TabIndex = 15;
            this.txtCPN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox4_KeyDown);
            // 
            // txtMakeValue
            // 
            this.txtMakeValue.BackColor = System.Drawing.Color.White;
            this.txtMakeValue.ForeColor = System.Drawing.Color.Yellow;
            this.txtMakeValue.Location = new System.Drawing.Point(564, 28);
            this.txtMakeValue.Name = "txtMakeValue";
            this.txtMakeValue.ReadOnly = true;
            this.txtMakeValue.Size = new System.Drawing.Size(111, 21);
            this.txtMakeValue.TabIndex = 37;
            // 
            // txtVendor
            // 
            this.txtVendor.BackColor = System.Drawing.Color.Gray;
            this.txtVendor.ForeColor = System.Drawing.Color.Yellow;
            this.txtVendor.Location = new System.Drawing.Point(432, 89);
            this.txtVendor.Name = "txtVendor";
            this.txtVendor.ReadOnly = true;
            this.txtVendor.Size = new System.Drawing.Size(97, 21);
            this.txtVendor.TabIndex = 32;
            // 
            // txtDC
            // 
            this.txtDC.BackColor = System.Drawing.Color.Gray;
            this.txtDC.ForeColor = System.Drawing.Color.Yellow;
            this.txtDC.Location = new System.Drawing.Point(432, 58);
            this.txtDC.Name = "txtDC";
            this.txtDC.ReadOnly = true;
            this.txtDC.Size = new System.Drawing.Size(97, 21);
            this.txtDC.TabIndex = 35;
            // 
            // txtLowerTo
            // 
            this.txtLowerTo.BackColor = System.Drawing.Color.Gray;
            this.txtLowerTo.ForeColor = System.Drawing.Color.Yellow;
            this.txtLowerTo.Location = new System.Drawing.Point(244, 28);
            this.txtLowerTo.Name = "txtLowerTo";
            this.txtLowerTo.ReadOnly = true;
            this.txtLowerTo.Size = new System.Drawing.Size(74, 21);
            this.txtLowerTo.TabIndex = 39;
            // 
            // txtQTY
            // 
            this.txtQTY.BackColor = System.Drawing.Color.Gray;
            this.txtQTY.ForeColor = System.Drawing.Color.Yellow;
            this.txtQTY.Location = new System.Drawing.Point(62, 89);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.ReadOnly = true;
            this.txtQTY.Size = new System.Drawing.Size(157, 21);
            this.txtQTY.TabIndex = 34;
            // 
            // txtDESC
            // 
            this.txtDESC.BackColor = System.Drawing.Color.Gray;
            this.txtDESC.ForeColor = System.Drawing.Color.Yellow;
            this.txtDESC.Location = new System.Drawing.Point(244, 89);
            this.txtDESC.Multiline = true;
            this.txtDESC.Name = "txtDESC";
            this.txtDESC.ReadOnly = true;
            this.txtDESC.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDESC.Size = new System.Drawing.Size(171, 49);
            this.txtDESC.TabIndex = 33;
            // 
            // txtreelid
            // 
            this.txtreelid.Location = new System.Drawing.Point(62, 28);
            this.txtreelid.Name = "txtreelid";
            this.txtreelid.Size = new System.Drawing.Size(157, 21);
            this.txtreelid.TabIndex = 17;
            this.txtreelid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtreelid_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 489);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据接收";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 17);
            this.textBox1.MaxLength = 3276700;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(565, 469);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(740, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "LCR设备测试";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(740, 686);
            this.splitContainer1.SplitterDistance = 48;
            this.splitContainer1.TabIndex = 0;
            // 
            // FrmDeviceTesting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 686);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmDeviceTesting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LCR设备测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmDeviceTesting_FormClosed);
            this.Load += new System.EventHandler(this.FrmDeviceTesting_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.av.ResumeLayout(false);
            this.av.PerformLayout();
            this.aw.ResumeLayout(false);
            this.aw.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort ab;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel av;
        private System.Windows.Forms.Panel aw;
        private System.Windows.Forms.Label x;
        private System.Windows.Forms.Label v;
        private System.Windows.Forms.Label y;
        private System.Windows.Forms.Label u;
        private System.Windows.Forms.Label z;
        private System.Windows.Forms.Label aa;
        private System.Windows.Forms.Label w;
        private System.Windows.Forms.ComboBox m;
        private System.Windows.Forms.ComboBox n;
        private System.Windows.Forms.CheckBox l;
        private System.Windows.Forms.Label o;
        private System.Windows.Forms.CheckBox k;
        private System.Windows.Forms.Label p;
        private System.Windows.Forms.Label q;
        private System.Windows.Forms.ComboBox i;
        private System.Windows.Forms.Label r;
        private System.Windows.Forms.ComboBox h;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.ComboBox g;
        private System.Windows.Forms.Button j;
        private System.Windows.Forms.TextBox txtCPN;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox txtreelid;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox txtUpperTo;
        private System.Windows.Forms.TextBox txtLowerTo;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtMakeValue;
        private System.Windows.Forms.TextBox txtLC;
        private System.Windows.Forms.TextBox txtDC;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.TextBox txtDESC;
        private System.Windows.Forms.TextBox txtVendor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAutoMode;
    }
}