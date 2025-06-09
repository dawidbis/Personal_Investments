




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
            generujRaportToolStripMenuItem = new ToolStripMenuItem();
            eksportujDaneToolStripMenuItem = new ToolStripMenuItem();
            importujDaneToolStripMenuItem = new ToolStripMenuItem();
            UsunKontoToolStripMenuItem = new ToolStripMenuItem();
            wylogujToolStripMenuItem1 = new ToolStripMenuItem();
            sprzedajToolStripMenuItem = new ToolStripMenuItem();
            panelUser = new Panel();
            labelWelcome = new Label();
            labelBilans=new Label();
            labelBilansAktualny =new Label();
            panelMain = new Panel();
            groupBox1 = new GroupBox();
            listView1 = new ListView();
            imageList1 = new ImageList(components);
            btnOdswiez= new Button();
            btnHistoria = new Button();
            btnAktualne = new Button();

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
            menuStrip2.Size = new Size(1000, 555); // większa wysokość

            menuStrip2.Items.AddRange(new ToolStripItem[] {
            inwestycjePersonalneToolStripMenuItem,
            generujRaportToolStripMenuItem,    
            eksportujDaneToolStripMenuItem,
            importujDaneToolStripMenuItem,
            UsunKontoToolStripMenuItem,
            wylogujToolStripMenuItem1,
            sprzedajToolStripMenuItem
            });
                inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";
                inwestycjePersonalneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                akcjaToolStripMenuItem, obligacjaToolStripMenuItem,
                kryptowalutaToolStripMenuItem, surowiecToolStripMenuItem
            });

            akcjaToolStripMenuItem.Text = "Akcja";
            akcjaToolStripMenuItem.Click += akcjaToolStripMenuItem_Click;
            kryptowalutaToolStripMenuItem.Click += kryptowalutaToolStripMenuItem_Click;
            obligacjaToolStripMenuItem.Text = "Obligacja";
            kryptowalutaToolStripMenuItem.Text = "Kryptowaluta";
            surowiecToolStripMenuItem.Text = "Surowiec";
            surowiecToolStripMenuItem.Click += surowiecToolStripMenuItem_Click;

            generujRaportToolStripMenuItem.Text = "Generuj raport";
            generujRaportToolStripMenuItem.Click += generujRaportToolStripMenuItem_Click;
            eksportujDaneToolStripMenuItem.Text = "Eksportuj dane";
            eksportujDaneToolStripMenuItem.Click += eksportujDaneToolStripMenuItem_Click;
            importujDaneToolStripMenuItem.Text = "Importuj dane";
            importujDaneToolStripMenuItem.Click += importujDaneToolStripMenuItem_Click;
            UsunKontoToolStripMenuItem.Text = "Usuń konto";
            UsunKontoToolStripMenuItem.Click += UsunKontoToolStripMenuItem_Click;
            wylogujToolStripMenuItem1.Text = "Wyloguj";
            wylogujToolStripMenuItem1.Click += wylogujToolStripMenuItem1_Click;
            sprzedajToolStripMenuItem.Text = "Sprzedaj zaznaczoną inwestycję";
            sprzedajToolStripMenuItem.Click += sprzedajToolStripMenuItem_Click;
          

            // Ustawienia padding i kolory dla elementów menu i podmenu
            foreach (ToolStripMenuItem parent in menuStrip2.Items.OfType<ToolStripMenuItem>())
            {
                parent.Padding = new Padding(15, 10, 15, 10);
                parent.ForeColor = Color.White;
                parent.Margin = new Padding(10, 0, 0, 0); // przesunięcie w dół'

                foreach (ToolStripItem subItem in parent.DropDownItems)
                {
                    subItem.BackColor = Color.FromArgb(25, 25, 35);
                    subItem.ForeColor = Color.White;
                    subItem.Padding = new Padding(6, 7, 6, 7); // większy padding góra-dół
                    subItem.Height = 25;   
                    subItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    subItem.MouseEnter += (s, e) => { menuStrip2.Cursor = Cursors.Hand; };
                    subItem.MouseLeave += (s, e) => { menuStrip2.Cursor = Cursors.Default; };
                }
            }

            // panelUser
            panelUser.Dock = DockStyle.Left;
            panelUser.Width = 300;
            panelUser.BackColor = Color.FromArgb(10, 10, 10);
            panelUser.Controls.Add(labelWelcome);
            panelUser.Controls.Add(labelBilans);

            labelWelcome.ForeColor = Color.White;
            labelWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelWelcome.Location = new Point(10, 50);
            labelWelcome.AutoSize = true;
            labelWelcome.Text = "Cześć, USER!";

            labelBilans.ForeColor = Color.White;
            labelBilans.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelBilans.Location = new Point(10, 80);
            labelBilans.AutoSize = true;
            labelBilans.Text = "Bilans ogólny: BILANS";


            labelBilansAktualny.ForeColor = Color.White;
            labelBilansAktualny.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelBilansAktualny.Location = new Point(10, 110);
            labelBilansAktualny.AutoSize = true;
            labelBilansAktualny.Text = "Bilans aktualny: BILANS";

            checkBoxTrybTestowy = new CheckBox();
            checkBoxTrybTestowy.ForeColor = Color.White;
            checkBoxTrybTestowy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            checkBoxTrybTestowy.Location = new Point(10, 150);
            checkBoxTrybTestowy.AutoSize = true;
            checkBoxTrybTestowy.Text = "Tryb testowy";
            checkBoxTrybTestowy.CheckedChanged += checkBoxTrybTestowy_CheckedChanged;
            checkBoxTrybTestowy.Visible = false;
            panelUser.Controls.Add(checkBoxTrybTestowy);

            // Etykieta do pola aktualnej ceny
            labelTestPrice = new Label();
            labelTestPrice.ForeColor = Color.White;
            labelTestPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            labelTestPrice.Location = new Point(10, 180);
            labelTestPrice.AutoSize = true;
            labelTestPrice.Text = "Aktualna cena (test):";
            labelTestPrice.Visible = false;
            panelUser.Controls.Add(labelTestPrice);

            // Pole tekstowe do wpisania aktualnej ceny
            textBoxAktualnaCenaTest = new TextBox(); // zadeklaruj wcześniej jako pole klasy
            textBoxAktualnaCenaTest.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            textBoxAktualnaCenaTest.Location = new Point(10, 200);
            textBoxAktualnaCenaTest.Width = 160;
            textBoxAktualnaCenaTest.Visible = false;
            panelUser.Controls.Add(textBoxAktualnaCenaTest);

            // panelMain
            panelMain.Dock = DockStyle.Fill;
            panelMain.BackColor = Color.FromArgb(25, 25, 35);
            panelMain.Controls.Add(groupBox1);
            panelMain.Controls.Add(btnOdswiez);
            panelMain.Controls.Add(btnHistoria);
            panelMain.Controls.Add(btnAktualne);

            labelRaport = new Label()
            {
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 340), // pod labelBilans (który jest na 80)
                AutoSize = true,
                Visible = false // domyślnie niewidoczny
            };

            panelUser.Controls.Add(labelRaport);
            panelUser.Controls.Add(labelBilansAktualny);

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
            btnOdswiez.Location = new Point(350, 500);
            btnOdswiez.Size = new Size(150, 40);
            btnOdswiez.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnOdswiez.BackColor = Color.FromArgb(100, 40, 160);
            btnOdswiez.ForeColor = Color.White;
            btnOdswiez.FlatStyle = FlatStyle.Flat;
            btnOdswiez.FlatAppearance.BorderColor = Color.White;
            btnOdswiez.FlatAppearance.BorderSize = 1;
            btnOdswiez.Click += btnOdswiez_Click;

            btnHistoria.Text = "Historia sprzedaży";
            btnHistoria.Location = new Point(190, 500);
            btnHistoria.Size = new Size(150, 40);
            btnHistoria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHistoria.BackColor = Color.FromArgb(100, 40, 160);
            btnHistoria.ForeColor = Color.White;
            btnHistoria.FlatStyle = FlatStyle.Flat;
            btnHistoria.FlatAppearance.BorderColor = Color.White;
            btnHistoria.FlatAppearance.BorderSize = 1;
            btnHistoria.Click += btnHistoria_Click;

            btnAktualne.Text = "Twoje inwestycje";
            btnAktualne.Location = new Point(30, 500);
            btnAktualne.Size = new Size(150, 40);
            btnAktualne.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAktualne.BackColor = Color.FromArgb(100, 40, 160);
            btnAktualne.ForeColor = Color.White;
            btnAktualne.FlatStyle = FlatStyle.Flat;
            btnAktualne.FlatAppearance.BorderColor = Color.White;
            btnAktualne.FlatAppearance.BorderSize = 1;
            btnAktualne.Click += btnAktualne_Click;

            // MainWindow
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = true;
            ClientSize = new Size(1400, 640);
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
        private ToolStripMenuItem generujRaportToolStripMenuItem;
        private ToolStripMenuItem eksportujDaneToolStripMenuItem;
        private ToolStripMenuItem importujDaneToolStripMenuItem;
        private ToolStripMenuItem UsunKontoToolStripMenuItem;
        private ToolStripMenuItem wylogujToolStripMenuItem1;
        private ToolStripMenuItem sprzedajToolStripMenuItem;
        private Panel panelUser;
        private Label labelWelcome;
        private Label labelBilansAktualny;
        private Label labelBilans;
        private Panel panelMain;
        private GroupBox groupBox1;
        private ListView listView1;
        private ImageList imageList1;
        private Button btnOdswiez;
        private Button btnHistoria;
        private Button btnAktualne;
        private CheckBox checkBoxTrybTestowy;
        private TextBox textBoxAktualnaCenaTest;
        private Label labelTestPrice;
        private Label labelRaport;

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
