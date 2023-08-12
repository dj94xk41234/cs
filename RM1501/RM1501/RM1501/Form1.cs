using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using RM1501.mylib;
namespace RM1501
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
        }
        SerialPort dev_serialport = new SerialPort();
        ChartArea chartArea_A = new ChartArea();
        Series series_A = new Series();
        Thread thread_read;
        Thread thread_updateForm;
        Thread thread_test;

        bool isRun_thread_read;
        bool isRun_thread_updateForm;
        bool isRun_thread_test;

        RM1501_dev rm1501_Dev = new RM1501_dev();

        bool isRead_newData;
        double read_value;
        List<double> list_read_Value = new List<double>();
        List<string> list_read_time = new List<string>();
        int point_idx = 1;

        int moveChart_display_update = 1;
        int moveChart_display_pointNum = 60;
        List<double> list_AxisY_read_Value = new List<double>();
        List<string> list_AxisX_read_time = new List<string>();

        Random random = new Random();

        private void th_test(object obj)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(500);
                    if (!isRun_thread_test)
                    {
                        return;
                    }
                    read_value = random.NextDouble() * 10;
                    isRead_newData = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void th_read(object obj)
        {

            int errorCount = 0;
            int bufferSize = 0;
            byte[] buffer_ByteArray;
            byte[] Dev_byteArray = new byte[10];
            int captureLength_correct_format = 0; //錯誤為-1，正常回傳長度
            bool isParseData;
            while (true)
            {
                try
                {
                    Thread.Sleep(1);
                    if (!isRun_thread_read)
                    {
                        return;
                    }
                    bufferSize = dev_serialport.BytesToRead;
                    buffer_ByteArray = new byte[bufferSize];
                    if (bufferSize < 10)
                    {
                        continue;
                    }
                    dev_serialport.Read(buffer_ByteArray, 0, bufferSize);
                    captureLength_correct_format = rm1501_Dev.captureByteList(buffer_ByteArray, Dev_byteArray);
                    if (captureLength_correct_format == -1)
                    {
                        continue;
                    }

                    isParseData = rm1501_Dev.parseByteData(Dev_byteArray, 10);
                    if (isParseData)//解析成功
                    {
                        read_value = rm1501_Dev.readingValue;
                        isRead_newData = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    if (errorCount++ > 3)
                    {
                        InvokeIfRequire(btn_disconnectDev, action: () =>
                        {
                            btn_disconnectDev_Click(btn_disconnectDev, null);
                        });
                        return;
                    }
                }
            }
        }

        private void th_updateForm(object obj)
        {
            DateTime dt = new DateTime();
            string csv_line = "";
            while (true)
            {
                try
                {
                    Thread.Sleep(1);
                    if (!isRun_thread_updateForm)
                    {
                        return;
                    }
                    if (isRead_newData)
                    {
                        dt = DateTime.Now;
                        list_read_Value.Add(read_value);
                        list_read_time.Add((dt.ToString("ss.ffff")));
                        csv_line = point_idx.ToString() + "\t" + read_value.ToString("F3") + "\t" + dt.ToString("HH:mm:ss.ffff");
                        point_idx++;
                        isRead_newData = false;
                        InvokeIfRequire(txt_display, action: () =>
                        {
                            AddTextToTextBox(txt_display, csv_line);
                        });
                    }
                    if (list_read_Value.Count >= moveChart_display_update)
                    {
                        list_AxisY_read_Value.AddRange(list_read_Value);
                        list_AxisX_read_time.AddRange(list_read_time);
                        list_read_time.Clear();
                        list_read_Value.Clear();
                    }
                    if (list_AxisY_read_Value.Count >= (moveChart_display_update + moveChart_display_pointNum))
                    {
                        list_AxisY_read_Value.RemoveRange(0, moveChart_display_update);
                        list_AxisX_read_time.RemoveRange(0, moveChart_display_update);

                    }
                    InvokeIfRequire(chart_realtime, action: () =>
                     {
                         chart_realtime.Series[series_A.Name].Points.DataBindXY(list_AxisX_read_time, list_AxisY_read_Value);
                        // chartArea_A.AxisY .Title = rm1501_Dev.str_unit;

                     });
                }
                catch (Exception ex)
                {`
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void initControl()
        {
            txt_display.Text = "Index\tValue\tDateTime";
        }

        private void initChart()
        {
            chart_realtime.ChartAreas.Clear();
            chart_realtime.Series.Clear();

            chartArea_A = new ChartArea("realtime");
            chartArea_A.AxisX.Title = "time(s)";

            chart_realtime.ChartAreas.Add(chartArea_A);

            series_A = new Series("RM1501");
            series_A.ChartType = SeriesChartType.FastLine;
            series_A.XValueType = ChartValueType.Double;
            series_A.YValueType = ChartValueType.Double;
            chart_realtime.Series.Add(series_A);

            chart_realtime.Titles[0].Text = "RM1501_Chart";
        }
        private void findComport(ComboBox combo)
        {
            combo.Items.Clear();
            combo.Text = "";
            combo.Items.AddRange(SerialPort.GetPortNames());
            combo.SelectedIndex = -1;
        }

        private void closeSerialport(SerialPort port)
        {
            try
            {
                port.Close();
                port.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool openSerialport(SerialPort port)
        {
            try
            {
                port.PortName = cmb_comport.Text;
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.StopBits = StopBits.One;
                port.Parity = Parity.None;
                if (!port.IsOpen)
                {
                    port.Open();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public void AddTextToTextBox(TextBox txt, string t)
        {
            txt.Text += "\r\n" + t;
            //txt.Focus();
            txt.Select(txt.TextLength, 0);
            txt.ScrollToCaret();
        }
        public void InvokeIfRequire(Control control, System.Windows.Forms.MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else { action(); }
        }

        ///event 
        private void cmb_comport_Click(object sender, EventArgs e)
        {
            findComport((ComboBox)sender);
        }

        private void btn_connectDev_Click(object sender, EventArgs e)
        {
            if (openSerialport(dev_serialport))
            {
                initControl();
                btn_connectDev.Enabled = false;
                btn_disconnectDev.Enabled = true;
                initChart();

                //isRun_thread_test = true;
                //thread_read = new Thread(th_test);
                //thread_read.Name = "th_read";
                //thread_read.IsBackground = true;
                //thread_read.Start(null);
                
                isRun_thread_read = true;
                thread_read = new Thread(th_read);
                thread_read.Name = "th_read";
                thread_read.IsBackground = true;
                thread_read.Start(null);

                isRun_thread_updateForm = true;
                thread_updateForm = new Thread(th_updateForm);
                thread_updateForm.Name = "th_updateForm";
                thread_updateForm.IsBackground = true;
                thread_updateForm.Start(null);
            }
        }

        private void btn_disconnectDev_Click(object sender, EventArgs e)
        {
            closeSerialport(dev_serialport);
            btn_connectDev.Enabled = true;

            isRun_thread_read = false;
            isRun_thread_test = false;
            isRun_thread_updateForm = false;
            Thread.Sleep(500);
        }
    }
}
