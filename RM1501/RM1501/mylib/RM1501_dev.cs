using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace RM1501.mylib
{
    class RM1501_dev
    {

        public byte data_start = 0x0d;//開頭字元
        public byte data_end = 0x0d;//結束字元 未用到
        public byte data_len = 10;//封包長度
        public List<byte> list_unalayed_byteData = new List<byte>();//未分析data
        public List<byte> list_alayed_byteData = new List<byte>();//完整擷取後的data

        public double gain_decimal = 1; //小數點冪數
        public BitArray bitArray_status1; //目前狀態旗標
        public string str_unit = "rpm";//單位
        public string measureMethod = "normal";//量測方法(功能)
        public BitArray bitArray_status2;//目前狀態旗標
        public byte[] ByteArray_readingValue;// byte of reading in binary formatk
        public double readingValue = 0.0;//目前讀值

        public RM1501_dev(byte dataStart = 0x0d, byte dataLen = 10)
        {
            data_start = data_start;
            data_len = dataLen;
        }

        public int captureByteList(byte[] source, byte[] result)
        {

            int source_length = list_unalayed_byteData.Count;
            int idx_start, idx_end, len;

            if (source_length > data_len * 5)
            {
                list_unalayed_byteData.RemoveRange(0, data_len * 3);
            }
            list_unalayed_byteData.AddRange(source.ToList());

            idx_end = list_unalayed_byteData.LastIndexOf(data_start);
            if (idx_end == 0)
            {
                return -1;
            }
            idx_start = list_unalayed_byteData.LastIndexOf(data_start, idx_end - 1);
            if (idx_start == -1 || idx_end == -1)
            {
                return -1;
            }
            len = idx_end - idx_start;
            if (len == data_len && idx_end > idx_start)
            {
                list_unalayed_byteData.CopyTo(idx_start, result, 0, data_len);
                list_unalayed_byteData.RemoveRange(0, idx_end);
                list_alayed_byteData = result.ToList();
                return len;
            }
            else
            {
                return -1;
            }
        }

        public bool parseByteData(byte[] source, int chkLength = 10)
        {
            try
            {
                if (source[0] != data_start && source.Length != chkLength)
                {
                    return false;
                }

                byte[] byteArray_Temp = new byte[1];
                //小數點
                byteArray_Temp = new byte[1];
                Array.Copy(source, 1, byteArray_Temp, 0, 1);
                parse_DecimalPoint(byteArray_Temp);

                //讀值
                byteArray_Temp = new byte[4];
                Array.Copy(source, 6, byteArray_Temp, 0, 4);
                ByteArray_readingValue = byteArray_Temp;
                parse_readingValue(ByteArray_readingValue);

                //單位
                byteArray_Temp = new byte[1];
                Array.Copy(source, 3, byteArray_Temp, 0, 1);
                parse_unit(byteArray_Temp);

                //量測方式
                byteArray_Temp = new byte[1];
                Array.Copy(source, 4, byteArray_Temp, 0, 1);
                parse_measureMethod(byteArray_Temp);

                //狀態1
                byteArray_Temp = new byte[1];
                Array.Copy(source, 2, byteArray_Temp, 0, 1);
                bitArray_status1 = new BitArray(byteArray_Temp);
                //狀態2
                byteArray_Temp = new byte[1];
                Array.Copy(source, 5, byteArray_Temp, 0, 1);
                bitArray_status2 = new BitArray(byteArray_Temp);

                return true;

            }
            catch (Exception ex)
            {

                throw;
            }

        }
        /// <summary>
        /// 解析小數點
        /// </summary>
        /// <param name="byteArray"></param>
        public void parse_DecimalPoint(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            if (bitArray[0])
            {
                gain_decimal = 0.1;
            }
            else if (bitArray[1])
            {
                gain_decimal = 0.01;
            }
            else if (bitArray[2])
            {
                gain_decimal = 0.001;
            }
            else
            {
                gain_decimal = 1;
            }
        }
        /// <summary>
        /// 解析數字
        /// </summary>
        /// <param name="byteArray"></param>
        public void parse_readingValue(byte[] byteArray)
        {
            int value = Convert.ToInt32(byteArray[2]) * 65536 + Convert.ToInt32(byteArray[1] )* 256 + Convert.ToInt32(byteArray[0]);
            readingValue = value * gain_decimal;
        }

        /// <summary>
        /// 解析單位
        /// </summary>
        /// <param name="byteArray"></param>
        public void parse_unit(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            if (bitArray[0])
            {
                str_unit = "rpm";
            }
            else if (bitArray[1])
            {
                str_unit = "m/min";
            }
            else if (bitArray[2])
            {
                str_unit = "ft/min";
            }
            else if (bitArray[3])
            {
                str_unit = "yd/min";
            }
            else if (bitArray[4])
            {
                str_unit = "rps";
            }
            else if (bitArray[5])
            {
                str_unit = "counter with external light source,";
            }
            else if (bitArray[6])
            {
                str_unit = "counter without external light source";
            }
        }
        /// <summary>
        /// 量測方式
        /// </summary>
        /// <param name="byteArray"></param>
        public void parse_measureMethod(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            if (bitArray[0])
            {
                measureMethod = "normal";
            }
            else if (bitArray[1])
            {
                measureMethod = "max";
            }
            else if (bitArray[2])
            {
                measureMethod = "min";
            }
            else if (bitArray[3])
            {
                measureMethod = "average";
            }

        }
    }
}
