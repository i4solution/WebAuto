﻿namespace BqUpdater
{
    partial class frmUpdate
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
            this.lblUpdateVer = new System.Windows.Forms.Label();
            this.lblCurrentVer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDetail = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label0 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUpdateVer);
            this.panel1.Controls.Add(this.lblCurrentVer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDetail);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.picIcon);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.label0);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Location = new System.Drawing.Point(10, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 256);
            this.panel1.TabIndex = 2;
            // 
            // lblUpdateVer
            // 
            this.lblUpdateVer.AutoSize = true;
            this.lblUpdateVer.BackColor = System.Drawing.SystemColors.Control;
            this.lblUpdateVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateVer.Location = new System.Drawing.Point(123, 32);
            this.lblUpdateVer.Name = "lblUpdateVer";
            this.lblUpdateVer.Size = new System.Drawing.Size(13, 18);
            this.lblUpdateVer.TabIndex = 29;
            this.lblUpdateVer.Text = "-";
            this.lblUpdateVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCurrentVer
            // 
            this.lblCurrentVer.AutoSize = true;
            this.lblCurrentVer.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentVer.Location = new System.Drawing.Point(123, 6);
            this.lblCurrentVer.Name = "lblCurrentVer";
            this.lblCurrentVer.Size = new System.Drawing.Size(13, 18);
            this.lblCurrentVer.TabIndex = 28;
            this.lblCurrentVer.Text = "-";
            this.lblCurrentVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 27;
            this.label1.Text = "Chi tiết:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(20, 78);
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ReadOnly = true;
            this.txtDetail.Size = new System.Drawing.Size(315, 105);
            this.txtDetail.TabIndex = 26;
            this.txtDetail.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 18);
            this.label2.TabIndex = 25;
            this.label2.Text = "Bản nâng cấp:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picIcon
            // 
            this.picIcon.Image = global::BqUpdater.Properties.Resources.rect286_3;
            this.picIcon.Location = new System.Drawing.Point(378, 19);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(75, 74);
            this.picIcon.TabIndex = 22;
            this.picIcon.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(190, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 52);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Không";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(478, 3);
            this.label14.TabIndex = 19;
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(362, 197);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(91, 52);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.BackColor = System.Drawing.SystemColors.Control;
            this.label0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label0.Location = new System.Drawing.Point(17, 6);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(88, 18);
            this.label0.TabIndex = 17;
            this.label0.Text = "Bản hiện tại:";
            this.label0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Lime;
            this.lblInfo.Location = new System.Drawing.Point(346, 143);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(138, 24);
            this.lblInfo.TabIndex = 24;
            this.lblInfo.Text = "THÀNH CÔNG";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblInfo.Visible = false;
            // 
            // frmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 272);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmUpdate";
            this.ShowIcon = false;
            this.Text = "Web Auto GoLike - Update Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCurrentVer;
        private System.Windows.Forms.Label lblUpdateVer;
    }
}

