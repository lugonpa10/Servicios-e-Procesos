namespace Ejercicio4
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
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.BtnBusqueda = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.btnPosicion = new System.Windows.Forms.Button();
            this.btnHttp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.listResultados = new System.Windows.Forms.ListBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(206, 60);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(338, 20);
            this.txtUrl.TabIndex = 0;
            // 
            // BtnBusqueda
            // 
            this.BtnBusqueda.Location = new System.Drawing.Point(134, 165);
            this.BtnBusqueda.Name = "BtnBusqueda";
            this.BtnBusqueda.Size = new System.Drawing.Size(75, 23);
            this.BtnBusqueda.TabIndex = 1;
            this.BtnBusqueda.Text = "busqueda";
            this.BtnBusqueda.UseVisualStyleBackColor = true;
            this.BtnBusqueda.Click += new System.EventHandler(this.BtnBusqueda_Click);
            // 
            // txt1
            // 
            this.txt1.Location = new System.Drawing.Point(244, 165);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(100, 20);
            this.txt1.TabIndex = 2;
            // 
            // btnPosicion
            // 
            this.btnPosicion.Location = new System.Drawing.Point(134, 212);
            this.btnPosicion.Name = "btnPosicion";
            this.btnPosicion.Size = new System.Drawing.Size(75, 23);
            this.btnPosicion.TabIndex = 3;
            this.btnPosicion.Text = "Posicion";
            this.btnPosicion.UseVisualStyleBackColor = true;
            this.btnPosicion.Click += new System.EventHandler(this.btnPosicion_Click);
            // 
            // btnHttp
            // 
            this.btnHttp.Location = new System.Drawing.Point(134, 252);
            this.btnHttp.Name = "btnHttp";
            this.btnHttp.Size = new System.Drawing.Size(75, 23);
            this.btnHttp.TabIndex = 4;
            this.btnHttp.Text = "Http";
            this.btnHttp.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(357, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "URL";
            // 
            // listResultados
            // 
            this.listResultados.FormattingEnabled = true;
            this.listResultados.Location = new System.Drawing.Point(455, 167);
            this.listResultados.Name = "listResultados";
            this.listResultados.Size = new System.Drawing.Size(312, 225);
            this.listResultados.TabIndex = 9;
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(244, 212);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(100, 20);
            this.txt2.TabIndex = 10;
            // 
            // txt3
            // 
            this.txt3.Location = new System.Drawing.Point(244, 255);
            this.txt3.Name = "txt3";
            this.txt3.Size = new System.Drawing.Size(100, 20);
            this.txt3.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt3);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.listResultados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHttp);
            this.Controls.Add(this.btnPosicion);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.BtnBusqueda);
            this.Controls.Add(this.txtUrl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button BtnBusqueda;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Button btnPosicion;
        private System.Windows.Forms.Button btnHttp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listResultados;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt3;
    }
}

