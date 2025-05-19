namespace Personal_Investment_App
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            menuStrip2 = new MenuStrip();
            inwestycjePersonalneToolStripMenuItem = new ToolStripMenuItem();
            akcjaToolStripMenuItem = new ToolStripMenuItem();
            obligacjaToolStripMenuItem = new ToolStripMenuItem();
            kryptowalutaToolStripMenuItem = new ToolStripMenuItem();
            surowiecToolStripMenuItem = new ToolStripMenuItem();
            eTFToolStripMenuItem = new ToolStripMenuItem();
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
            menuStrip2.Items.AddRange(new ToolStripItem[] { inwestycjePersonalneToolStripMenuItem, generujRaportToolStripMenuItem, eksportujDaneToolStripMenuItem, UsunKontoToolStripMenuItem, wylogujToolStripMenuItem1 });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(800, 34);
            menuStrip2.TabIndex = 1;
            menuStrip2.Text = "menuStrip2";
            // 
            // inwestycjePersonalneToolStripMenuItem
            // 
            inwestycjePersonalneToolStripMenuItem.AutoSize = false;
            inwestycjePersonalneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { akcjaToolStripMenuItem, obligacjaToolStripMenuItem, kryptowalutaToolStripMenuItem, surowiecToolStripMenuItem, eTFToolStripMenuItem });
            inwestycjePersonalneToolStripMenuItem.Name = "inwestycjePersonalneToolStripMenuItem";
            inwestycjePersonalneToolStripMenuItem.Size = new Size(140, 30);
            inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";
            inwestycjePersonalneToolStripMenuItem.Click += inwestycjePersonalneToolStripMenuItem_Click;
            // 
            // akcjaToolStripMenuItem
            // 
            akcjaToolStripMenuItem.Name = "akcjaToolStripMenuItem";
            akcjaToolStripMenuItem.Size = new Size(144, 22);
            akcjaToolStripMenuItem.Text = "Akcja";
            akcjaToolStripMenuItem.Click += akcjaToolStripMenuItem_Click;
            // 
            // obligacjaToolStripMenuItem
            // 
            obligacjaToolStripMenuItem.Name = "obligacjaToolStripMenuItem";
            obligacjaToolStripMenuItem.Size = new Size(144, 22);
            obligacjaToolStripMenuItem.Text = "Obligacja";
            // 
            // kryptowalutaToolStripMenuItem
            // 
            kryptowalutaToolStripMenuItem.Name = "kryptowalutaToolStripMenuItem";
            kryptowalutaToolStripMenuItem.Size = new Size(144, 22);
            kryptowalutaToolStripMenuItem.Text = "Kryptowaluta";
            // 
            // surowiecToolStripMenuItem
            // 
            surowiecToolStripMenuItem.Name = "surowiecToolStripMenuItem";
            surowiecToolStripMenuItem.Size = new Size(144, 22);
            surowiecToolStripMenuItem.Text = "Surowiec";
            // 
            // eTFToolStripMenuItem
            // 
            eTFToolStripMenuItem.Name = "eTFToolStripMenuItem";
            eTFToolStripMenuItem.Size = new Size(144, 22);
            eTFToolStripMenuItem.Text = "ETF";
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
            panel1.Location = new Point(127, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(247, 178);
            panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(listView1);
            groupBox1.Location = new Point(3, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(673, 438);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { Typ_inwestycji, Nazwa_Inwestycji, Data_zakupu, Kwota_inwestycji, Obecna_wartość });
            listView1.Location = new Point(3, 17);
            listView1.Name = "listView1";
            listView1.Size = new Size(667, 416);
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(menuStrip2);
            Name = "MainWindow";
            Text = "Personal Investments";
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
        private ImageList imageList1;
        private ToolStripMenuItem eksportujDaneToolStripMenuItem;
        private GroupBox groupBox1;
        private ListView listView1;
        private ColumnHeader Typ_inwestycji;
        private ColumnHeader Nazwa_Inwestycji;
        private ColumnHeader Data_zakupu;
        private ColumnHeader Kwota_inwestycji;
        private ColumnHeader Obecna_wartość;
        private ToolStripMenuItem akcjaToolStripMenuItem;
        private ToolStripMenuItem obligacjaToolStripMenuItem;
        private ToolStripMenuItem kryptowalutaToolStripMenuItem;
        private ToolStripMenuItem surowiecToolStripMenuItem;
        private ToolStripMenuItem eTFToolStripMenuItem;
    }
}
