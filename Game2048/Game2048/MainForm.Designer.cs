namespace Game2048
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
            this.btn_init = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btn_pause = new System.Windows.Forms.Button();
            this.speedLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moveNumberLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.estimatedScoreLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_init
            // 
            this.btn_init.Location = new System.Drawing.Point(13, 3);
            this.btn_init.Name = "btn_init";
            this.btn_init.Size = new System.Drawing.Size(75, 23);
            this.btn_init.TabIndex = 0;
            this.btn_init.Text = "Start";
            this.btn_init.UseVisualStyleBackColor = true;
            this.btn_init.Click += new System.EventHandler(this.btn_init_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(13, 32);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(510, 501);
            this.webBrowser1.TabIndex = 1;
            // 
            // btn_pause
            // 
            this.btn_pause.Location = new System.Drawing.Point(94, 3);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(75, 23);
            this.btn_pause.TabIndex = 3;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = true;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(245, 62);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(74, 31);
            this.speedLabel.TabIndex = 4;
            this.speedLabel.Text = "1232";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(245, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(120, 31);
            this.timeLabel.TabIndex = 5;
            this.timeLabel.Text = "00:00:00";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.speedLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.moveNumberLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.estimatedScoreLabel, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(529, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(379, 271);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Time elapsed:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 31);
            this.label2.TabIndex = 7;
            this.label2.Text = "Moves per minute:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "Move number:";
            // 
            // moveNumberLabel
            // 
            this.moveNumberLabel.AutoSize = true;
            this.moveNumberLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveNumberLabel.Location = new System.Drawing.Point(245, 31);
            this.moveNumberLabel.Name = "moveNumberLabel";
            this.moveNumberLabel.Size = new System.Drawing.Size(74, 31);
            this.moveNumberLabel.TabIndex = 9;
            this.moveNumberLabel.Text = "1232";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estimated score:";
            // 
            // estimatedScoreLabel
            // 
            this.estimatedScoreLabel.AutoSize = true;
            this.estimatedScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.estimatedScoreLabel.Location = new System.Drawing.Point(245, 93);
            this.estimatedScoreLabel.Name = "estimatedScoreLabel";
            this.estimatedScoreLabel.Size = new System.Drawing.Size(74, 31);
            this.estimatedScoreLabel.TabIndex = 11;
            this.estimatedScoreLabel.Text = "1232";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 545);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btn_init);
            this.Name = "MainForm";
            this.Text = "2048 bot";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_init;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label moveNumberLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label estimatedScoreLabel;
    }
}

