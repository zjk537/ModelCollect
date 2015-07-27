        //private void button1_Click(object sender, EventArgs e)
        //        {
        //            FileStream fileStream = File.OpenRead(textBox1.Text.Trim());
        //            Byte[] content = new Byte[fileStream.Length];
        //            fileStream.Read(content, 0, content.Length);
        //            fileStream.Close();

        //            System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(textBox1.Text.Trim());
        //            listBox1.Items.Add("程序集名：" + ass.GetName().FullName);
        //            listBox1.Items.Add("版本号：" + ass.GetName().Version);
        //            listBox1.Items.Add("版本号：" + ass.GetName().VersionCompatibility);
                    
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
        //        this.label1.Text = "CPU频率：" + MyReg.GetValue("~MHz").ToString() + "   MHz";
        //        this.label4.Text = "CPU标识：" + MyReg.GetValue("Identifier").ToString();
        //        this.label3.Text = "CPU名称：" + MyReg.GetValue("ProcessorNameString").ToString();
        //        this.label2.Text = "CPU供应商：" + MyReg.GetValue("VendorIdentifier").ToString();

        //        this.label16.Text = "CPU序列号：" + GetCpuID();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("读取注册表信息发生错误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //获取操作系统的版本信息   
        //    Process MyProcess = new Process();
        //    //设定程序名   
        //    MyProcess.StartInfo.FileName = "cmd.exe";
        //    //关闭Shell的使用   
        //    MyProcess.StartInfo.UseShellExecute = false;
        //    //重定向标准输入   
        //    MyProcess.StartInfo.RedirectStandardInput = true;
        //    //重定向标准输出   
        //    MyProcess.StartInfo.RedirectStandardOutput = true;
        //    //重定向错误输出   
        //    MyProcess.StartInfo.RedirectStandardError = true;
        //    //设置不显示窗口   
        //    MyProcess.StartInfo.CreateNoWindow = true;
        //    //执行VER命令   
        //    MyProcess.Start();
        //    MyProcess.StandardInput.WriteLine("Ver");
        //    MyProcess.StandardInput.WriteLine("exit");
        //    //从输出流获取命令执行结果，   
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
        //        label17.Text = "计算机硬盘的编号：" + driveSerialNum;
        //    }
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    //获取系统信息   
        //    try
        //    {
        //        this.label14.Text = "用户名：" + SystemInformation.UserName;
        //        this.label13.Text = "计算机名：" + SystemInformation.ComputerName;
        //        this.label12.Text = "操作系统：" + Environment.OSVersion.Platform;
        //        this.label11.Text = "版本号：" + Environment.OSVersion.Version;
        //        this.label15.Text = "NetBIOS的名称：" + Environment.MachineName;
        //        if (SystemInformation.BootMode.ToString() == "Normal")
        //            this.label7.Text = "启动方式：正常启动";
        //        if (SystemInformation.BootMode.ToString() == "FailSafe")
        //            this.label7.Text = "启动方式：安全启动";
        //        if (SystemInformation.BootMode.ToString() == "FailSafeWithNetwork")
        //            this.label7.Text = "启动方式：通过网络服务启动";
        //        if (SystemInformation.Network == true)
        //            this.label8.Text = "网络连接：已连接";
        //        else
        //            this.label8.Text = "网络连接：未连接";
        //        this.label9.Text = "显示器数量：" + SystemInformation.MonitorCount.ToString();
        //        this.label10.Text = "显示器分辨率：" + SystemInformation.PrimaryMonitorMaximizedWindowSize.Width.ToString() + "X" +
        //                SystemInformation.PrimaryMonitorMaximizedWindowSize.Height.ToString();
        //    }
        //    catch (Exception Err)
        //    {
        //        MessageBox.Show("获取系统信息发生错误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //} 