        //private void button1_Click(object sender, EventArgs e)
        //        {
        //            FileStream fileStream = File.OpenRead(textBox1.Text.Trim());
        //            Byte[] content = new Byte[fileStream.Length];
        //            fileStream.Read(content, 0, content.Length);
        //            fileStream.Close();

        //            System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(textBox1.Text.Trim());
        //            listBox1.Items.Add("��������" + ass.GetName().FullName);
        //            listBox1.Items.Add("�汾�ţ�" + ass.GetName().Version);
        //            listBox1.Items.Add("�汾�ţ�" + ass.GetName().VersionCompatibility);
                    
        //        }

        //private string GetCpuID()
        //{
        //    ManagementClass mc = new ManagementClass("Win32_Processor");
        //    ManagementObjectCollection moc = mc.GetInstances();

        //    String strCpuID = null;
        //    foreach (ManagementObject mo in moc)
        //    {
        //        strCpuID = mo.Properties["ProcessorId"].Value.ToString();
        //        break;
        //    }
        //    return strCpuID;   
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RegistryKey MyReg = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\SYSTEM\\CentralProcessor\\0");
        //        this.label1.Text = "CPUƵ�ʣ�" + MyReg.GetValue("~MHz").ToString() + "   MHz";
        //        this.label4.Text = "CPU��ʶ��" + MyReg.GetValue("Identifier").ToString();
        //        this.label3.Text = "CPU���ƣ�" + MyReg.GetValue("ProcessorNameString").ToString();
        //        this.label2.Text = "CPU��Ӧ�̣�" + MyReg.GetValue("VendorIdentifier").ToString();

        //        this.label16.Text = "CPU���кţ�" + GetCpuID();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("��ȡע�����Ϣ��������", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //��ȡ����ϵͳ�İ汾��Ϣ   
        //    Process MyProcess = new Process();
        //    //�趨������   
        //    MyProcess.StartInfo.FileName = "cmd.exe";
        //    //�ر�Shell��ʹ��   
        //    MyProcess.StartInfo.UseShellExecute = false;
        //    //�ض����׼����   
        //    MyProcess.StartInfo.RedirectStandardInput = true;
        //    //�ض����׼���   
        //    MyProcess.StartInfo.RedirectStandardOutput = true;
        //    //�ض���������   
        //    MyProcess.StartInfo.RedirectStandardError = true;
        //    //���ò���ʾ����   
        //    MyProcess.StartInfo.CreateNoWindow = true;
        //    //ִ��VER����   
        //    MyProcess.Start();
        //    MyProcess.StandardInput.WriteLine("Ver");
        //    MyProcess.StandardInput.WriteLine("exit");
        //    //���������ȡ����ִ�н����   
        //    string StrInfo = MyProcess.StandardOutput.ReadToEnd();
        //    this.textBox1.Text = StrInfo.Substring(0, StrInfo.IndexOf("Corp.") + 5);
        //}

        //string driveSerialNum = "";

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Scripting.FileSystemObjectClass MySystem = new Scripting.FileSystemObjectClass();
        //    foreach (Scripting.Drive MyDriver in MySystem.Drives)
        //    {
        //        string DriveLetter = "";
        //        string DriveType = "";
        //        string VolumeName = "";
        //        string SerialNumber = "";
        //        string FileSystem = "";
        //        try
        //        {
        //            DriveLetter = MyDriver.DriveLetter.ToString();
        //            DriveType = MyDriver.DriveType.ToString();
        //            VolumeName = MyDriver.VolumeName;
        //            SerialNumber = MyDriver.SerialNumber.ToString();
        //            FileSystem = MyDriver.FileSystem;
        //            driveSerialNum = driveSerialNum + SerialNumber;

        //        }
        //        catch (Exception Err)
        //        {
        //        }
        //        string[] SubItems = { DriveLetter, DriveType, VolumeName, SerialNumber, FileSystem };
        //        ListViewItem MyItem = new ListViewItem(SubItems);
        //        this.listView1.Items.Add(MyItem);
        //        label17.Text = "�����Ӳ�̵ı�ţ�" + driveSerialNum;
        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    //��ȡϵͳ��Ϣ   
        //    try
        //    {
        //        this.label14.Text = "�û�����" + SystemInformation.UserName;
        //        this.label13.Text = "���������" + SystemInformation.ComputerName;
        //        this.label12.Text = "����ϵͳ��" + Environment.OSVersion.Platform;
        //        this.label11.Text = "�汾�ţ�" + Environment.OSVersion.Version;
        //        this.label15.Text = "NetBIOS�����ƣ�" + Environment.MachineName;
        //        if (SystemInformation.BootMode.ToString() == "Normal")
        //            this.label7.Text = "������ʽ����������";
        //        if (SystemInformation.BootMode.ToString() == "FailSafe")
        //            this.label7.Text = "������ʽ����ȫ����";
        //        if (SystemInformation.BootMode.ToString() == "FailSafeWithNetwork")
        //            this.label7.Text = "������ʽ��ͨ�������������";
        //        if (SystemInformation.Network == true)
        //            this.label8.Text = "�������ӣ�������";
        //        else
        //            this.label8.Text = "�������ӣ�δ����";
        //        this.label9.Text = "��ʾ��������" + SystemInformation.MonitorCount.ToString();
        //        this.label10.Text = "��ʾ���ֱ��ʣ�" + SystemInformation.PrimaryMonitorMaximizedWindowSize.Width.ToString() + "X" +
        //                SystemInformation.PrimaryMonitorMaximizedWindowSize.Height.ToString();
        //    }
        //    catch (Exception Err)
        //    {
        //        MessageBox.Show("��ȡϵͳ��Ϣ��������", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //} 