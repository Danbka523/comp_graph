namespace laba6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.AxisCheckBox = new System.Windows.Forms.CheckBox();
            this.XYRadioButton = new System.Windows.Forms.RadioButton();
            this.XZRadioButton = new System.Windows.Forms.RadioButton();
            this.YZRadioButton = new System.Windows.Forms.RadioButton();
            this.mirrorButton = new System.Windows.Forms.Button();
            this.cXtextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cYtextBox = new System.Windows.Forms.TextBox();
            this.cZtextBox = new System.Windows.Forms.TextBox();
            this.shiftButton = new System.Windows.Forms.Button();
            this.scaleButton = new System.Windows.Forms.Button();
            this.sZtextBox = new System.Windows.Forms.TextBox();
            this.sYtextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sXtextBox = new System.Windows.Forms.TextBox();
            this.degreeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RotateAxisButton = new System.Windows.Forms.Button();
            this.ZRadioButton = new System.Windows.Forms.RadioButton();
            this.YRadioButton = new System.Windows.Forms.RadioButton();
            this.XRadioButton = new System.Windows.Forms.RadioButton();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(203, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1004, 603);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "тетраэдр",
            "гексаэдр",
            "октаэдр",
            "икосаэдр",
            "додекаэдр"});
            this.comboBox1.Location = new System.Drawing.Point(35, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "тетраэдр";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Нарисовать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AxisCheckBox
            // 
            this.AxisCheckBox.AutoSize = true;
            this.AxisCheckBox.Location = new System.Drawing.Point(12, 586);
            this.AxisCheckBox.Name = "AxisCheckBox";
            this.AxisCheckBox.Size = new System.Drawing.Size(96, 17);
            this.AxisCheckBox.TabIndex = 3;
            this.AxisCheckBox.Text = "Показать оси";
            this.AxisCheckBox.UseVisualStyleBackColor = true;
            this.AxisCheckBox.CheckedChanged += new System.EventHandler(this.AxisCheckBox_CheckedChanged);
            // 
            // XYRadioButton
            // 
            this.XYRadioButton.AutoSize = true;
            this.XYRadioButton.Checked = true;
            this.XYRadioButton.Location = new System.Drawing.Point(24, 68);
            this.XYRadioButton.Name = "XYRadioButton";
            this.XYRadioButton.Size = new System.Drawing.Size(39, 17);
            this.XYRadioButton.TabIndex = 4;
            this.XYRadioButton.TabStop = true;
            this.XYRadioButton.Text = "XY";
            this.XYRadioButton.UseVisualStyleBackColor = true;
            // 
            // XZRadioButton
            // 
            this.XZRadioButton.AutoSize = true;
            this.XZRadioButton.Location = new System.Drawing.Point(69, 68);
            this.XZRadioButton.Name = "XZRadioButton";
            this.XZRadioButton.Size = new System.Drawing.Size(39, 17);
            this.XZRadioButton.TabIndex = 5;
            this.XZRadioButton.Text = "XZ";
            this.XZRadioButton.UseVisualStyleBackColor = true;
            // 
            // YZRadioButton
            // 
            this.YZRadioButton.AutoSize = true;
            this.YZRadioButton.Location = new System.Drawing.Point(117, 68);
            this.YZRadioButton.Name = "YZRadioButton";
            this.YZRadioButton.Size = new System.Drawing.Size(39, 17);
            this.YZRadioButton.TabIndex = 6;
            this.YZRadioButton.Text = "YZ";
            this.YZRadioButton.UseVisualStyleBackColor = true;
            // 
            // mirrorButton
            // 
            this.mirrorButton.Location = new System.Drawing.Point(58, 91);
            this.mirrorButton.Name = "mirrorButton";
            this.mirrorButton.Size = new System.Drawing.Size(75, 23);
            this.mirrorButton.TabIndex = 7;
            this.mirrorButton.Text = "Отразить";
            this.mirrorButton.UseVisualStyleBackColor = true;
            this.mirrorButton.Click += new System.EventHandler(this.mirrorButton_Click);
            // 
            // cXtextBox
            // 
            this.cXtextBox.Location = new System.Drawing.Point(35, 120);
            this.cXtextBox.Name = "cXtextBox";
            this.cXtextBox.Size = new System.Drawing.Size(32, 20);
            this.cXtextBox.TabIndex = 8;
            this.cXtextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "cX";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "cY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "cZ";
            // 
            // cYtextBox
            // 
            this.cYtextBox.Location = new System.Drawing.Point(99, 120);
            this.cYtextBox.Name = "cYtextBox";
            this.cYtextBox.Size = new System.Drawing.Size(32, 20);
            this.cYtextBox.TabIndex = 12;
            this.cYtextBox.Text = "0";
            // 
            // cZtextBox
            // 
            this.cZtextBox.Location = new System.Drawing.Point(165, 120);
            this.cZtextBox.Name = "cZtextBox";
            this.cZtextBox.Size = new System.Drawing.Size(32, 20);
            this.cZtextBox.TabIndex = 13;
            this.cZtextBox.Text = "0";
            // 
            // shiftButton
            // 
            this.shiftButton.Location = new System.Drawing.Point(56, 146);
            this.shiftButton.Name = "shiftButton";
            this.shiftButton.Size = new System.Drawing.Size(75, 23);
            this.shiftButton.TabIndex = 14;
            this.shiftButton.Text = "Сместить";
            this.shiftButton.UseVisualStyleBackColor = true;
            this.shiftButton.Click += new System.EventHandler(this.shiftButton_Click);
            // 
            // scaleButton
            // 
            this.scaleButton.Location = new System.Drawing.Point(47, 201);
            this.scaleButton.Name = "scaleButton";
            this.scaleButton.Size = new System.Drawing.Size(109, 23);
            this.scaleButton.TabIndex = 21;
            this.scaleButton.Text = "Масштабировать";
            this.scaleButton.UseVisualStyleBackColor = true;
            this.scaleButton.Click += new System.EventHandler(this.scaleButton_Click);
            // 
            // sZtextBox
            // 
            this.sZtextBox.Location = new System.Drawing.Point(165, 175);
            this.sZtextBox.Name = "sZtextBox";
            this.sZtextBox.Size = new System.Drawing.Size(32, 20);
            this.sZtextBox.TabIndex = 20;
            this.sZtextBox.Text = "1";
            // 
            // sYtextBox
            // 
            this.sYtextBox.Location = new System.Drawing.Point(99, 175);
            this.sYtextBox.Name = "sYtextBox";
            this.sYtextBox.Size = new System.Drawing.Size(32, 20);
            this.sYtextBox.TabIndex = 19;
            this.sYtextBox.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "sZ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(73, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "sY";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "sX";
            // 
            // sXtextBox
            // 
            this.sXtextBox.Location = new System.Drawing.Point(35, 175);
            this.sXtextBox.Name = "sXtextBox";
            this.sXtextBox.Size = new System.Drawing.Size(32, 20);
            this.sXtextBox.TabIndex = 15;
            this.sXtextBox.Text = "1";
            // 
            // degreeTextBox
            // 
            this.degreeTextBox.Location = new System.Drawing.Point(65, 227);
            this.degreeTextBox.Name = "degreeTextBox";
            this.degreeTextBox.Size = new System.Drawing.Size(43, 20);
            this.degreeTextBox.TabIndex = 22;
            this.degreeTextBox.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Градусы";
            // 
            // RotateAxisButton
            // 
            this.RotateAxisButton.Location = new System.Drawing.Point(47, 276);
            this.RotateAxisButton.Name = "RotateAxisButton";
            this.RotateAxisButton.Size = new System.Drawing.Size(84, 23);
            this.RotateAxisButton.TabIndex = 24;
            this.RotateAxisButton.Text = "Вращать";
            this.RotateAxisButton.UseVisualStyleBackColor = true;
            this.RotateAxisButton.Click += new System.EventHandler(this.RotateAxisButton_Click);
            // 
            // ZRadioButton
            // 
            this.ZRadioButton.AutoSize = true;
            this.ZRadioButton.Location = new System.Drawing.Point(106, 253);
            this.ZRadioButton.Name = "ZRadioButton";
            this.ZRadioButton.Size = new System.Drawing.Size(32, 17);
            this.ZRadioButton.TabIndex = 27;
            this.ZRadioButton.Text = "Z";
            this.ZRadioButton.UseVisualStyleBackColor = true;
            // 
            // YRadioButton
            // 
            this.YRadioButton.AutoSize = true;
            this.YRadioButton.Location = new System.Drawing.Point(58, 253);
            this.YRadioButton.Name = "YRadioButton";
            this.YRadioButton.Size = new System.Drawing.Size(32, 17);
            this.YRadioButton.TabIndex = 26;
            this.YRadioButton.Text = "Y";
            this.YRadioButton.UseVisualStyleBackColor = true;
            // 
            // XRadioButton
            // 
            this.XRadioButton.AutoSize = true;
            this.XRadioButton.Checked = true;
            this.XRadioButton.Location = new System.Drawing.Point(13, 253);
            this.XRadioButton.Name = "XRadioButton";
            this.XRadioButton.Size = new System.Drawing.Size(32, 17);
            this.XRadioButton.TabIndex = 25;
            this.XRadioButton.TabStop = true;
            this.XRadioButton.Text = "X";
            this.XRadioButton.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(114, 582);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 28;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 615);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.ZRadioButton);
            this.Controls.Add(this.YRadioButton);
            this.Controls.Add(this.XRadioButton);
            this.Controls.Add(this.RotateAxisButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.degreeTextBox);
            this.Controls.Add(this.scaleButton);
            this.Controls.Add(this.sZtextBox);
            this.Controls.Add(this.sYtextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sXtextBox);
            this.Controls.Add(this.shiftButton);
            this.Controls.Add(this.cZtextBox);
            this.Controls.Add(this.cYtextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cXtextBox);
            this.Controls.Add(this.mirrorButton);
            this.Controls.Add(this.YZRadioButton);
            this.Controls.Add(this.XZRadioButton);
            this.Controls.Add(this.XYRadioButton);
            this.Controls.Add(this.AxisCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "триДЭ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox AxisCheckBox;
        private System.Windows.Forms.RadioButton XYRadioButton;
        private System.Windows.Forms.RadioButton XZRadioButton;
        private System.Windows.Forms.RadioButton YZRadioButton;
        private System.Windows.Forms.Button mirrorButton;
        private System.Windows.Forms.TextBox cXtextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cYtextBox;
        private System.Windows.Forms.TextBox cZtextBox;
        private System.Windows.Forms.Button shiftButton;
        private System.Windows.Forms.Button scaleButton;
        private System.Windows.Forms.TextBox sZtextBox;
        private System.Windows.Forms.TextBox sYtextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sXtextBox;
        private System.Windows.Forms.TextBox degreeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button RotateAxisButton;
        private System.Windows.Forms.RadioButton ZRadioButton;
        private System.Windows.Forms.RadioButton YRadioButton;
        private System.Windows.Forms.RadioButton XRadioButton;
        private System.Windows.Forms.Button clearButton;
    }
}

