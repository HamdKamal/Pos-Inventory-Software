
namespace Pos_Inventory_Software
{
    partial class frmDatabase
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnBac = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnRes = new System.Windows.Forms.Button();
            this.btnPste = new System.Windows.Forms.Button();
            this.TXT_PATH1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(12, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 28);
            this.panel1.TabIndex = 116;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Gill Sans MT", 14.25F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(-16, -19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 65);
            this.button1.TabIndex = 76;
            this.button1.Text = "[ خروج ]";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans MT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(522, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "إدارة قاعدة البيانات";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(12, 353);
            this.panel4.TabIndex = 119;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(689, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 353);
            this.panel3.TabIndex = 118;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(701, 10);
            this.panel2.TabIndex = 117;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.btnBac);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.txt_path);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(18, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(664, 151);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "إنشاء نسخة من قاعدة البيانات";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(439, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(200, 13);
            this.labelControl1.TabIndex = 100;
            this.labelControl1.Text = "الرجاء تحديد المسار لتخزين قاعدة البيانات ...";
            // 
            // btnBac
            // 
            this.btnBac.BackColor = System.Drawing.Color.SeaGreen;
            this.btnBac.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBac.FlatAppearance.BorderSize = 0;
            this.btnBac.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBac.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBac.ForeColor = System.Drawing.Color.White;
            this.btnBac.Location = new System.Drawing.Point(32, 104);
            this.btnBac.Name = "btnBac";
            this.btnBac.Size = new System.Drawing.Size(137, 30);
            this.btnBac.TabIndex = 99;
            this.btnBac.Text = "إنشاء النسخة الإحتياطية";
            this.btnBac.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBac.UseVisualStyleBackColor = false;
            this.btnBac.Click += new System.EventHandler(this.btnBac_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.btnCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopy.FlatAppearance.BorderSize = 0;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(32, 70);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCopy.Size = new System.Drawing.Size(137, 27);
            this.btnCopy.TabIndex = 94;
            this.btnCopy.Text = "... إختيار المسار ";
            this.btnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txt_path
            // 
            this.txt_path.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_path.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_path.Location = new System.Drawing.Point(175, 70);
            this.txt_path.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            this.txt_path.Size = new System.Drawing.Size(464, 27);
            this.txt_path.TabIndex = 93;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.btnRes);
            this.groupBox2.Controls.Add(this.btnPste);
            this.groupBox2.Controls.Add(this.TXT_PATH1);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(18, 195);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(664, 151);
            this.groupBox2.TabIndex = 121;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "إستعادة النسخة المخزنة من قاعدة البيانات";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(390, 46);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(249, 13);
            this.labelControl2.TabIndex = 102;
            this.labelControl2.Text = "الرجاء إختيار المسار الذي يحتوي على قاعدة البيانات ...";
            // 
            // btnRes
            // 
            this.btnRes.BackColor = System.Drawing.Color.SeaGreen;
            this.btnRes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRes.FlatAppearance.BorderSize = 0;
            this.btnRes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRes.ForeColor = System.Drawing.Color.White;
            this.btnRes.Location = new System.Drawing.Point(32, 100);
            this.btnRes.Name = "btnRes";
            this.btnRes.Size = new System.Drawing.Size(137, 30);
            this.btnRes.TabIndex = 101;
            this.btnRes.Text = "إنشاء النسخة الإحتياطية";
            this.btnRes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRes.UseVisualStyleBackColor = false;
            this.btnRes.Click += new System.EventHandler(this.btnRes_Click);
            // 
            // btnPste
            // 
            this.btnPste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(73)))), ((int)(((byte)(133)))));
            this.btnPste.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPste.FlatAppearance.BorderSize = 0;
            this.btnPste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPste.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPste.ForeColor = System.Drawing.Color.White;
            this.btnPste.Location = new System.Drawing.Point(32, 66);
            this.btnPste.Name = "btnPste";
            this.btnPste.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPste.Size = new System.Drawing.Size(137, 27);
            this.btnPste.TabIndex = 100;
            this.btnPste.Text = "... إختيار النسخة ";
            this.btnPste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPste.UseVisualStyleBackColor = false;
            this.btnPste.Click += new System.EventHandler(this.btnPste_Click);
            // 
            // TXT_PATH1
            // 
            this.TXT_PATH1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TXT_PATH1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_PATH1.Location = new System.Drawing.Point(175, 66);
            this.TXT_PATH1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PATH1.Name = "TXT_PATH1";
            this.TXT_PATH1.ReadOnly = true;
            this.TXT_PATH1.Size = new System.Drawing.Size(464, 27);
            this.TXT_PATH1.TabIndex = 94;
            // 
            // frmDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(701, 363);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDatabase";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDatabase";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.TextBox TXT_PATH1;
        public System.Windows.Forms.Button btnCopy;
        public System.Windows.Forms.Button btnBac;
        public System.Windows.Forms.Button btnRes;
        public System.Windows.Forms.Button btnPste;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}