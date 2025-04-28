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
            walutaToolStripMenuItem = new ToolStripMenuItem();
            euroToolStripMenuItem = new ToolStripMenuItem();
            dolarToolStripMenuItem = new ToolStripMenuItem();
            złotyToolStripMenuItem = new ToolStripMenuItem();
            funtyToolStripMenuItem = new ToolStripMenuItem();
            dezaktywujInwestycjęToolStripMenuItem = new ToolStripMenuItem();
            wylogujToolStripMenuItem1 = new ToolStripMenuItem();
            panel1 = new Panel();
            groupBox3 = new GroupBox();
            listBox2 = new ListBox();
            groupBox2 = new GroupBox();
            lblśredniZysk = new Label();
            label6 = new Label();
            lblłącznaWartość = new Label();
            label4 = new Label();
            button1 = new Button();
            label3 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            listView1 = new ListView();
            Typ_inwestycji = new ColumnHeader();
            Nazwa_Inwestycji = new ColumnHeader();
            Data_zakupu = new ColumnHeader();
            Kwota_inwestycji = new ColumnHeader();
            Obecna_wartość = new ColumnHeader();
            imageList1 = new ImageList(components);
            label1 = new Label();
            lblilośćInwestycji = new Label();
            menuStrip2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = SystemColors.ActiveBorder;
            menuStrip2.Dock = DockStyle.Left;
            menuStrip2.Items.AddRange(new ToolStripItem[] { inwestycjePersonalneToolStripMenuItem, sprzedajInwestycjęToolStripMenuItem, toolStripMenuItem1, generujRaportToolStripMenuItem, eksportujDaneToolStripMenuItem, walutaToolStripMenuItem, dezaktywujInwestycjęToolStripMenuItem, wylogujToolStripMenuItem1 });
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
            // walutaToolStripMenuItem
            // 
            walutaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { euroToolStripMenuItem, dolarToolStripMenuItem, złotyToolStripMenuItem, funtyToolStripMenuItem });
            walutaToolStripMenuItem.Name = "walutaToolStripMenuItem";
            walutaToolStripMenuItem.Size = new Size(114, 19);
            walutaToolStripMenuItem.Text = "Waluta";
            // 
            // euroToolStripMenuItem
            // 
            euroToolStripMenuItem.Name = "euroToolStripMenuItem";
            euroToolStripMenuItem.Size = new Size(104, 22);
            euroToolStripMenuItem.Text = "Euro";
            // 
            // dolarToolStripMenuItem
            // 
            dolarToolStripMenuItem.Name = "dolarToolStripMenuItem";
            dolarToolStripMenuItem.Size = new Size(104, 22);
            dolarToolStripMenuItem.Text = "Dolar";
            // 
            // złotyToolStripMenuItem
            // 
            złotyToolStripMenuItem.Name = "złotyToolStripMenuItem";
            złotyToolStripMenuItem.Size = new Size(104, 22);
            złotyToolStripMenuItem.Text = "Złoty";
            // 
            // funtyToolStripMenuItem
            // 
            funtyToolStripMenuItem.Name = "funtyToolStripMenuItem";
            funtyToolStripMenuItem.Size = new Size(104, 22);
            funtyToolStripMenuItem.Text = "Funty";
            // 
            // dezaktywujInwestycjęToolStripMenuItem
            // 
            dezaktywujInwestycjęToolStripMenuItem.Name = "dezaktywujInwestycjęToolStripMenuItem";
            dezaktywujInwestycjęToolStripMenuItem.Size = new Size(114, 19);
            dezaktywujInwestycjęToolStripMenuItem.Text = "Dezaktywuj konto";
            // 
            // wylogujToolStripMenuItem1
            // 
            wylogujToolStripMenuItem1.Name = "wylogujToolStripMenuItem1";
            wylogujToolStripMenuItem1.Size = new Size(114, 19);
            wylogujToolStripMenuItem1.Text = "Wyloguj";
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(117, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(682, 450);
            panel1.TabIndex = 2;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(listBox2);
            groupBox3.Location = new Point(13, 331);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(524, 116);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "Szczególy wybranej inwestycji";
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(6, 21);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(500, 79);
            listBox2.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblilośćInwestycji);
            groupBox2.Controls.Add(lblśredniZysk);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(lblłącznaWartość);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(537, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(145, 444);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            // 
            // lblśredniZysk
            // 
            lblśredniZysk.AutoSize = true;
            lblśredniZysk.Location = new Point(24, 53);
            lblśredniZysk.Name = "lblśredniZysk";
            lblśredniZysk.Size = new Size(86, 15);
            lblśredniZysk.TabIndex = 6;
            lblśredniZysk.Text = "Średni zysk (%)";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(79, 108);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 5;
            // 
            // lblłącznaWartość
            // 
            lblłącznaWartość.AutoSize = true;
            lblłącznaWartość.Location = new Point(0, 38);
            lblłącznaWartość.Name = "lblłącznaWartość";
            lblłącznaWartość.Size = new Size(145, 15);
            lblłącznaWartość.TabIndex = 4;
            lblłącznaWartość.Text = "Łączna wartość inwestycji:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 19);
            label4.Name = "label4";
            label4.Size = new Size(98, 15);
            label4.TabIndex = 3;
            label4.Text = "Szybkie statystyki";
            // 
            // button1
            // 
            button1.Location = new Point(6, 378);
            button1.Name = "button1";
            button1.Size = new Size(127, 23);
            button1.TabIndex = 2;
            button1.Text = "Sprawdź stan Konta";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(62, 360);
            label3.Name = "label3";
            label3.Size = new Size(13, 15);
            label3.TabIndex = 1;
            label3.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 345);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 0;
            label2.Text = "Stan Konta:";
            label2.Click += label2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listView1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(13, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(524, 322);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { Typ_inwestycji, Nazwa_Inwestycji, Data_zakupu, Kwota_inwestycji, Obecna_wartość });
            listView1.Location = new Point(6, 38);
            listView1.Name = "listView1";
            listView1.Size = new Size(512, 278);
            listView1.SmallImageList = imageList1;
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(194, 19);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 1;
            label1.Text = "Posiadane inwestycje";
            label1.Click += label1_Click;
            // 
            // lblilośćInwestycji
            // 
            lblilośćInwestycji.AutoSize = true;
            lblilośćInwestycji.Location = new Point(21, 68);
            lblilośćInwestycji.Name = "lblilośćInwestycji";
            lblilośćInwestycji.Size = new Size(89, 15);
            lblilośćInwestycji.TabIndex = 7;
            lblilośćInwestycji.Text = "Ilość inwestycji:";
            lblilośćInwestycji.Click += lblilośćInwestycji_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(menuStrip2);
            Name = "Form1";
            Text = "Form1";
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            panel1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private Label label1;
        private ToolStripMenuItem walutaToolStripMenuItem;
        private ToolStripMenuItem euroToolStripMenuItem;
        private ToolStripMenuItem dolarToolStripMenuItem;
        private ToolStripMenuItem złotyToolStripMenuItem;
        private ToolStripMenuItem funtyToolStripMenuItem;
        private Label label3;
        private Label label2;
        private Button button1;
        private ToolStripMenuItem sprzedajInwestycjęToolStripMenuItem;
        private ListView listView1;
        private ImageList imageList1;
        private ToolStripMenuItem eksportujDaneToolStripMenuItem;
        private GroupBox groupBox2;
        private ColumnHeader Typ_inwestycji;
        private ColumnHeader Nazwa_Inwestycji;
        private ColumnHeader Data_zakupu;
        private ColumnHeader Kwota_inwestycji;
        private ColumnHeader Obecna_wartość;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private ListBox listBox2;
        private Label lblśredniZysk;
        private Label label6;
        private Label lblłącznaWartość;
        private Label label4;
        private Label lblilośćInwestycji;
    }
}
