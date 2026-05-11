




namespace Personal_Investment.UI.Forms;
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
        TextBox textBoxAktualnaCenaTest;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
        menuStrip2 = new MenuStrip();
        inwestycjePersonalneToolStripMenuItem = new ToolStripMenuItem();
        akcjaToolStripMenuItem = new ToolStripMenuItem();
        kryptowalutaToolStripMenuItem = new ToolStripMenuItem();
        surowiecToolStripMenuItem = new ToolStripMenuItem();
        sprzedajToolStripMenuItem = new ToolStripMenuItem();
        spacerLeft = new ToolStripMenuItem();
        generujRaportToolStripMenuItem = new ToolStripMenuItem();
        eksportujDaneToolStripMenuItem = new ToolStripMenuItem();
        importujDaneToolStripMenuItem = new ToolStripMenuItem();
        UsunKontoToolStripMenuItem = new ToolStripMenuItem();
        wylogujToolStripMenuItem1 = new ToolStripMenuItem();
        toolStripMenuItem1 = new ToolStripMenuItem();
        spacerRight = new ToolStripMenuItem();
        panelUser = new Panel();
        labelWelcome = new Label();
        labelBilans = new Label();
        checkBoxTrybTestowy = new CheckBox();
        labelTestPrice = new Label();
        labelRaport = new Label();
        labelBilansAktualny = new Label();
        panelMain = new Panel();
        groupBox1 = new GroupBox();
        listView1 = new ListView();
        imageList1 = new ImageList(components);
        btnOdswiez = new Button();
        btnHistoria = new Button();
        btnAktualne = new Button();
        textBoxAktualnaCenaTest = new TextBox();
        menuStrip2.SuspendLayout();
        panelUser.SuspendLayout();
        panelMain.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // textBoxAktualnaCenaTest
        // 
        textBoxAktualnaCenaTest.BackColor = Color.FromArgb(30, 30, 30);
        textBoxAktualnaCenaTest.BorderStyle = BorderStyle.FixedSingle;
        textBoxAktualnaCenaTest.Font = new Font("Segoe UI", 9F);
        textBoxAktualnaCenaTest.ForeColor = Color.White;
        textBoxAktualnaCenaTest.Location = new Point(30, 223);
        textBoxAktualnaCenaTest.Name = "textBoxAktualnaCenaTest";
        textBoxAktualnaCenaTest.Size = new Size(176, 27);
        textBoxAktualnaCenaTest.TabIndex = 4;
        textBoxAktualnaCenaTest.Visible = false;
        // 
        // menuStrip2
        // 
        menuStrip2.BackColor = Color.FromArgb(10, 10, 10);
        menuStrip2.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
        menuStrip2.ForeColor = Color.White;
        menuStrip2.ImageScalingSize = new Size(20, 20);
        menuStrip2.Items.AddRange(new ToolStripItem[] { inwestycjePersonalneToolStripMenuItem, sprzedajToolStripMenuItem, spacerLeft, generujRaportToolStripMenuItem, eksportujDaneToolStripMenuItem, importujDaneToolStripMenuItem, UsunKontoToolStripMenuItem, wylogujToolStripMenuItem1, toolStripMenuItem1, spacerRight });
        menuStrip2.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
        menuStrip2.Location = new Point(0, 0);
        menuStrip2.Name = "menuStrip2";
        menuStrip2.Padding = new Padding(6, 3, 0, 3);
        menuStrip2.RenderMode = ToolStripRenderMode.Professional;
        menuStrip2.Size = new Size(1482, 45);
        menuStrip2.TabIndex = 2;
        // 
        // inwestycjePersonalneToolStripMenuItem
        // 
        inwestycjePersonalneToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        inwestycjePersonalneToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { akcjaToolStripMenuItem, kryptowalutaToolStripMenuItem, surowiecToolStripMenuItem });
        inwestycjePersonalneToolStripMenuItem.ForeColor = SystemColors.ButtonFace;
        inwestycjePersonalneToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        inwestycjePersonalneToolStripMenuItem.Name = "inwestycjePersonalneToolStripMenuItem";
        inwestycjePersonalneToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        inwestycjePersonalneToolStripMenuItem.Size = new Size(169, 39);
        inwestycjePersonalneToolStripMenuItem.Text = "Dodaj inwestycję";
        // 
        // akcjaToolStripMenuItem
        // 
        akcjaToolStripMenuItem.Name = "akcjaToolStripMenuItem";
        akcjaToolStripMenuItem.Size = new Size(202, 30);
        akcjaToolStripMenuItem.Text = "Akcja";
        akcjaToolStripMenuItem.Click += akcjaToolStripMenuItem_Click;
        // 
        // kryptowalutaToolStripMenuItem
        // 
        kryptowalutaToolStripMenuItem.Name = "kryptowalutaToolStripMenuItem";
        kryptowalutaToolStripMenuItem.Size = new Size(202, 30);
        kryptowalutaToolStripMenuItem.Text = "Kryptowaluta";
        kryptowalutaToolStripMenuItem.Click += kryptowalutaToolStripMenuItem_Click;
        // 
        // surowiecToolStripMenuItem
        // 
        surowiecToolStripMenuItem.Name = "surowiecToolStripMenuItem";
        surowiecToolStripMenuItem.Size = new Size(202, 30);
        surowiecToolStripMenuItem.Text = "Surowiec";
        surowiecToolStripMenuItem.Click += surowiecToolStripMenuItem_Click;
        // 
        // sprzedajToolStripMenuItem
        // 
        sprzedajToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        sprzedajToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        sprzedajToolStripMenuItem.Name = "sprzedajToolStripMenuItem";
        sprzedajToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        sprzedajToolStripMenuItem.Size = new Size(284, 39);
        sprzedajToolStripMenuItem.Text = "Sprzedaj zaznaczoną inwestycję";
        sprzedajToolStripMenuItem.Click += sprzedajToolStripMenuItem_Click;
        // 
        // spacerLeft
        // 
        spacerLeft.Enabled = false;
        spacerLeft.ForeColor = Color.FromArgb(10, 10, 10);
        spacerLeft.Name = "spacerLeft";
        spacerLeft.Size = new Size(14, 39);
        // 
        // generujRaportToolStripMenuItem
        // 
        generujRaportToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        generujRaportToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        generujRaportToolStripMenuItem.Name = "generujRaportToolStripMenuItem";
        generujRaportToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        generujRaportToolStripMenuItem.Size = new Size(150, 39);
        generujRaportToolStripMenuItem.Text = "Generuj raport";
        generujRaportToolStripMenuItem.Click += generujRaportToolStripMenuItem_Click;
        // 
        // eksportujDaneToolStripMenuItem
        // 
        eksportujDaneToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        eksportujDaneToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        eksportujDaneToolStripMenuItem.Name = "eksportujDaneToolStripMenuItem";
        eksportujDaneToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        eksportujDaneToolStripMenuItem.Size = new Size(154, 39);
        eksportujDaneToolStripMenuItem.Text = "Eksportuj dane";
        eksportujDaneToolStripMenuItem.Click += eksportujDaneToolStripMenuItem_Click;
        // 
        // importujDaneToolStripMenuItem
        // 
        importujDaneToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        importujDaneToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        importujDaneToolStripMenuItem.Name = "importujDaneToolStripMenuItem";
        importujDaneToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        importujDaneToolStripMenuItem.Size = new Size(149, 39);
        importujDaneToolStripMenuItem.Text = "Importuj dane";
        importujDaneToolStripMenuItem.Click += importujDaneToolStripMenuItem_Click;
        // 
        // UsunKontoToolStripMenuItem
        // 
        UsunKontoToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
        UsunKontoToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
        UsunKontoToolStripMenuItem.Margin = new Padding(5, 0, 5, 0);
        UsunKontoToolStripMenuItem.Name = "UsunKontoToolStripMenuItem";
        UsunKontoToolStripMenuItem.Padding = new Padding(10, 5, 10, 5);
        UsunKontoToolStripMenuItem.Size = new Size(128, 39);
        UsunKontoToolStripMenuItem.Text = "Usuń konto";
        UsunKontoToolStripMenuItem.Click += UsunKontoToolStripMenuItem_Click;
        // 
        // wylogujToolStripMenuItem1
        // 
        wylogujToolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
        wylogujToolStripMenuItem1.DisplayStyle = ToolStripItemDisplayStyle.Text;
        wylogujToolStripMenuItem1.Margin = new Padding(5, 0, 5, 0);
        wylogujToolStripMenuItem1.Name = "wylogujToolStripMenuItem1";
        wylogujToolStripMenuItem1.Padding = new Padding(10, 5, 10, 5);
        wylogujToolStripMenuItem1.Size = new Size(102, 39);
        wylogujToolStripMenuItem1.Text = "Wyloguj";
        wylogujToolStripMenuItem1.Click += wylogujToolStripMenuItem1_Click;
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new Size(31, 39);
        toolStripMenuItem1.Text = " ";
        // 
        // spacerRight
        // 
        spacerRight.Enabled = false;
        spacerRight.ForeColor = Color.FromArgb(10, 10, 10);
        spacerRight.Name = "spacerRight";
        spacerRight.Size = new Size(14, 39);
        // 
        // panelUser
        // 
        panelUser.BackColor = Color.FromArgb(15, 15, 15);
        panelUser.Controls.Add(labelWelcome);
        panelUser.Controls.Add(labelBilans);
        panelUser.Controls.Add(checkBoxTrybTestowy);
        panelUser.Controls.Add(labelTestPrice);
        panelUser.Controls.Add(textBoxAktualnaCenaTest);
        panelUser.Controls.Add(labelRaport);
        panelUser.Controls.Add(labelBilansAktualny);
        panelUser.Dock = DockStyle.Left;
        panelUser.Location = new Point(0, 45);
        panelUser.Name = "panelUser";
        panelUser.Padding = new Padding(20, 20, 0, 0);
        panelUser.Size = new Size(350, 708);
        panelUser.TabIndex = 1;
        // 
        // labelWelcome
        // 
        labelWelcome.AutoSize = true;
        labelWelcome.BackColor = Color.Transparent;
        labelWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        labelWelcome.ForeColor = Color.White;
        labelWelcome.Location = new Point(30, 70);
        labelWelcome.Name = "labelWelcome";
        labelWelcome.Size = new Size(131, 28);
        labelWelcome.TabIndex = 0;
        labelWelcome.Text = "Cześć, USER!";
        // 
        // labelBilans
        // 
        labelBilans.AutoSize = true;
        labelBilans.BackColor = Color.Transparent;
        labelBilans.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
        labelBilans.ForeColor = Color.White;
        labelBilans.Location = new Point(30, 100);
        labelBilans.Name = "labelBilans";
        labelBilans.Size = new Size(176, 23);
        labelBilans.TabIndex = 1;
        labelBilans.Text = "Bilans ogólny: BILANS";
        // 
        // checkBoxTrybTestowy
        // 
        checkBoxTrybTestowy.AutoSize = true;
        checkBoxTrybTestowy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        checkBoxTrybTestowy.ForeColor = Color.White;
        checkBoxTrybTestowy.Location = new Point(30, 170);
        checkBoxTrybTestowy.Name = "checkBoxTrybTestowy";
        checkBoxTrybTestowy.Size = new Size(137, 27);
        checkBoxTrybTestowy.TabIndex = 2;
        checkBoxTrybTestowy.Text = "Tryb testowy";
        checkBoxTrybTestowy.Visible = false;
        checkBoxTrybTestowy.CheckedChanged += checkBoxTrybTestowy_CheckedChanged;
        // 
        // labelTestPrice
        // 
        labelTestPrice.AutoSize = true;
        labelTestPrice.Font = new Font("Segoe UI", 9F);
        labelTestPrice.ForeColor = Color.White;
        labelTestPrice.Location = new Point(30, 200);
        labelTestPrice.Name = "labelTestPrice";
        labelTestPrice.Size = new Size(143, 20);
        labelTestPrice.TabIndex = 3;
        labelTestPrice.Text = "Aktualna cena (test):";
        labelTestPrice.Visible = false;
        // 
        // labelRaport
        // 
        labelRaport.Location = new Point(0, 0);
        labelRaport.Name = "labelRaport";
        labelRaport.Size = new Size(100, 23);
        labelRaport.TabIndex = 5;
        // 
        // labelBilansAktualny
        // 
        labelBilansAktualny.AutoSize = true;
        labelBilansAktualny.BackColor = Color.Transparent;
        labelBilansAktualny.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
        labelBilansAktualny.ForeColor = Color.White;
        labelBilansAktualny.Location = new Point(30, 130);
        labelBilansAktualny.Name = "labelBilansAktualny";
        labelBilansAktualny.Size = new Size(188, 23);
        labelBilansAktualny.TabIndex = 6;
        labelBilansAktualny.Text = "Bilans aktualny: BILANS";
        // 
        // panelMain
        // 
        panelMain.BackColor = Color.FromArgb(25, 25, 35);
        panelMain.Controls.Add(groupBox1);
        panelMain.Controls.Add(btnOdswiez);
        panelMain.Controls.Add(btnHistoria);
        panelMain.Controls.Add(btnAktualne);
        panelMain.Dock = DockStyle.Fill;
        panelMain.Location = new Point(350, 45);
        panelMain.Name = "panelMain";
        panelMain.Size = new Size(1132, 708);
        panelMain.TabIndex = 0;
        // 
        // groupBox1
        // 
        groupBox1.BackColor = Color.FromArgb(35, 0, 55);
        groupBox1.Controls.Add(listView1);
        groupBox1.Dock = DockStyle.Top;
        groupBox1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupBox1.ForeColor = Color.White;
        groupBox1.Location = new Point(0, 0);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(10, 20, 10, 10);
        groupBox1.Size = new Size(1132, 480);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Twoje inwestycje";
        // 
        // listView1
        // 
        listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        listView1.BackColor = Color.FromArgb(25, 25, 25);
        listView1.BorderStyle = BorderStyle.None;
        listView1.Font = new Font("Segoe UI", 10F);
        listView1.ForeColor = Color.White;
        listView1.FullRowSelect = true;
        listView1.Location = new Point(10, 43);
        listView1.Name = "listView1";
        listView1.OwnerDraw = true;
        listView1.Size = new Size(1112, 427);
        listView1.SmallImageList = imageList1;
        listView1.TabIndex = 0;
        listView1.UseCompatibleStateImageBehavior = false;
        listView1.View = View.Details;
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
        // btnOdswiez
        // 
        btnOdswiez.BackColor = Color.FromArgb(15, 15, 15);
        btnOdswiez.FlatAppearance.BorderColor = Color.FromArgb(35, 0, 55);
        btnOdswiez.FlatStyle = FlatStyle.Flat;
        btnOdswiez.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnOdswiez.ForeColor = Color.White;
        btnOdswiez.Location = new Point(356, 500);
        btnOdswiez.Name = "btnOdswiez";
        btnOdswiez.Size = new Size(160, 40);
        btnOdswiez.TabIndex = 1;
        btnOdswiez.Text = "Odśwież dane";
        btnOdswiez.UseVisualStyleBackColor = false;
        btnOdswiez.Click += btnOdswiez_Click;
        // 
        // btnHistoria
        // 
        btnHistoria.BackColor = Color.FromArgb(15, 15, 15);
        btnHistoria.FlatAppearance.BorderColor = Color.FromArgb(35, 0, 55);
        btnHistoria.FlatStyle = FlatStyle.Flat;
        btnHistoria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnHistoria.ForeColor = Color.White;
        btnHistoria.Location = new Point(190, 500);
        btnHistoria.Name = "btnHistoria";
        btnHistoria.Size = new Size(170, 40);
        btnHistoria.TabIndex = 2;
        btnHistoria.Text = "Historia sprzedaży";
        btnHistoria.UseVisualStyleBackColor = false;
        btnHistoria.Click += btnHistoria_Click;
        // 
        // btnAktualne
        // 
        btnAktualne.BackColor = Color.FromArgb(15, 15, 15);
        btnAktualne.FlatAppearance.BorderColor = Color.FromArgb(35, 0, 55);
        btnAktualne.FlatStyle = FlatStyle.Flat;
        btnAktualne.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnAktualne.ForeColor = Color.White;
        btnAktualne.Location = new Point(30, 500);
        btnAktualne.Name = "btnAktualne";
        btnAktualne.Size = new Size(163, 40);
        btnAktualne.TabIndex = 3;
        btnAktualne.Text = "Twoje inwestycje";
        btnAktualne.UseVisualStyleBackColor = false;
        btnAktualne.Click += btnAktualne_Click;
        // 
        // MainWindow
        // 
        BackColor = Color.FromArgb(18, 18, 18);
        ClientSize = new Size(1482, 753);
        Controls.Add(panelMain);
        Controls.Add(panelUser);
        Controls.Add(menuStrip2);
        ForeColor = SystemColors.ButtonFace;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Icon = (Icon)resources.GetObject("$this.Icon");
        MainMenuStrip = menuStrip2;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(1300, 800);
        Name = "MainWindow";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Personal Investments";
        Resize += MainWindow_Resize;
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
    private ToolStripMenuItem spacerLeft;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem spacerRight;
}
