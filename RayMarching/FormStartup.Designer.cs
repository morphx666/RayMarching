namespace RayMarching {
    partial class FormStartup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton3 = new System.Windows.Forms.RadioButton();
            this.RadioButton4 = new System.Windows.Forms.RadioButton();
            this.ButtonRun = new System.Windows.Forms.Button();
            this.LabelStep1 = new System.Windows.Forms.Label();
            this.LabelStep2 = new System.Windows.Forms.Label();
            this.LabelStep3 = new System.Windows.Forms.Label();
            this.LabelStep4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the startup form:";
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(41, 32);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(84, 29);
            this.RadioButton1.TabIndex = 1;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "Step 1";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(41, 82);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(84, 29);
            this.RadioButton2.TabIndex = 1;
            this.RadioButton2.Text = "Step 2";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton3
            // 
            this.RadioButton3.AutoSize = true;
            this.RadioButton3.Location = new System.Drawing.Point(41, 132);
            this.RadioButton3.Name = "RadioButton3";
            this.RadioButton3.Size = new System.Drawing.Size(84, 29);
            this.RadioButton3.TabIndex = 1;
            this.RadioButton3.Text = "Step 3";
            this.RadioButton3.UseVisualStyleBackColor = true;
            // 
            // RadioButton4
            // 
            this.RadioButton4.AutoSize = true;
            this.RadioButton4.Location = new System.Drawing.Point(41, 182);
            this.RadioButton4.Name = "RadioButton4";
            this.RadioButton4.Size = new System.Drawing.Size(84, 29);
            this.RadioButton4.TabIndex = 1;
            this.RadioButton4.Text = "Step 4";
            this.RadioButton4.UseVisualStyleBackColor = true;
            // 
            // ButtonRun
            // 
            this.ButtonRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.ButtonRun.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ButtonRun.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepSkyBlue;
            this.ButtonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRun.Location = new System.Drawing.Point(567, 272);
            this.ButtonRun.Name = "ButtonRun";
            this.ButtonRun.Size = new System.Drawing.Size(112, 40);
            this.ButtonRun.TabIndex = 2;
            this.ButtonRun.Text = "Run";
            this.ButtonRun.UseVisualStyleBackColor = false;
            this.ButtonRun.Click += new System.EventHandler(this.ButtonRun_Click);
            // 
            // LabelStep1
            // 
            this.LabelStep1.AutoSize = true;
            this.LabelStep1.ForeColor = System.Drawing.Color.LightSlateGray;
            this.LabelStep1.Location = new System.Drawing.Point(59, 59);
            this.LabelStep1.Name = "LabelStep1";
            this.LabelStep1.Size = new System.Drawing.Size(581, 25);
            this.LabelStep1.TabIndex = 3;
            this.LabelStep1.Text = "Adjust the size of a circle based on the proximity of different shapes";
            // 
            // LabelStep2
            // 
            this.LabelStep2.AutoSize = true;
            this.LabelStep2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.LabelStep2.Location = new System.Drawing.Point(59, 109);
            this.LabelStep2.Name = "LabelStep2";
            this.LabelStep2.Size = new System.Drawing.Size(385, 25);
            this.LabelStep2.TabIndex = 3;
            this.LabelStep2.Text = "Create the walking circles until a shape is hit";
            // 
            // LabelStep3
            // 
            this.LabelStep3.AutoSize = true;
            this.LabelStep3.ForeColor = System.Drawing.Color.LightSlateGray;
            this.LabelStep3.Location = new System.Drawing.Point(59, 159);
            this.LabelStep3.Name = "LabelStep3";
            this.LabelStep3.Size = new System.Drawing.Size(371, 25);
            this.LabelStep3.TabIndex = 3;
            this.LabelStep3.Text = "Sweep the scene to detect all visible points";
            // 
            // LabelStep4
            // 
            this.LabelStep4.AutoSize = true;
            this.LabelStep4.ForeColor = System.Drawing.Color.LightSlateGray;
            this.LabelStep4.Location = new System.Drawing.Point(59, 209);
            this.LabelStep4.Name = "LabelStep4";
            this.LabelStep4.Size = new System.Drawing.Size(419, 25);
            this.LabelStep4.TabIndex = 3;
            this.LabelStep4.Text = "Render a \"3D\" scence based on the visible points";
            // 
            // FormStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(691, 324);
            this.Controls.Add(this.LabelStep4);
            this.Controls.Add(this.LabelStep3);
            this.Controls.Add(this.LabelStep2);
            this.Controls.Add(this.LabelStep1);
            this.Controls.Add(this.ButtonRun);
            this.Controls.Add(this.RadioButton4);
            this.Controls.Add(this.RadioButton3);
            this.Controls.Add(this.RadioButton2);
            this.Controls.Add(this.RadioButton1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormStartup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormStartup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RadioButton1;
        private System.Windows.Forms.RadioButton RadioButton2;
        private System.Windows.Forms.RadioButton RadioButton3;
        private System.Windows.Forms.RadioButton RadioButton4;
        private System.Windows.Forms.Button ButtonRun;
        private System.Windows.Forms.Label LabelStep1;
        private System.Windows.Forms.Label LabelStep2;
        private System.Windows.Forms.Label LabelStep3;
        private System.Windows.Forms.Label LabelStep4;
    }
}