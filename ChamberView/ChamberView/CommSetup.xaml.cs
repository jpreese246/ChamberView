using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Windows.Forms;
using System.IO.Ports;
using Modbus.Device;

namespace ChamberView
{
    /// <summary>
    /// Interaction logic for CommSetup.xaml
    /// </summary>
    public partial class CommSetup : Page
    {
        public static void ModbusSerialRtuMasterWriteRegisters(string baudBaud)
        {

            //MessageBox.Show(baudBaud);
            using (SerialPort port = new SerialPort(baudBaud))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                //ushort[] ReadHoldingRegisters(
                byte slaveId = 1;
                ushort startAddress = 300;
                ushort value = 480;
                master.WriteSingleRegister(slaveId, startAddress, value);
                ushort[] holding_register = master.ReadHoldingRegisters(slaveId, startAddress, 1);
            }

        }

        public CommSetup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string whif = comboBox1.Text;
            string whif = "COM6";
            ModbusSerialRtuMasterWriteRegisters(whif);
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
