namespace DocumentGenerator
{
    partial class DocumentGenerator
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
            this.lbSubirArchivo = new System.Windows.Forms.Label();
            this.btSubir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btGuardar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btIndexar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbSubirArchivo
            // 
            this.lbSubirArchivo.AutoSize = true;
            this.lbSubirArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubirArchivo.Location = new System.Drawing.Point(12, 20);
            this.lbSubirArchivo.Name = "lbSubirArchivo";
            this.lbSubirArchivo.Size = new System.Drawing.Size(185, 20);
            this.lbSubirArchivo.TabIndex = 7;
            this.lbSubirArchivo.Text = "Subir archivo a indexar:";
            // 
            // btSubir
            // 
            this.btSubir.Location = new System.Drawing.Point(212, 17);
            this.btSubir.Name = "btSubir";
            this.btSubir.Size = new System.Drawing.Size(249, 29);
            this.btSubir.TabIndex = 8;
            this.btSubir.Text = "Buscar archivo ...";
            this.btSubir.UseVisualStyleBackColor = true;
            this.btSubir.Click += new System.EventHandler(this.SubirFichero);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Guardar JSON:";
            // 
            // btGuardar
            // 
            this.btGuardar.Location = new System.Drawing.Point(212, 58);
            this.btGuardar.Name = "btGuardar";
            this.btGuardar.Size = new System.Drawing.Size(249, 29);
            this.btGuardar.TabIndex = 10;
            this.btGuardar.Text = "Guardar archivo ...";
            this.btGuardar.UseVisualStyleBackColor = true;
            this.btGuardar.Click += new System.EventHandler(this.Guardar);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Indexar en Elastic:";
            // 
            // btIndexar
            // 
            this.btIndexar.Location = new System.Drawing.Point(212, 101);
            this.btIndexar.Name = "btIndexar";
            this.btIndexar.Size = new System.Drawing.Size(249, 29);
            this.btIndexar.TabIndex = 12;
            this.btIndexar.Text = "Indexar";
            this.btIndexar.UseVisualStyleBackColor = true;
            this.btIndexar.Click += new System.EventHandler(this.Indexar);
            // 
            // DocumentGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 163);
            this.Controls.Add(this.btIndexar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btGuardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSubir);
            this.Controls.Add(this.lbSubirArchivo);
            this.Name = "DocumentGenerator";
            this.Text = "DocumentGenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSubirArchivo;
        private System.Windows.Forms.Button btSubir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btGuardar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btIndexar;
    }
}