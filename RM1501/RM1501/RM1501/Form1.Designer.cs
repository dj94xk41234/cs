
namespace RM1501
{
    partial class Form_main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.cmb_comport = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_connectDev = new System.Windows.Forms.Button();
            this.btn_disconnectDev = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chart_realtime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txt_display = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_realtime)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_comport
            // 
            this.cmb_comport.FormattingEnabled = true;
            this.cmb_comport.Location = new System.Drawing.Point(89, 19);
            this.cmb_comport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_comport.Name = "cmb_comport";
            this.cmb_comport.Size = new System.Drawing.Size(111, 23);
            this.cmb_comport.TabIndex = 0;
            this.cmb_comport.Click += new System.EventHandler(this.cmb_comport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "comport";
            // 
            // btn_connectDev
            // 
            this.btn_connectDev.Location = new System.Drawing.Point(209, 16);
            this.btn_connectDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_connectDev.Name = "btn_connectDev";
            this.btn_connectDev.Size = new System.Drawing.Size(100, 29);
            this.btn_connectDev.TabIndex = 2;
            this.btn_connectDev.Text = "connect";
            this.btn_connectDev.UseVisualStyleBackColor = true;
            this.btn_connectDev.Click += new System.EventHandler(this.btn_connectDev_Click);
            // 
            // btn_disconnectDev
            // 
            this.btn_disconnectDev.Location = new System.Drawing.Point(317, 16);
            this.btn_disconnectDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_disconnectDev.Name = "btn_disconnectDev";
            this.btn_disconnectDev.Size = new System.Drawing.Size(100, 29);
            this.btn_disconnectDev.TabIndex = 2;
            this.btn_disconnectDev.Text = "disconnect";
            this.btn_disconnectDev.UseVisualStyleBackColor = true;
            this.btn_disconnectDev.Click += new System.EventHandler(this.btn_disconnectDev_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_disconnectDev);
            this.panel1.Controls.Add(this.cmb_comport);
            this.panel1.Controls.Add(this.btn_connectDev);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1261, 61);
            this.panel1.TabIndex = 3;
            // 
            // chart_realtime
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_realtime.ChartAreas.Add(chartArea1);
            this.chart_realtime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chart_realtime.Legends.Add(legend1);
            this.chart_realtime.Location = new System.Drawing.Point(346, 61);
            this.chart_realtime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart_realtime.Name = "chart_realtime";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_realtime.Series.Add(series1);
            this.chart_realtime.Size = new System.Drawing.Size(915, 610);
            this.chart_realtime.TabIndex = 4;
            this.chart_realtime.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "RM1501_chart";
            this.chart_realtime.Titles.Add(title1);
            // 
            // txt_display
            // 
            this.txt_display.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_display.Location = new System.Drawing.Point(0, 61);
            this.txt_display.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_display.Multiline = true;
            this.txt_display.Name = "txt_display";
            this.txt_display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_display.Size = new System.Drawing.Size(346, 610);
            this.txt_display.TabIndex = 5;
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 671);
            this.Controls.Add(this.chart_realtime);
            this.Controls.Add(this.txt_display);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form_main";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_realtime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_comport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_connectDev;
        private System.Windows.Forms.Button btn_disconnectDev;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_realtime;
        private System.Windows.Forms.TextBox txt_display;
    }
}

