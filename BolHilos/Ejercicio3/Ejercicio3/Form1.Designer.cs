namespace Ejercicio3
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnDescarga = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(346, 69);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(100, 20);
            this.txtFileName.TabIndex = 0;
            // 
            // btnDescarga
            // 
            this.btnDescarga.Location = new System.Drawing.Point(356, 126);
            this.btnDescarga.Name = "btnDescarga";
            this.btnDescarga.Size = new System.Drawing.Size(75, 23);
            this.btnDescarga.TabIndex = 1;
            this.btnDescarga.Text = "Descargar";
            this.btnDescarga.UseVisualStyleBackColor = true;
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(110, 69);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(173, 204);
            this.txtResults.TabIndex = 2;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnDescarga;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnDescarga);
            this.Controls.Add(this.txtFileName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnDescarga;
        private System.Windows.Forms.TextBox txtResults;
    }
}

