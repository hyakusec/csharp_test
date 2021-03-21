        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //コントロール内にドラッグされたとき実行される
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
                e.Effect = DragDropEffects.Copy;
            else
                //ファイル以外は受け付けない
                e.Effect = DragDropEffects.None;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string dirPath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
            if (!Directory.Exists(dirPath))
            {
                return;
            }
            DirectoryInfo di = new DirectoryInfo(dirPath);
            FileInfo[] fis = di.GetFiles("*", SearchOption.AllDirectories);
            foreach(FileInfo fi in fis)
            {
                if (fi.Name.IndexOf('_') < 0)
                {
                    continue;
                }
                if (Path.GetExtension(fi.Name) != ".txt")
                {
                    continue;
                }
                string fn = Path.GetFileNameWithoutExtension(fi.Name);
                string unix = fn.Split('_')[1];
                DateTime dt = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(unix)).UtcDateTime;
                Trace.WriteLine($"{fi.FullName},{dt.ToString("yyyy/MM/dd HH:mm:ss.fff")}");
            }
        }
    }
