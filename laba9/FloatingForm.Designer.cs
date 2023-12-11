namespace laba7
{
    partial class FloatingForm
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
            trianglesRadioButton = new RadioButton();
            linesRadioButton = new RadioButton();
            netRadioButton = new RadioButton();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // trianglesRadioButton
            // 
            trianglesRadioButton.AutoSize = true;
            trianglesRadioButton.Checked = true;
            trianglesRadioButton.Location = new System.Drawing.Point(1001, 12);
            trianglesRadioButton.Name = "trianglesRadioButton";
            trianglesRadioButton.Size = new Size(102, 19);
            trianglesRadioButton.TabIndex = 0;
            trianglesRadioButton.TabStop = true;
            trianglesRadioButton.Text = "Треугольники";
            trianglesRadioButton.UseVisualStyleBackColor = true;
            trianglesRadioButton.CheckedChanged += trianglesRadioButton_CheckedChanged;
            // 
            // linesRadioButton
            // 
            linesRadioButton.AutoSize = true;
            linesRadioButton.Location = new System.Drawing.Point(1001, 37);
            linesRadioButton.Name = "linesRadioButton";
            linesRadioButton.Size = new Size(61, 19);
            linesRadioButton.TabIndex = 1;
            linesRadioButton.Text = "Линии";
            linesRadioButton.UseVisualStyleBackColor = true;
            linesRadioButton.CheckedChanged += linesRadioButton_CheckedChanged;
            // 
            // netRadioButton
            // 
            netRadioButton.AutoSize = true;
            netRadioButton.Location = new System.Drawing.Point(1001, 62);
            netRadioButton.Name = "netRadioButton";
            netRadioButton.Size = new Size(50, 19);
            netRadioButton.TabIndex = 2;
            netRadioButton.Text = "Сеть";
            netRadioButton.UseVisualStyleBackColor = true;
            netRadioButton.CheckedChanged += netRadioButton_CheckedChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(12, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(944, 506);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // FloatingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1106, 527);
            Controls.Add(pictureBox1);
            Controls.Add(netRadioButton);
            Controls.Add(linesRadioButton);
            Controls.Add(trianglesRadioButton);
            KeyPreview = true;
            Name = "FloatingForm";
            Text = "FloatingForm";
            KeyPress += FloatingForm_KeyPress;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton trianglesRadioButton;
        private RadioButton linesRadioButton;
        private RadioButton netRadioButton;
        private PictureBox pictureBox1;
    }
}