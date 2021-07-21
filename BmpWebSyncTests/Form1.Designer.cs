namespace BmpWebSyncTests
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
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.getStatus = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckConnection = new System.Windows.Forms.Button();
            this.getGroups = new System.Windows.Forms.Button();
            this.putGroups = new System.Windows.Forms.Button();
            this.getProducts = new System.Windows.Forms.Button();
            this.putProducts = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.putfiles = new System.Windows.Forms.Button();
            this.all = new System.Windows.Forms.Button();
            this.getPrices = new System.Windows.Forms.Button();
            this.putPrices = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.putStatus = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.getStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Location = new System.Drawing.Point(4, 121);
            this.rtb.Margin = new System.Windows.Forms.Padding(4);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(1059, 77);
            this.rtb.TabIndex = 0;
            this.rtb.Text = "";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.rtb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.getStatus, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1067, 554);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(4, 206);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1059, 344);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // getStatus
            // 
            this.getStatus.Controls.Add(this.CheckConnection);
            this.getStatus.Controls.Add(this.getGroups);
            this.getStatus.Controls.Add(this.putGroups);
            this.getStatus.Controls.Add(this.getProducts);
            this.getStatus.Controls.Add(this.putProducts);
            this.getStatus.Controls.Add(this.button5);
            this.getStatus.Controls.Add(this.putfiles);
            this.getStatus.Controls.Add(this.all);
            this.getStatus.Controls.Add(this.getPrices);
            this.getStatus.Controls.Add(this.putPrices);
            this.getStatus.Controls.Add(this.button1);
            this.getStatus.Controls.Add(this.putStatus);
            this.getStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.getStatus.Location = new System.Drawing.Point(4, 4);
            this.getStatus.Margin = new System.Windows.Forms.Padding(4);
            this.getStatus.Name = "getStatus";
            this.getStatus.Size = new System.Drawing.Size(1059, 109);
            this.getStatus.TabIndex = 1;
            this.getStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // CheckConnection
            // 
            this.CheckConnection.Location = new System.Drawing.Point(4, 4);
            this.CheckConnection.Margin = new System.Windows.Forms.Padding(4);
            this.CheckConnection.Name = "CheckConnection";
            this.CheckConnection.Size = new System.Drawing.Size(100, 28);
            this.CheckConnection.TabIndex = 0;
            this.CheckConnection.Text = "CheckConnection";
            this.CheckConnection.UseVisualStyleBackColor = true;
            this.CheckConnection.Click += new System.EventHandler(this.CheckConnection_Click);
            // 
            // getGroups
            // 
            this.getGroups.Location = new System.Drawing.Point(112, 4);
            this.getGroups.Margin = new System.Windows.Forms.Padding(4);
            this.getGroups.Name = "getGroups";
            this.getGroups.Size = new System.Drawing.Size(100, 28);
            this.getGroups.TabIndex = 1;
            this.getGroups.Text = "getGroups";
            this.getGroups.UseVisualStyleBackColor = true;
            this.getGroups.Click += new System.EventHandler(this.button1_Click);
            // 
            // putGroups
            // 
            this.putGroups.Location = new System.Drawing.Point(220, 4);
            this.putGroups.Margin = new System.Windows.Forms.Padding(4);
            this.putGroups.Name = "putGroups";
            this.putGroups.Size = new System.Drawing.Size(100, 28);
            this.putGroups.TabIndex = 2;
            this.putGroups.Text = "putGroups";
            this.putGroups.UseVisualStyleBackColor = true;
            this.putGroups.Click += new System.EventHandler(this.button2_Click);
            // 
            // getProducts
            // 
            this.getProducts.Location = new System.Drawing.Point(328, 4);
            this.getProducts.Margin = new System.Windows.Forms.Padding(4);
            this.getProducts.Name = "getProducts";
            this.getProducts.Size = new System.Drawing.Size(100, 28);
            this.getProducts.TabIndex = 3;
            this.getProducts.Text = "getProducts";
            this.getProducts.UseVisualStyleBackColor = true;
            this.getProducts.Click += new System.EventHandler(this.button3_Click);
            // 
            // putProducts
            // 
            this.putProducts.Location = new System.Drawing.Point(436, 4);
            this.putProducts.Margin = new System.Windows.Forms.Padding(4);
            this.putProducts.Name = "putProducts";
            this.putProducts.Size = new System.Drawing.Size(100, 28);
            this.putProducts.TabIndex = 4;
            this.putProducts.Text = "putProducts";
            this.putProducts.UseVisualStyleBackColor = true;
            this.putProducts.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(544, 4);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 28);
            this.button5.TabIndex = 5;
            this.button5.Text = "getFiles";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // putfiles
            // 
            this.putfiles.Location = new System.Drawing.Point(652, 4);
            this.putfiles.Margin = new System.Windows.Forms.Padding(4);
            this.putfiles.Name = "putfiles";
            this.putfiles.Size = new System.Drawing.Size(100, 28);
            this.putfiles.TabIndex = 6;
            this.putfiles.Text = "putFiles";
            this.putfiles.UseVisualStyleBackColor = true;
            this.putfiles.Click += new System.EventHandler(this.putfiles_Click);
            // 
            // all
            // 
            this.all.Location = new System.Drawing.Point(760, 4);
            this.all.Margin = new System.Windows.Forms.Padding(4);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(100, 28);
            this.all.TabIndex = 7;
            this.all.Text = "all";
            this.all.UseVisualStyleBackColor = true;
            this.all.Click += new System.EventHandler(this.all_Click);
            // 
            // getPrices
            // 
            this.getPrices.Location = new System.Drawing.Point(868, 4);
            this.getPrices.Margin = new System.Windows.Forms.Padding(4);
            this.getPrices.Name = "getPrices";
            this.getPrices.Size = new System.Drawing.Size(100, 28);
            this.getPrices.TabIndex = 8;
            this.getPrices.Text = "getPrices";
            this.getPrices.UseVisualStyleBackColor = true;
            this.getPrices.Click += new System.EventHandler(this.getPrices_Click);
            // 
            // putPrices
            // 
            this.putPrices.Location = new System.Drawing.Point(4, 40);
            this.putPrices.Margin = new System.Windows.Forms.Padding(4);
            this.putPrices.Name = "putPrices";
            this.putPrices.Size = new System.Drawing.Size(100, 28);
            this.putPrices.TabIndex = 9;
            this.putPrices.Text = "putPrices";
            this.putPrices.UseVisualStyleBackColor = true;
            this.putPrices.Click += new System.EventHandler(this.putPrices_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 39);
            this.button1.Name = "getStatus";
            this.button1.Size = new System.Drawing.Size(101, 29);
            this.button1.TabIndex = 10;
            this.button1.Text = "getStatus";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.getStatusWarehouse_Click);
            // 
            // putStatus
            // 
            this.putStatus.Location = new System.Drawing.Point(218, 39);
            this.putStatus.Name = "putStatus";
            this.putStatus.Size = new System.Drawing.Size(102, 29);
            this.putStatus.TabIndex = 11;
            this.putStatus.Text = "putStatus";
            this.putStatus.UseVisualStyleBackColor = true;
            this.putStatus.Click += new System.EventHandler(this.putStatusWarehouse_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.getStatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel getStatus;
        private System.Windows.Forms.Button CheckConnection;
        private System.Windows.Forms.Button getGroups;
        private System.Windows.Forms.Button putGroups;
        private System.Windows.Forms.Button getProducts;
        private System.Windows.Forms.Button putProducts;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button putfiles;
        private System.Windows.Forms.Button all;
        private System.Windows.Forms.Button getPrices;
        private System.Windows.Forms.Button putPrices;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button putStatus;
    }
}

