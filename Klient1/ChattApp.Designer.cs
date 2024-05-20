namespace Klient1
{
    partial class KlientForm
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
            this.btnKoppla = new System.Windows.Forms.Button();
            this.btnSkicka = new System.Windows.Forms.Button();
            this.tbxMeddelanden = new System.Windows.Forms.TextBox();
            this.tbxInkorg = new System.Windows.Forms.TextBox();
            this.btnLoggaUt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKoppla
            // 
            this.btnKoppla.Location = new System.Drawing.Point(881, 12);
            this.btnKoppla.Name = "btnKoppla";
            this.btnKoppla.Size = new System.Drawing.Size(148, 68);
            this.btnKoppla.TabIndex = 0;
            this.btnKoppla.Text = "Koppla";
            this.btnKoppla.UseVisualStyleBackColor = true;
            this.btnKoppla.Click += new System.EventHandler(this.btnKoppla_Click);
            // 
            // btnSkicka
            // 
            this.btnSkicka.Location = new System.Drawing.Point(881, 387);
            this.btnSkicka.Name = "btnSkicka";
            this.btnSkicka.Size = new System.Drawing.Size(148, 68);
            this.btnSkicka.TabIndex = 1;
            this.btnSkicka.Text = "Skicka";
            this.btnSkicka.UseVisualStyleBackColor = true;
            this.btnSkicka.Click += new System.EventHandler(this.btnSkicka_Click);
            // 
            // tbxMeddelanden
            // 
            this.tbxMeddelanden.Location = new System.Drawing.Point(26, 461);
            this.tbxMeddelanden.Multiline = true;
            this.tbxMeddelanden.Name = "tbxMeddelanden";
            this.tbxMeddelanden.Size = new System.Drawing.Size(1003, 45);
            this.tbxMeddelanden.TabIndex = 2;
            // 
            // tbxInkorg
            // 
            this.tbxInkorg.Location = new System.Drawing.Point(26, 86);
            this.tbxInkorg.Multiline = true;
            this.tbxInkorg.Name = "tbxInkorg";
            this.tbxInkorg.Size = new System.Drawing.Size(778, 351);
            this.tbxInkorg.TabIndex = 3;
            // 
            // btnLoggaUt
            // 
            this.btnLoggaUt.Location = new System.Drawing.Point(26, 12);
            this.btnLoggaUt.Name = "btnLoggaUt";
            this.btnLoggaUt.Size = new System.Drawing.Size(148, 68);
            this.btnLoggaUt.TabIndex = 4;
            this.btnLoggaUt.Text = "Logga ut";
            this.btnLoggaUt.UseVisualStyleBackColor = true;
            this.btnLoggaUt.Click += new System.EventHandler(this.btnLoggaUt_Click);
            // 
            // KlientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 518);
            this.Controls.Add(this.btnLoggaUt);
            this.Controls.Add(this.tbxInkorg);
            this.Controls.Add(this.tbxMeddelanden);
            this.Controls.Add(this.btnSkicka);
            this.Controls.Add(this.btnKoppla);
            this.Name = "KlientForm";
            this.Text = "ChattApp";
            this.Load += new System.EventHandler(this.KlientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKoppla;
        private System.Windows.Forms.Button btnSkicka;
        private System.Windows.Forms.TextBox tbxMeddelanden;
        private System.Windows.Forms.TextBox tbxInkorg;
        private System.Windows.Forms.Button btnLoggaUt;
    }
}
