namespace Personal_Investment_App
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip2 = new MenuStrip();
            inwestycjePersonalneToolStripMenuItem = new ToolStripMenuItem();
            sprzedajInwestycjęToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            generujRaportToolStripMenuItem = new ToolStripMenuItem();
            eksportujDaneToolStripMenuItem = new ToolStripMenuItem();
            dezaktywujInwestycjęToolStripMenuItem = new ToolStripMenuItem();
            wylogujToolStripMenuItem1 = new ToolStripMenuItem();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            listView1 = new ListView();
            Typ_inwestycji = new ColumnHeader();
            Nazwa_Inwestycji = new ColumnHeader();
            Data_zakupu = new ColumnHeader();
            Kwota_inwestycji = new ColumnHeader();
            Obecna_wartość = new ColumnHeader();
            imageList1 = new ImageList(components);
            menuStrip2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = SystemColors.ActiveBorder;
            menuStrip2.Dock = DockStyle.Left;
            menuStrip2.Items.AddRange(new ToolStripItem[] { inwestycjePersonalneToolStripMenuItem, sprzedajInwestycjęToolStripMenuItem, toolStripMenuItem1, generujRaportToolStripMenuItem, eksportujDaneToolStripMenuItem, dezaktywujInwestycjęToolStripMenuItem, wylogujToolStripMenuItem1 });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(127, 450);
            menuStrip2.TabIndex = 1;
            menuStrip2.Text = "menuStrip2";
            // 
            // inwestycjePersonalneToolStripMenuItem
            // 
            inwestycjePersonalneToolStripMenuItem.Name = "inwestycjePersonalneToolStripMenuItem";
            inwestycjePersonalneToolStripMenuItem.Size = new Size(114, 19);
            inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";
            inwestycjePersonalneToolStripMenuItem.Click += inwestycjePersonalneToolStripMenuItem_Click;
            // 
            // sprzedajInwestycjęToolStripMenuItem
            // 
            sprzedajInwestycjęToolStripMenuItem.Name = "sprzedajInwestycjęToolStripMenuItem";
            sprzedajInwestycjęToolStripMenuItem.Size = new Size(114, 19);
            sprzedajInwestycjęToolStripMenuItem.Text = "Sprzedaj inwestycję";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(114, 4);
            // 
            // generujRaportToolStripMenuItem
            // 
            generujRaportToolStripMenuItem.Name = "generujRaportToolStripMenuItem";
            generujRaportToolStripMenuItem.Size = new Size(114, 19);
            generujRaportToolStripMenuItem.Text = "Generuj raport";
            // 
            // eksportujDaneToolStripMenuItem
            // 
            eksportujDaneToolStripMenuItem.Name = "eksportujDaneToolStripMenuItem";
            eksportujDaneToolStripMenuItem.Size = new Size(114, 19);
            eksportujDaneToolStripMenuItem.Text = "Eksportuj dane";
            // 
            // dezaktywujInwestycjęToolStripMenuItem
            // 
            dezaktywujInwestycjęToolStripMenuItem.Name = "dezaktywujInwestycjęToolStripMenuItem";
            dezaktywujInwestycjęToolStripMenuItem.Size = new Size(114, 19);
            dezaktywujInwestycjęToolStripMenuItem.Text = "Usuń konto";
            // 
            // wylogujToolStripMenuItem1
            // 
            wylogujToolStripMenuItem1.Name = "wylogujToolStripMenuItem1";
            wylogujToolStripMenuItem1.Size = new Size(114, 19);
            wylogujToolStripMenuItem1.Text = "Wyloguj";
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(117, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(682, 450);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(listView1);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(682, 450);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { Typ_inwestycji, Nazwa_Inwestycji, Data_zakupu, Kwota_inwestycji, Obecna_wartość });
            listView1.Location = new Point(0, 16);
            listView1.Name = "listView1";
            listView1.Size = new Size(676, 428);
            listView1.SmallImageList = imageList1;
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.SmallIcon;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "bitcoin.png");
            imageList1.Images.SetKeyName(1, "graph.png");
            imageList1.Images.SetKeyName(2, "business.png");
            imageList1.Images.SetKeyName(3, "bars.png");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(menuStrip2);
            Name = "Form1";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip2;
        private ToolStripMenuItem inwestycjePersonalneToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem dezaktywujInwestycjęToolStripMenuItem;
        private ToolStripMenuItem generujRaportToolStripMenuItem;
        private ToolStripMenuItem wylogujToolStripMenuItem1;
        private Panel panel1;
        private ToolStripMenuItem sprzedajInwestycjęToolStripMenuItem;
        private ImageList imageList1;
        private ToolStripMenuItem eksportujDaneToolStripMenuItem;
        private GroupBox groupBox1;
        private ListView listView1;
        private ColumnHeader Typ_inwestycji;
        private ColumnHeader Nazwa_Inwestycji;
        private ColumnHeader Data_zakupu;
        private ColumnHeader Kwota_inwestycji;
        private ColumnHeader Obecna_wartość;
    }
}
