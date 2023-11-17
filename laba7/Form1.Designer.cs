namespace laba7
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
            pictureBox1 = new PictureBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            AxisCheckBox = new CheckBox();
            mirrorButton = new Button();
            cXtextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cYtextBox = new TextBox();
            cZtextBox = new TextBox();
            shiftButton = new Button();
            scaleButton = new Button();
            sZtextBox = new TextBox();
            sYtextBox = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            sXtextBox = new TextBox();
            degreeTextBox = new TextBox();
            label7 = new Label();
            RotateAxisButton = new Button();
            clearButton = new Button();
            perspectiveRadioButtom = new RadioButton();
            isometricRadioButtom = new RadioButton();
            RotateCustomAxisButton = new Button();
            label8 = new Label();
            degreeCustom = new TextBox();
            label9 = new Label();
            z2textBox = new TextBox();
            y2textBox = new TextBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            x2textBox = new TextBox();
            z1textBox = new TextBox();
            y1textBox = new TextBox();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            x1textBox = new TextBox();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            saveButton = new Button();
            loadButton = new Button();
            label16 = new Label();
            figureRotButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(237, 0);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1185, 713);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "тетраэдр", "гексаэдр", "октаэдр", "икосаэдр", "додекаэдр" });
            comboBox1.Location = new System.Drawing.Point(41, 14);
            comboBox1.Margin = new Padding(4, 3, 4, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(140, 23);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "тетраэдр";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(55, 45);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(100, 27);
            button1.TabIndex = 2;
            button1.Text = "Нарисовать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // AxisCheckBox
            // 
            AxisCheckBox.AutoSize = true;
            AxisCheckBox.Location = new System.Drawing.Point(14, 676);
            AxisCheckBox.Margin = new Padding(4, 3, 4, 3);
            AxisCheckBox.Name = "AxisCheckBox";
            AxisCheckBox.Size = new Size(99, 19);
            AxisCheckBox.TabIndex = 3;
            AxisCheckBox.Text = "Показать оси";
            AxisCheckBox.UseVisualStyleBackColor = true;
            AxisCheckBox.CheckedChanged += AxisCheckBox_CheckedChanged;
            // 
            // mirrorButton
            // 
            mirrorButton.Location = new System.Drawing.Point(68, 105);
            mirrorButton.Margin = new Padding(4, 3, 4, 3);
            mirrorButton.Name = "mirrorButton";
            mirrorButton.Size = new Size(88, 27);
            mirrorButton.TabIndex = 7;
            mirrorButton.Text = "Отразить";
            mirrorButton.UseVisualStyleBackColor = true;
            mirrorButton.Click += mirrorButton_Click;
            // 
            // cXtextBox
            // 
            cXtextBox.Location = new System.Drawing.Point(41, 138);
            cXtextBox.Margin = new Padding(4, 3, 4, 3);
            cXtextBox.Name = "cXtextBox";
            cXtextBox.Size = new Size(37, 23);
            cXtextBox.TabIndex = 8;
            cXtextBox.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 142);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 9;
            label1.Text = "cX";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(85, 142);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(20, 15);
            label2.TabIndex = 10;
            label2.Text = "cY";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(162, 142);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(20, 15);
            label3.TabIndex = 11;
            label3.Text = "cZ";
            // 
            // cYtextBox
            // 
            cYtextBox.Location = new System.Drawing.Point(115, 138);
            cYtextBox.Margin = new Padding(4, 3, 4, 3);
            cYtextBox.Name = "cYtextBox";
            cYtextBox.Size = new Size(37, 23);
            cYtextBox.TabIndex = 12;
            cYtextBox.Text = "0";
            // 
            // cZtextBox
            // 
            cZtextBox.Location = new System.Drawing.Point(192, 138);
            cZtextBox.Margin = new Padding(4, 3, 4, 3);
            cZtextBox.Name = "cZtextBox";
            cZtextBox.Size = new Size(37, 23);
            cZtextBox.TabIndex = 13;
            cZtextBox.Text = "0";
            // 
            // shiftButton
            // 
            shiftButton.Location = new System.Drawing.Point(65, 168);
            shiftButton.Margin = new Padding(4, 3, 4, 3);
            shiftButton.Name = "shiftButton";
            shiftButton.Size = new Size(88, 27);
            shiftButton.TabIndex = 14;
            shiftButton.Text = "Сместить";
            shiftButton.UseVisualStyleBackColor = true;
            shiftButton.Click += shiftButton_Click;
            // 
            // scaleButton
            // 
            scaleButton.Location = new System.Drawing.Point(55, 232);
            scaleButton.Margin = new Padding(4, 3, 4, 3);
            scaleButton.Name = "scaleButton";
            scaleButton.Size = new Size(127, 27);
            scaleButton.TabIndex = 21;
            scaleButton.Text = "Масштабировать";
            scaleButton.UseVisualStyleBackColor = true;
            scaleButton.Click += scaleButton_Click;
            // 
            // sZtextBox
            // 
            sZtextBox.Location = new System.Drawing.Point(192, 202);
            sZtextBox.Margin = new Padding(4, 3, 4, 3);
            sZtextBox.Name = "sZtextBox";
            sZtextBox.Size = new Size(37, 23);
            sZtextBox.TabIndex = 20;
            sZtextBox.Text = "1";
            // 
            // sYtextBox
            // 
            sYtextBox.Location = new System.Drawing.Point(115, 202);
            sYtextBox.Margin = new Padding(4, 3, 4, 3);
            sYtextBox.Name = "sYtextBox";
            sYtextBox.Size = new Size(37, 23);
            sYtextBox.TabIndex = 19;
            sYtextBox.Text = "1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(162, 205);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 18;
            label4.Text = "sZ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(85, 205);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 17;
            label5.Text = "sY";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(10, 205);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(19, 15);
            label6.TabIndex = 16;
            label6.Text = "sX";
            // 
            // sXtextBox
            // 
            sXtextBox.Location = new System.Drawing.Point(41, 202);
            sXtextBox.Margin = new Padding(4, 3, 4, 3);
            sXtextBox.Name = "sXtextBox";
            sXtextBox.Size = new Size(37, 23);
            sXtextBox.TabIndex = 15;
            sXtextBox.Text = "1";
            // 
            // degreeTextBox
            // 
            degreeTextBox.Location = new System.Drawing.Point(76, 262);
            degreeTextBox.Margin = new Padding(4, 3, 4, 3);
            degreeTextBox.Name = "degreeTextBox";
            degreeTextBox.Size = new Size(50, 23);
            degreeTextBox.TabIndex = 22;
            degreeTextBox.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(10, 265);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(53, 15);
            label7.TabIndex = 23;
            label7.Text = "Градусы";
            // 
            // RotateAxisButton
            // 
            RotateAxisButton.Location = new System.Drawing.Point(55, 318);
            RotateAxisButton.Margin = new Padding(4, 3, 4, 3);
            RotateAxisButton.Name = "RotateAxisButton";
            RotateAxisButton.Size = new Size(98, 27);
            RotateAxisButton.TabIndex = 24;
            RotateAxisButton.Text = "Вращать";
            RotateAxisButton.UseVisualStyleBackColor = true;
            RotateAxisButton.Click += RotateAxisButton_Click;
            // 
            // clearButton
            // 
            clearButton.Location = new System.Drawing.Point(133, 672);
            clearButton.Margin = new Padding(4, 3, 4, 3);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(88, 27);
            clearButton.TabIndex = 28;
            clearButton.Text = "Очистить";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // perspectiveRadioButtom
            // 
            perspectiveRadioButtom.AutoSize = true;
            perspectiveRadioButtom.Checked = true;
            perspectiveRadioButtom.Location = new System.Drawing.Point(14, 623);
            perspectiveRadioButtom.Margin = new Padding(4, 3, 4, 3);
            perspectiveRadioButtom.Name = "perspectiveRadioButtom";
            perspectiveRadioButtom.Size = new Size(109, 19);
            perspectiveRadioButtom.TabIndex = 29;
            perspectiveRadioButtom.TabStop = true;
            perspectiveRadioButtom.Text = "Перспективная";
            perspectiveRadioButtom.UseVisualStyleBackColor = true;
            perspectiveRadioButtom.CheckedChanged += perspectiveRadioButtom_CheckedChanged;
            // 
            // isometricRadioButtom
            // 
            isometricRadioButtom.AutoSize = true;
            isometricRadioButtom.Location = new System.Drawing.Point(14, 650);
            isometricRadioButtom.Margin = new Padding(4, 3, 4, 3);
            isometricRadioButtom.Name = "isometricRadioButtom";
            isometricRadioButtom.Size = new Size(117, 19);
            isometricRadioButtom.TabIndex = 31;
            isometricRadioButtom.Text = "Изометрическая";
            isometricRadioButtom.UseVisualStyleBackColor = true;
            isometricRadioButtom.CheckedChanged += isometricRadioButtom_CheckedChanged;
            // 
            // RotateCustomAxisButton
            // 
            RotateCustomAxisButton.Location = new System.Drawing.Point(55, 483);
            RotateCustomAxisButton.Margin = new Padding(4, 3, 4, 3);
            RotateCustomAxisButton.Name = "RotateCustomAxisButton";
            RotateCustomAxisButton.Size = new Size(98, 27);
            RotateCustomAxisButton.TabIndex = 34;
            RotateCustomAxisButton.Text = "Вращать";
            RotateCustomAxisButton.UseVisualStyleBackColor = true;
            RotateCustomAxisButton.Click += RotateCustomAxisButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(10, 457);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 33;
            label8.Text = "Градусы";
            // 
            // degreeCustom
            // 
            degreeCustom.Location = new System.Drawing.Point(76, 453);
            degreeCustom.Margin = new Padding(4, 3, 4, 3);
            degreeCustom.Name = "degreeCustom";
            degreeCustom.Size = new Size(50, 23);
            degreeCustom.TabIndex = 32;
            degreeCustom.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(10, 366);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(50, 15);
            label9.TabIndex = 38;
            label9.Text = "Прямая";
            // 
            // z2textBox
            // 
            z2textBox.Location = new System.Drawing.Point(192, 420);
            z2textBox.Margin = new Padding(4, 3, 4, 3);
            z2textBox.Name = "z2textBox";
            z2textBox.Size = new Size(37, 23);
            z2textBox.TabIndex = 51;
            z2textBox.Text = "1";
            // 
            // y2textBox
            // 
            y2textBox.Location = new System.Drawing.Point(115, 420);
            y2textBox.Margin = new Padding(4, 3, 4, 3);
            y2textBox.Name = "y2textBox";
            y2textBox.Size = new Size(37, 23);
            y2textBox.TabIndex = 50;
            y2textBox.Text = "1";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(162, 423);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(18, 15);
            label10.TabIndex = 49;
            label10.Text = "z2";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(85, 423);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(19, 15);
            label11.TabIndex = 48;
            label11.Text = "y2";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(10, 423);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(19, 15);
            label12.TabIndex = 47;
            label12.Text = "x2";
            // 
            // x2textBox
            // 
            x2textBox.Location = new System.Drawing.Point(41, 420);
            x2textBox.Margin = new Padding(4, 3, 4, 3);
            x2textBox.Name = "x2textBox";
            x2textBox.Size = new Size(37, 23);
            x2textBox.TabIndex = 46;
            x2textBox.Text = "1";
            // 
            // z1textBox
            // 
            z1textBox.Location = new System.Drawing.Point(192, 389);
            z1textBox.Margin = new Padding(4, 3, 4, 3);
            z1textBox.Name = "z1textBox";
            z1textBox.Size = new Size(37, 23);
            z1textBox.TabIndex = 44;
            z1textBox.Text = "0";
            // 
            // y1textBox
            // 
            y1textBox.Location = new System.Drawing.Point(115, 389);
            y1textBox.Margin = new Padding(4, 3, 4, 3);
            y1textBox.Name = "y1textBox";
            y1textBox.Size = new Size(37, 23);
            y1textBox.TabIndex = 43;
            y1textBox.Text = "0";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(162, 392);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(18, 15);
            label13.TabIndex = 42;
            label13.Text = "z1";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(85, 392);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(19, 15);
            label14.TabIndex = 41;
            label14.Text = "y1";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(10, 392);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(19, 15);
            label15.TabIndex = 40;
            label15.Text = "x1";
            // 
            // x1textBox
            // 
            x1textBox.Location = new System.Drawing.Point(41, 389);
            x1textBox.Margin = new Padding(4, 3, 4, 3);
            x1textBox.Name = "x1textBox";
            x1textBox.Size = new Size(37, 23);
            x1textBox.TabIndex = 39;
            x1textBox.Text = "0";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "XY", "XZ", "YZ" });
            comboBox2.Location = new System.Drawing.Point(68, 74);
            comboBox2.Margin = new Padding(4, 3, 4, 3);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(84, 23);
            comboBox2.TabIndex = 52;
            comboBox2.Text = "XY";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "X", "Y", "Z" });
            comboBox3.Location = new System.Drawing.Point(58, 292);
            comboBox3.Margin = new Padding(4, 3, 4, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(94, 23);
            comboBox3.TabIndex = 53;
            comboBox3.Text = "X";
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(10, 516);
            saveButton.Margin = new Padding(4, 3, 4, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(98, 27);
            saveButton.TabIndex = 54;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // loadButton
            // 
            loadButton.Location = new System.Drawing.Point(10, 549);
            loadButton.Margin = new Padding(4, 3, 4, 3);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(98, 27);
            loadButton.TabIndex = 55;
            loadButton.Text = "Загрузить";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(1429, 9);
            label16.Name = "label16";
            label16.Size = new Size(106, 15);
            label16.TabIndex = 56;
            label16.Text = "Фигура вращения";
            // 
            // figureRotButton
            // 
            figureRotButton.Location = new System.Drawing.Point(1429, 27);
            figureRotButton.Name = "figureRotButton";
            figureRotButton.Size = new Size(75, 23);
            figureRotButton.TabIndex = 57;
            figureRotButton.Text = "Выбрать";
            figureRotButton.UseVisualStyleBackColor = true;
            figureRotButton.Click += figureRotButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1684, 710);
            Controls.Add(figureRotButton);
            Controls.Add(label16);
            Controls.Add(loadButton);
            Controls.Add(saveButton);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(z2textBox);
            Controls.Add(y2textBox);
            Controls.Add(label10);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(x2textBox);
            Controls.Add(z1textBox);
            Controls.Add(y1textBox);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(label15);
            Controls.Add(x1textBox);
            Controls.Add(label9);
            Controls.Add(RotateCustomAxisButton);
            Controls.Add(label8);
            Controls.Add(degreeCustom);
            Controls.Add(isometricRadioButtom);
            Controls.Add(perspectiveRadioButtom);
            Controls.Add(clearButton);
            Controls.Add(RotateAxisButton);
            Controls.Add(label7);
            Controls.Add(degreeTextBox);
            Controls.Add(scaleButton);
            Controls.Add(sZtextBox);
            Controls.Add(sYtextBox);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(sXtextBox);
            Controls.Add(shiftButton);
            Controls.Add(cZtextBox);
            Controls.Add(cYtextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cXtextBox);
            Controls.Add(mirrorButton);
            Controls.Add(AxisCheckBox);
            Controls.Add(button1);
            Controls.Add(comboBox1);
            Controls.Add(pictureBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "триДЭ";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private ComboBox comboBox1;
        private Button button1;
        private CheckBox AxisCheckBox;
        private Button mirrorButton;
        private TextBox cXtextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox cYtextBox;
        private TextBox cZtextBox;
        private Button shiftButton;
        private Button scaleButton;
        private TextBox sZtextBox;
        private TextBox sYtextBox;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox sXtextBox;
        private TextBox degreeTextBox;
        private Label label7;
        private Button RotateAxisButton;
        private Button clearButton;
        private RadioButton perspectiveRadioButtom;
        private RadioButton isometricRadioButtom;
        private Button RotateCustomAxisButton;
        private Label label8;
        private TextBox degreeCustom;
        private Label label9;
        private TextBox z2textBox;
        private TextBox y2textBox;
        private Label label10;
        private Label label11;
        private Label label12;
        private TextBox x2textBox;
        private TextBox z1textBox;
        private TextBox y1textBox;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox x1textBox;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Button saveButton;
        private Button loadButton;
        private Label label16;
        private Button figureRotButton;
    }
}

