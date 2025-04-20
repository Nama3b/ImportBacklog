using System.Windows.Forms;

namespace BacklogImportData
{
    partial class MainForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.searchUser = new System.Windows.Forms.Button();
            this.selectUser = new System.Windows.Forms.ComboBox();
            this.selectProject = new System.Windows.Forms.ComboBox();
            this.openFileExcel = new System.Windows.Forms.Button();
            this.submit = new System.Windows.Forms.Button();
            this.displayFileName = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.showImportLog = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.clearData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(551, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(0, 0);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 2;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFileExcel";
            // 
            // searchUser
            // 
            this.searchUser.Location = new System.Drawing.Point(397, 49);
            this.searchUser.Name = "searchUser";
            this.searchUser.Size = new System.Drawing.Size(97, 21);
            this.searchUser.TabIndex = 0;
            this.searchUser.Text = "ユーザー検索";
            this.searchUser.UseVisualStyleBackColor = true;
            this.searchUser.Click += new System.EventHandler(this.searchUser_Click);
            // 
            // selectUser
            // 
            this.selectUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.selectUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.selectUser.FormattingEnabled = true;
            this.selectUser.Location = new System.Drawing.Point(84, 49);
            this.selectUser.Name = "selectUser";
            this.selectUser.Size = new System.Drawing.Size(307, 21);
            this.selectUser.TabIndex = 5;
            // 
            // selectProject
            // 
            this.selectProject.FormattingEnabled = true;
            this.selectProject.Location = new System.Drawing.Point(84, 110);
            this.selectProject.Name = "selectProject";
            this.selectProject.Size = new System.Drawing.Size(307, 21);
            this.selectProject.TabIndex = 6;
            // 
            // openFileExcel
            // 
            this.openFileExcel.Location = new System.Drawing.Point(397, 173);
            this.openFileExcel.Name = "openFileExcel";
            this.openFileExcel.Size = new System.Drawing.Size(97, 21);
            this.openFileExcel.TabIndex = 7;
            this.openFileExcel.Text = "参考";
            this.openFileExcel.UseVisualStyleBackColor = true;
            this.openFileExcel.Click += new System.EventHandler(this.openFileExcel_Click);
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(151, 405);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(124, 30);
            this.submit.TabIndex = 10;
            this.submit.Text = "登録";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // displayFileName
            // 
            this.displayFileName.Location = new System.Drawing.Point(84, 174);
            this.displayFileName.Name = "displayFileName";
            this.displayFileName.Size = new System.Drawing.Size(307, 20);
            this.displayFileName.TabIndex = 11;
            this.displayFileName.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "1. アカウント 選択";
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Location = new System.Drawing.Point(180, 36);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(0, 13);
            this.userName.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "2. プロジェ外ト選択";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "3. 对象資料選選択";
            // 
            // showImportLog
            // 
            this.showImportLog.Location = new System.Drawing.Point(84, 237);
            this.showImportLog.Name = "showImportLog";
            this.showImportLog.Size = new System.Drawing.Size(307, 134);
            this.showImportLog.TabIndex = 17;
            this.showImportLog.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "ログデータの詳細";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(247, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 20);
            this.textBox1.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(231, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0, 20);
            this.textBox2.TabIndex = 20;
            // 
            // clearData
            // 
            this.clearData.Location = new System.Drawing.Point(312, 405);
            this.clearData.Name = "clearData";
            this.clearData.Size = new System.Drawing.Size(124, 30);
            this.clearData.TabIndex = 21;
            this.clearData.Text = "データをクリアします";
            this.clearData.UseVisualStyleBackColor = true;
            this.clearData.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 491);
            this.Controls.Add(this.clearData);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.showImportLog);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.displayFileName);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.openFileExcel);
            this.Controls.Add(this.selectProject);
            this.Controls.Add(this.selectUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.searchUser);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "課題登録";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button searchUser;
        private System.Windows.Forms.ComboBox selectUser;
        private System.Windows.Forms.ComboBox selectProject;
        private System.Windows.Forms.Button openFileExcel;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.RichTextBox displayFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox showImportLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private Button clearData;
    }
}

