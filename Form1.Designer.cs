namespace Personal_Investment_App
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip2 = new MenuStrip();
            inwestycjePersonalneToolStripMenuItem = new ToolStripMenuItem();
            sprzedajInwestycjęToolStripMenuItem = new ToolStripMenuItem();
            generujRaportToolStripMenuItem = new ToolStripMenuItem();
            eksportujDaneToolStripMenuItem = new ToolStripMenuItem();
            UsunKontoToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { inwestycjePersonalneToolStripMenuItem, sprzedajInwestycjęToolStripMenuItem, generujRaportToolStripMenuItem, eksportujDaneToolStripMenuItem, UsunKontoToolStripMenuItem, wylogujToolStripMenuItem1 });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Padding = new Padding(7, 3, 0, 3);
            menuStrip2.Size = new Size(914, 36);
            menuStrip2.TabIndex = 1;
            menuStrip2.Text = "menuStrip2";
            // 
            // inwestycjePersonalneToolStripMenuItem
            // 
            inwestycjePersonalneToolStripMenuItem.AutoSize = false;
            inwestycjePersonalneToolStripMenuItem.Name = "inwestycjePersonalneToolStripMenuItem";
            inwestycjePersonalneToolStripMenuItem.Size = new Size(140, 30);
            inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";
            inwestycjePersonalneToolStripMenuItem.Click += inwestycjePersonalneToolStripMenuItem_Click;
            // 
            // sprzedajInwestycjęToolStripMenuItem
            // 
            sprzedajInwestycjęToolStripMenuItem.AutoSize = false;
            sprzedajInwestycjęToolStripMenuItem.Name = "sprzedajInwestycjęToolStripMenuItem";
            sprzedajInwestycjęToolStripMenuItem.Size = new Size(140, 30);
            sprzedajInwestycjęToolStripMenuItem.Text = "Sprzedaj inwestycję";
            // 
            // generujRaportToolStripMenuItem
            // 
            generujRaportToolStripMenuItem.AutoSize = false;
            generujRaportToolStripMenuItem.Name = "generujRaportToolStripMenuItem";
            generujRaportToolStripMenuItem.Size = new Size(140, 30);
            generujRaportToolStripMenuItem.Text = "Generuj raport";
            // 
            // eksportujDaneToolStripMenuItem
            // 
            eksportujDaneToolStripMenuItem.AutoSize = false;
            eksportujDaneToolStripMenuItem.Name = "eksportujDaneToolStripMenuItem";
            eksportujDaneToolStripMenuItem.Size = new Size(140, 30);
            eksportujDaneToolStripMenuItem.Text = "Eksportuj dane";
            // 
            // UsunKontoToolStripMenuItem
            // 
            UsunKontoToolStripMenuItem.AutoSize = false;
            UsunKontoToolStripMenuItem.Name = "UsunKontoToolStripMenuItem";
            UsunKontoToolStripMenuItem.Size = new Size(140, 30);
            UsunKontoToolStripMenuItem.Text = "Usuń konto";
            UsunKontoToolStripMenuItem.Click += UsunKontoToolStripMenuItem_Click;
            // 
            // wylogujToolStripMenuItem1
            // 
            wylogujToolStripMenuItem1.AutoSize = false;
            wylogujToolStripMenuItem1.Name = "wylogujToolStripMenuItem1";
            wylogujToolStripMenuItem1.Size = new Size(140, 30);
            wylogujToolStripMenuItem1.Text = "Wyloguj";
            wylogujToolStripMenuItem1.Click += wylogujToolStripMenuItem1_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(145, 40);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(282, 237);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(listView1);
            groupBox1.Location = new Point(3, 16);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(769, 584);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { Typ_inwestycji, Nazwa_Inwestycji, Data_zakupu, Kwota_inwestycji, Obecna_wartość });
            listView1.Location = new Point(3, 23);
            listView1.Margin = new Padding(3, 4, 3, 4);
            listView1.Name = "listView1";
            listView1.Size = new Size(762, 553);
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
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(914, 600);
            Controls.Add(panel1);
            Controls.Add(menuStrip2);
            Margin = new Padding(3, 4, 3, 4);
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
        private ToolStripMenuItem UsunKontoToolStripMenuItem;
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
