﻿namespace Personal_Investment_App
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
            panelUser = new Panel();
            labelWelcome = new Label();
            panelMain = new Panel();
            groupBox1 = new GroupBox();
            listView1 = new ListView();
            imageList1 = new ImageList(components);
            btnOdswiez = new Button();

            menuStrip2.SuspendLayout();
            panelUser.SuspendLayout();
            panelMain.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();

            // menuStrip2

            menuStrip2.BackColor = Color.Black;
            menuStrip2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            menuStrip2.ForeColor = Color.White;
            menuStrip2.RenderMode = ToolStripRenderMode.Professional;
            menuStrip2.Renderer = new DarkToolStripRenderer();

            // Zwiększamy wysokość i padding menuStrip2
            menuStrip2.Padding = new Padding(15, 12, 15, 12);
            menuStrip2.Size = new Size(1000, 55); // większa wysokość

            menuStrip2.Items.AddRange(new ToolStripItem[] {
        inwestycjePersonalneToolStripMenuItem,
        generujRaportToolStripMenuItem,
        eksportujDaneToolStripMenuItem,
        UsunKontoToolStripMenuItem,
        wylogujToolStripMenuItem1
    });

            inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";

            inwestycjePersonalneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
        akcjaToolStripMenuItem, obligacjaToolStripMenuItem,
        kryptowalutaToolStripMenuItem, surowiecToolStripMenuItem, eTFToolStripMenuItem
    });

            akcjaToolStripMenuItem.Text = "Akcja";
            akcjaToolStripMenuItem.Click += akcjaToolStripMenuItem_Click;
            obligacjaToolStripMenuItem.Text = "Obligacja";
            kryptowalutaToolStripMenuItem.Text = "Kryptowaluta";
            surowiecToolStripMenuItem.Text = "Surowiec";
            eTFToolStripMenuItem.Text = "ETF";

            generujRaportToolStripMenuItem.Text = "Generuj raport";
            eksportujDaneToolStripMenuItem.Text = "Eksportuj dane";
            UsunKontoToolStripMenuItem.Text = "Usuń konto";
            UsunKontoToolStripMenuItem.Click += UsunKontoToolStripMenuItem_Click;
            wylogujToolStripMenuItem1.Text = "Wyloguj";
            wylogujToolStripMenuItem1.Click += wylogujToolStripMenuItem1_Click;

            // Ustawienia padding i kolory dla elementów menu i podmenu
            foreach (ToolStripMenuItem parent in menuStrip2.Items.OfType<ToolStripMenuItem>())
            {
                // większy padding i biały tekst dla głównego menu
                parent.Padding = new Padding(15, 10, 15, 10);
                parent.ForeColor = Color.White;

                foreach (ToolStripItem subItem in parent.DropDownItems)
                {
                    subItem.BackColor = Color.FromArgb(25, 25, 35);
                    subItem.ForeColor = Color.White; // tekst w podmenu na biało
                    subItem.Padding = new Padding(20, 10, 20, 10); // większy padding podmenu
                    subItem.Height = 40; // zwiększona wysokość elementów podmenu
                }
            }

            // panelUser
            panelUser.Dock = DockStyle.Left;
            panelUser.Width = 200;
            panelUser.BackColor = Color.FromArgb(10, 10, 10);
            panelUser.Controls.Add(labelWelcome);

            labelWelcome.ForeColor = Color.White;
            labelWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelWelcome.Location = new Point(10, 50);
            labelWelcome.AutoSize = true;
            labelWelcome.Text = "Cześć, USER!";

            // panelMain
            panelMain.Dock = DockStyle.Fill;
            panelMain.BackColor = Color.FromArgb(25, 25, 35);
            panelMain.Controls.Add(groupBox1);
            panelMain.Controls.Add(btnOdswiez);

            groupBox1.Dock = DockStyle.Top;
            groupBox1.Height = 480;
            groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox1.ForeColor = Color.White;
            groupBox1.BackColor = Color.FromArgb(35, 0, 55);
            groupBox1.Text = "Twoje inwestycje";
            groupBox1.Padding = new Padding(20);
            groupBox1.Controls.Add(listView1);

            listView1.Dock = DockStyle.Fill;
            listView1.BackColor = Color.Black;
            listView1.ForeColor = Color.White;
            listView1.Font = new Font("Segoe UI", 10F);
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.SmallImageList = imageList1;
            listView1.OwnerDraw = false;

            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "bitcoin.png");
            imageList1.Images.SetKeyName(1, "graph.png");
            imageList1.Images.SetKeyName(2, "business.png");
            imageList1.Images.SetKeyName(3, "bars.png");

            btnOdswiez.Text = "Odśwież dane";
            btnOdswiez.Location = new Point(30, 500);
            btnOdswiez.Size = new Size(150, 40);
            btnOdswiez.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOdswiez.BackColor = Color.FromArgb(100, 40, 160);
            btnOdswiez.ForeColor = Color.White;
            btnOdswiez.FlatStyle = FlatStyle.Flat;
            btnOdswiez.FlatAppearance.BorderColor = Color.White;
            btnOdswiez.FlatAppearance.BorderSize = 1;
            btnOdswiez.Click += btnOdswiez_Click;

            // MainWindow
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = true;
            ClientSize = new Size(1154, 640);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Personal Investments";
            Controls.Add(panelMain);
            Controls.Add(panelUser);
            Controls.Add(menuStrip2);
            MainMenuStrip = menuStrip2;

            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            panelUser.ResumeLayout(false);
            panelUser.PerformLayout();
            panelMain.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip2;
        private ToolStripMenuItem inwestycjePersonalneToolStripMenuItem;
        private ToolStripMenuItem akcjaToolStripMenuItem;
        private ToolStripMenuItem obligacjaToolStripMenuItem;
        private ToolStripMenuItem kryptowalutaToolStripMenuItem;
        private ToolStripMenuItem surowiecToolStripMenuItem;
        private ToolStripMenuItem eTFToolStripMenuItem;
        private ToolStripMenuItem generujRaportToolStripMenuItem;
        private ToolStripMenuItem eksportujDaneToolStripMenuItem;
        private ToolStripMenuItem UsunKontoToolStripMenuItem;
        private ToolStripMenuItem wylogujToolStripMenuItem1;
        private Panel panelUser;
        private Label labelWelcome;
        private Panel panelMain;
        private GroupBox groupBox1;
        private ListView listView1;
        private ImageList imageList1;
        private Button btnOdswiez;

        public class DarkToolStripRenderer : ToolStripProfessionalRenderer
        {
            public DarkToolStripRenderer() : base(new DarkColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(80, 0, 130)), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(25, 25, 35)), e.Item.ContentRectangle);
                }
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(80, 0, 130)), 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            }
        }

        public class DarkColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Color.FromArgb(25, 25, 35);
            public override Color MenuItemSelected => Color.FromArgb(80, 0, 130);
            public override Color MenuItemBorder => Color.FromArgb(80, 0, 130);
            public override Color ImageMarginGradientBegin => Color.FromArgb(25, 25, 35);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(25, 25, 35);
            public override Color ImageMarginGradientEnd => Color.FromArgb(25, 25, 35);
        }
    }
}
