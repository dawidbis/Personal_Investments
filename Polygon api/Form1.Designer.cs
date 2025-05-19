namespace Polygon_api
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
            btnOdczytaj = new Button();
            dataGridView1 = new DataGridView();
            txtTicker = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnOdczytaj
            // 
            btnOdczytaj.Location = new Point(316, 373);
            btnOdczytaj.Name = "btnOdczytaj";
            btnOdczytaj.Size = new Size(172, 29);
            btnOdczytaj.TabIndex = 0;
            btnOdczytaj.Text = "Odczytaj Dane";
            btnOdczytaj.UseVisualStyleBackColor = true;
            btnOdczytaj.Click += btnOdczytaj_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 169);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(757, 198);
            dataGridView1.TabIndex = 1;
            // 
            // txtTicker
            // 
            txtTicker.Location = new Point(363, 116);
            txtTicker.Name = "txtTicker";
            txtTicker.Size = new Size(125, 27);
            txtTicker.TabIndex = 2;
            txtTicker.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(309, 119);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 5;
            label1.Text = "Ticker";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(txtTicker);
            Controls.Add(dataGridView1);
            Controls.Add(btnOdczytaj);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOdczytaj;
        private DataGridView dataGridView1;
        private TextBox txtTicker;
        private Label label1;
    }
}
