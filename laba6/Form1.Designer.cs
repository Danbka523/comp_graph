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
            this.clearButton = new System.Windows.Forms.Button();
            this.perspectiveRadioButtom = new System.Windows.Forms.RadioButton();
            this.isometricRadioButtom = new System.Windows.Forms.RadioButton();
            this.RotateCustomAxisButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.degreeCustom = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.z2textBox = new System.Windows.Forms.TextBox();
            this.y2textBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.x2textBox = new System.Windows.Forms.TextBox();
            this.z1textBox = new System.Windows.Forms.TextBox();
            this.y1textBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.x1textBox = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(203, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1016, 618);
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
            // perspectiveRadioButtom
            // 
            this.perspectiveRadioButtom.AutoSize = true;
            this.perspectiveRadioButtom.Checked = true;
            this.perspectiveRadioButtom.Location = new System.Drawing.Point(12, 540);
            this.perspectiveRadioButtom.Name = "perspectiveRadioButtom";
            this.perspectiveRadioButtom.Size = new System.Drawing.Size(104, 17);
            this.perspectiveRadioButtom.TabIndex = 29;
            this.perspectiveRadioButtom.TabStop = true;
            this.perspectiveRadioButtom.Text = "Перспективная";
            this.perspectiveRadioButtom.UseVisualStyleBackColor = true;
            this.perspectiveRadioButtom.CheckedChanged += new System.EventHandler(this.perspectiveRadioButtom_CheckedChanged);
            // 
            // isometricRadioButtom
            // 
            this.isometricRadioButtom.AutoSize = true;
            this.isometricRadioButtom.Location = new System.Drawing.Point(12, 563);
            this.isometricRadioButtom.Name = "isometricRadioButtom";
            this.isometricRadioButtom.Size = new System.Drawing.Size(111, 17);
            this.isometricRadioButtom.TabIndex = 31;
            this.isometricRadioButtom.Text = "Изометрическая";
            this.isometricRadioButtom.UseVisualStyleBackColor = true;
            this.isometricRadioButtom.CheckedChanged += new System.EventHandler(this.isometricRadioButtom_CheckedChanged);
            // 
            // RotateCustomAxisButton
            // 
            this.RotateCustomAxisButton.Location = new System.Drawing.Point(47, 419);
            this.RotateCustomAxisButton.Name = "RotateCustomAxisButton";
            this.RotateCustomAxisButton.Size = new System.Drawing.Size(84, 23);
            this.RotateCustomAxisButton.TabIndex = 34;
            this.RotateCustomAxisButton.Text = "Вращать";
            this.RotateCustomAxisButton.UseVisualStyleBackColor = true;
            this.RotateCustomAxisButton.Click += new System.EventHandler(this.RotateCustomAxisButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 396);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Градусы";
            // 
            // degreeCustom
            // 
            this.degreeCustom.Location = new System.Drawing.Point(65, 393);
            this.degreeCustom.Name = "degreeCustom";
            this.degreeCustom.Size = new System.Drawing.Size(43, 20);
            this.degreeCustom.TabIndex = 32;
            this.degreeCustom.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "Прямая";
            // 
            // z2textBox
            // 
            this.z2textBox.Location = new System.Drawing.Point(165, 364);
            this.z2textBox.Name = "z2textBox";
            this.z2textBox.Size = new System.Drawing.Size(32, 20);
            this.z2textBox.TabIndex = 51;
            this.z2textBox.Text = "1";
            // 
            // y2textBox
            // 
            this.y2textBox.Location = new System.Drawing.Point(99, 364);
            this.y2textBox.Name = "y2textBox";
            this.y2textBox.Size = new System.Drawing.Size(32, 20);
            this.y2textBox.TabIndex = 50;
            this.y2textBox.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(139, 367);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 49;
            this.label10.Text = "z2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 367);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "y2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 367);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 13);
            this.label12.TabIndex = 47;
            this.label12.Text = "x2";
            // 
            // x2textBox
            // 
            this.x2textBox.Location = new System.Drawing.Point(35, 364);
            this.x2textBox.Name = "x2textBox";
            this.x2textBox.Size = new System.Drawing.Size(32, 20);
            this.x2textBox.TabIndex = 46;
            this.x2textBox.Text = "1";
            // 
            // z1textBox
            // 
            this.z1textBox.Location = new System.Drawing.Point(165, 337);
            this.z1textBox.Name = "z1textBox";
            this.z1textBox.Size = new System.Drawing.Size(32, 20);
            this.z1textBox.TabIndex = 44;
            this.z1textBox.Text = "0";
            // 
            // y1textBox
            // 
            this.y1textBox.Location = new System.Drawing.Point(99, 337);
            this.y1textBox.Name = "y1textBox";
            this.y1textBox.Size = new System.Drawing.Size(32, 20);
            this.y1textBox.TabIndex = 43;
            this.y1textBox.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(139, 340);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 42;
            this.label13.Text = "z1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(73, 340);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "y1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 340);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "x1";
            // 
            // x1textBox
            // 
            this.x1textBox.Location = new System.Drawing.Point(35, 337);
            this.x1textBox.Name = "x1textBox";
            this.x1textBox.Size = new System.Drawing.Size(32, 20);
            this.x1textBox.TabIndex = 39;
            this.x1textBox.Text = "0";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "XY",
            "XZ",
            "YZ"});
            this.comboBox2.Location = new System.Drawing.Point(58, 64);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(73, 21);
            this.comboBox2.TabIndex = 52;
            this.comboBox2.Text = "XY";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.comboBox3.Location = new System.Drawing.Point(50, 253);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(81, 21);
            this.comboBox3.TabIndex = 53;
            this.comboBox3.Text = "X";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 615);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.z2textBox);
            this.Controls.Add(this.y2textBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.x2textBox);
            this.Controls.Add(this.z1textBox);
            this.Controls.Add(this.y1textBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.x1textBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.RotateCustomAxisButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.degreeCustom);
            this.Controls.Add(this.isometricRadioButtom);
            this.Controls.Add(this.perspectiveRadioButtom);
            this.Controls.Add(this.clearButton);
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
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RadioButton perspectiveRadioButtom;
        private System.Windows.Forms.RadioButton isometricRadioButtom;
        private System.Windows.Forms.Button RotateCustomAxisButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox degreeCustom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox z2textBox;
        private System.Windows.Forms.TextBox y2textBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox x2textBox;
        private System.Windows.Forms.TextBox z1textBox;
        private System.Windows.Forms.TextBox y1textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox x1textBox;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
    }
}

