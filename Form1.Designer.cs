namespace PrezentacjaBryl
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mfbtnLabolatorium = new System.Windows.Forms.Button();
            this.mfbtnIndywidualny = new System.Windows.Forms.Button();
            this.mfpictureBox1 = new System.Windows.Forms.PictureBox();
            this.mflabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mfpictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mfbtnLabolatorium
            // 
            this.mfbtnLabolatorium.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mfbtnLabolatorium.Location = new System.Drawing.Point(440, 441);
            this.mfbtnLabolatorium.Name = "mfbtnLabolatorium";
            this.mfbtnLabolatorium.Size = new System.Drawing.Size(247, 105);
            this.mfbtnLabolatorium.TabIndex = 0;
            this.mfbtnLabolatorium.Text = "Labolatorium Nr 3\r\n(Bryły obrotowe)";
            this.mfbtnLabolatorium.UseVisualStyleBackColor = true;
            this.mfbtnLabolatorium.Click += new System.EventHandler(this.mfbtnLabolatorium_Click);
            // 
            // mfbtnIndywidualny
            // 
            this.mfbtnIndywidualny.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.mfbtnIndywidualny.Location = new System.Drawing.Point(693, 441);
            this.mfbtnIndywidualny.Name = "mfbtnIndywidualny";
            this.mfbtnIndywidualny.Size = new System.Drawing.Size(247, 105);
            this.mfbtnIndywidualny.TabIndex = 1;
            this.mfbtnIndywidualny.Text = "Indywidualny Nr 3\r\n(Wielościany)";
            this.mfbtnIndywidualny.UseVisualStyleBackColor = true;
            this.mfbtnIndywidualny.Click += new System.EventHandler(this.mfbtnIndywidualny_Click);
            // 
            // mfpictureBox1
            // 
            this.mfpictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mfpictureBox1.BackgroundImage")));
            this.mfpictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("mfpictureBox1.InitialImage")));
            this.mfpictureBox1.Location = new System.Drawing.Point(516, 186);
            this.mfpictureBox1.Name = "mfpictureBox1";
            this.mfpictureBox1.Size = new System.Drawing.Size(343, 167);
            this.mfpictureBox1.TabIndex = 2;
            this.mfpictureBox1.TabStop = false;
            // 
            // mflabel1
            // 
            this.mflabel1.AutoSize = true;
            this.mflabel1.Font = new System.Drawing.Font("Segoe Print", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mflabel1.Location = new System.Drawing.Point(442, 87);
            this.mflabel1.Name = "mflabel1";
            this.mflabel1.Size = new System.Drawing.Size(526, 51);
            this.mflabel1.TabIndex = 3;
            this.mflabel1.Text = "Prezentacja Brył Geometrychnych";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleGreen;
            this.ClientSize = new System.Drawing.Size(1342, 701);
            this.Controls.Add(this.mflabel1);
            this.Controls.Add(this.mfpictureBox1);
            this.Controls.Add(this.mfbtnIndywidualny);
            this.Controls.Add(this.mfbtnLabolatorium);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.mfpictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button mfbtnLabolatorium;
        private System.Windows.Forms.Button mfbtnIndywidualny;
        private System.Windows.Forms.PictureBox mfpictureBox1;
        private System.Windows.Forms.Label mflabel1;
    }
}

