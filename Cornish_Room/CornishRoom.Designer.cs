namespace laba7
{
    partial class CornishRoom
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
            pictureBox1 = new PictureBox();
            createRoomButton = new Button();
            checkBox1 = new CheckBox();
            reflectCheck = new CheckBox();
            wallComboBox = new ComboBox();
            transparencyCheck = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(536, 494);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // createRoomButton
            // 
            createRoomButton.Location = new System.Drawing.Point(12, 537);
            createRoomButton.Name = "createRoomButton";
            createRoomButton.Size = new Size(75, 23);
            createRoomButton.TabIndex = 1;
            createRoomButton.Text = "Построить";
            createRoomButton.UseVisualStyleBackColor = true;
            createRoomButton.Click += createRoomButton_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(93, 541);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(138, 19);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Зеркальность стены";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // reflectCheck
            // 
            reflectCheck.AutoSize = true;
            reflectCheck.Location = new System.Drawing.Point(373, 541);
            reflectCheck.Name = "reflectCheck";
            reflectCheck.Size = new Size(102, 19);
            reflectCheck.TabIndex = 3;
            reflectCheck.Text = "Зеркальность";
            reflectCheck.UseVisualStyleBackColor = true;
            // 
            // wallComboBox
            // 
            wallComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            wallComboBox.Items.AddRange(new object[] { "Правая", "Левая", "Верхняя", "Нижняя", "Дальняя" });
            wallComboBox.Location = new System.Drawing.Point(237, 537);
            wallComboBox.Name = "wallComboBox";
            wallComboBox.Size = new Size(121, 23);
            wallComboBox.TabIndex = 4;
            // 
            // transparencyCheck
            // 
            transparencyCheck.AutoSize = true;
            transparencyCheck.Location = new System.Drawing.Point(481, 541);
            transparencyCheck.Name = "transparencyCheck";
            transparencyCheck.Size = new Size(105, 19);
            transparencyCheck.TabIndex = 5;
            transparencyCheck.Text = "Прозрачность";
            transparencyCheck.UseVisualStyleBackColor = true;
            // 
            // CornishRoom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(597, 572);
            Controls.Add(transparencyCheck);
            Controls.Add(wallComboBox);
            Controls.Add(reflectCheck);
            Controls.Add(checkBox1);
            Controls.Add(createRoomButton);
            Controls.Add(pictureBox1);
            Name = "CornishRoom";
            Text = "CornishRoom";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button createRoomButton;
        private CheckBox checkBox1;
        private CheckBox reflectCheck;
        private ComboBox wallComboBox;
        private CheckBox transparencyCheck;
    }
}