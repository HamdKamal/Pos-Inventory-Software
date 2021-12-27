
namespace Pos_Inventory_Software.Report
{
    partial class Reporting
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.Caching.MemoryDocumentStorage memoryDocumentStorage1 = new DevExpress.XtraPrinting.Caching.MemoryDocumentStorage();
            this.documentViewer1 = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.cachedReportSource1 = new DevExpress.XtraPrinting.Caching.CachedReportSource(this.components);
            this.SuspendLayout();
            // 
            // documentViewer1
            // 
            this.documentViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentViewer1.DocumentSource = this.cachedReportSource1;
            this.documentViewer1.IsMetric = true;
            this.documentViewer1.Location = new System.Drawing.Point(0, 0);
            this.documentViewer1.Name = "documentViewer1";
            this.documentViewer1.Size = new System.Drawing.Size(1062, 586);
            this.documentViewer1.TabIndex = 0;
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // cachedReportSource1
            // 
            this.cachedReportSource1.Storage = memoryDocumentStorage1;
            // 
            // Reporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 586);
            this.Controls.Add(this.documentViewer1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.Name = "Reporting";
            this.Text = "Reporting";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.Preview.DocumentViewer documentViewer1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        public DevExpress.XtraPrinting.Caching.CachedReportSource cachedReportSource1;
    }
}