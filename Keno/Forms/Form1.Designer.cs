namespace Keno
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clearTable = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLogIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.profitLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.siteStake = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.currencySelect = new System.Windows.Forms.ComboBox();
            this.riskSelect = new System.Windows.Forms.ComboBox();
            this.BetCost = new System.Windows.Forms.NumericUpDown();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.autoPickBtn = new System.Windows.Forms.Button();
            this.LiveBalLabel = new System.Windows.Forms.TextBox();
            this.riskLabel = new System.Windows.Forms.Label();
            this.stratSelector = new Keno.stratGrid();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BetCost)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(729, 270);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "Manual Bet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(466, 270);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 22);
            this.button2.TabIndex = 2;
            this.button2.Text = "Start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // clearTable
            // 
            this.clearTable.Location = new System.Drawing.Point(652, 270);
            this.clearTable.Name = "clearTable";
            this.clearTable.Size = new System.Drawing.Size(77, 22);
            this.clearTable.TabIndex = 4;
            this.clearTable.Text = "Clear Table";
            this.clearTable.UseVisualStyleBackColor = true;
            this.clearTable.Click += new System.EventHandler(this.clearTable_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Location = new System.Drawing.Point(0, 289);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 127);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.SystemColors.Control;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 8);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 119);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Hits";
            this.columnHeader2.Width = 110;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Picks";
            this.columnHeader3.Width = 165;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Result";
            this.columnHeader4.Width = 160;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Risk";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Bet Cost";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Multiplier";
            this.columnHeader7.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Payout";
            this.columnHeader8.Width = 100;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLogIn});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(12, 0, 1, 0);
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLogIn
            // 
            this.StatusLogIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StatusLogIn.Name = "StatusLogIn";
            this.StatusLogIn.Size = new System.Drawing.Size(78, 17);
            this.StatusLogIn.Text = "Unauthorized";
            this.StatusLogIn.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(459, 289);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.profitLabel);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.siteStake);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.currencySelect);
            this.tabPage1.Controls.Add(this.riskSelect);
            this.tabPage1.Controls.Add(this.BetCost);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(451, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // profitLabel
            // 
            this.profitLabel.AutoSize = true;
            this.profitLabel.Location = new System.Drawing.Point(59, 71);
            this.profitLabel.Name = "profitLabel";
            this.profitLabel.Size = new System.Drawing.Size(64, 13);
            this.profitLabel.TabIndex = 12;
            this.profitLabel.Text = "0.00000000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Profit:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "bet cost:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "apikey:";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(199, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 103);
            this.panel1.TabIndex = 8;
            // 
            // siteStake
            // 
            this.siteStake.BackColor = System.Drawing.SystemColors.Window;
            this.siteStake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.siteStake.FormattingEnabled = true;
            this.siteStake.Items.AddRange(new object[] {
            "Stake.com",
            "Stake.bet",
            "Stake.games",
            "Staketr.com",
            "Staketr2.com",
            "Staketr3.com",
            "Staketr4.com",
            "Staketr5.com",
            "Stake.bz",
            "Stake.jp",
            "Stake.ac",
            "Stake.icu"});
            this.siteStake.Location = new System.Drawing.Point(358, 3);
            this.siteStake.Name = "siteStake";
            this.siteStake.Size = new System.Drawing.Size(90, 21);
            this.siteStake.TabIndex = 7;
            this.siteStake.SelectedIndexChanged += new System.EventHandler(this.siteStake_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.UseSystemPasswordChar = true;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // currencySelect
            // 
            this.currencySelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currencySelect.FormattingEnabled = true;
            this.currencySelect.Items.AddRange(new object[] {
            "BTC",
            "ETH",
            "LTC",
            "DOGE",
            "XRP",
            "BCH",
            "TRX",
            "EOS"});
            this.currencySelect.Location = new System.Drawing.Point(295, 27);
            this.currencySelect.Name = "currencySelect";
            this.currencySelect.Size = new System.Drawing.Size(153, 21);
            this.currencySelect.TabIndex = 4;
            this.currencySelect.SelectedIndexChanged += new System.EventHandler(this.currencySelect_SelectedIndexChanged);
            // 
            // riskSelect
            // 
            this.riskSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.riskSelect.FormattingEnabled = true;
            this.riskSelect.Items.AddRange(new object[] {
            "Classic",
            "Low",
            "Medium",
            "High"});
            this.riskSelect.Location = new System.Drawing.Point(188, 27);
            this.riskSelect.Name = "riskSelect";
            this.riskSelect.Size = new System.Drawing.Size(105, 21);
            this.riskSelect.TabIndex = 2;
            this.riskSelect.SelectedIndexChanged += new System.EventHandler(this.riskSelect_SelectedIndexChanged);
            // 
            // BetCost
            // 
            this.BetCost.DecimalPlaces = 8;
            this.BetCost.Increment = new decimal(new int[] {
            1,
            0,
            0,
            393216});
            this.BetCost.Location = new System.Drawing.Point(63, 28);
            this.BetCost.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.BetCost.Name = "BetCost";
            this.BetCost.Size = new System.Drawing.Size(123, 20);
            this.BetCost.TabIndex = 0;
            this.BetCost.ValueChanged += new System.EventHandler(this.BetCost_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(451, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.SystemColors.Control;
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView2.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView2.HideSelection = false;
            this.listView2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.listView2.Location = new System.Drawing.Point(466, 230);
            this.listView2.Name = "listView2";
            this.listView2.Scrollable = false;
            this.listView2.Size = new System.Drawing.Size(334, 41);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // autoPickBtn
            // 
            this.autoPickBtn.Location = new System.Drawing.Point(588, 270);
            this.autoPickBtn.Name = "autoPickBtn";
            this.autoPickBtn.Size = new System.Drawing.Size(64, 22);
            this.autoPickBtn.TabIndex = 0;
            this.autoPickBtn.Text = "Auto Pick";
            this.autoPickBtn.UseVisualStyleBackColor = true;
            this.autoPickBtn.Click += new System.EventHandler(this.autoPickBtn_Click);
            // 
            // LiveBalLabel
            // 
            this.LiveBalLabel.BackColor = System.Drawing.SystemColors.Control;
            this.LiveBalLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LiveBalLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.LiveBalLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.LiveBalLabel.Location = new System.Drawing.Point(470, 5);
            this.LiveBalLabel.Name = "LiveBalLabel";
            this.LiveBalLabel.ReadOnly = true;
            this.LiveBalLabel.Size = new System.Drawing.Size(86, 16);
            this.LiveBalLabel.TabIndex = 9;
            this.LiveBalLabel.Text = "Live Balance";
            this.LiveBalLabel.TextChanged += new System.EventHandler(this.LiveBalLabel_TextChanged);
            // 
            // riskLabel
            // 
            this.riskLabel.AutoSize = true;
            this.riskLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.riskLabel.Location = new System.Drawing.Point(721, 6);
            this.riskLabel.Name = "riskLabel";
            this.riskLabel.Size = new System.Drawing.Size(31, 15);
            this.riskLabel.TabIndex = 10;
            this.riskLabel.Text = "Risk:";
            // 
            // stratSelector
            // 
            this.stratSelector.Location = new System.Drawing.Point(462, 21);
            this.stratSelector.Name = "stratSelector";
            this.stratSelector.Size = new System.Drawing.Size(398, 398);
            this.stratSelector.squareData = new int[] {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0};
            this.stratSelector.SquareSpacing = 3;
            this.stratSelector.TabIndex = 2;
            this.stratSelector.Text = "stratGrid1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 437);
            this.Controls.Add(this.riskLabel);
            this.Controls.Add(this.LiveBalLabel);
            this.Controls.Add(this.autoPickBtn);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.clearTable);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stratSelector);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Keno";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BetCost)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private stratGrid stratSelector;
        private System.Windows.Forms.Button clearTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Button autoPickBtn;
        private System.Windows.Forms.ComboBox riskSelect;
        private System.Windows.Forms.NumericUpDown BetCost;
        private System.Windows.Forms.ComboBox currencySelect;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox LiveBalLabel;
        private System.Windows.Forms.Label riskLabel;
        private System.Windows.Forms.ComboBox siteStake;
        private System.Windows.Forms.ToolStripStatusLabel StatusLogIn;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label profitLabel;
        private System.Windows.Forms.Label label3;
    }
}

