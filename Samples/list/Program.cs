string setting_path = Environment.CurrentDirectory + "1.txt";
Console.WriteLine("文件路径\n:"+setting_path);
List<string> setting_homelist = File.ReadAllText(Environment.CurrentDirectory + "\\1.txt").Split("${@}").ToList();
StreamWriter sm1 = new StreamWriter(setting_path);
Console.WriteLine(":文件长度\n"+setting_homelist.Count);
string T1 = "";
Console.WriteLine(T1);
for (int T0 = 0; T0 != setting_homelist.Count; T0 += 1)
{
    if (T0 != 2)
    {
        Console.WriteLine(setting_homelist[T0]);
        T1 = T1 + setting_homelist[T0]+"${@}";
        Console.WriteLine(T1);
    }
    else if (T0 == 2)
    {
        T1 = T1+"我日你妈我日年末"+"${@}";
    }
}
sm1.Write(T1);
Console.WriteLine(T1);
sm1.Close();
