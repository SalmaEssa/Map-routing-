using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FibonacciHeaps;
using System.Diagnostics;


namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        int count = 0;
        string Fname;
        Dictionary<int, KeyValuePair<int, KeyValuePair<double, double>>> ans = new Dictionary<int, KeyValuePair<int, KeyValuePair<double, double>>>() ;
        int NumOfInter;
        int Q ;
        KeyValuePair<int, KeyValuePair<double, double>>[] arr;
        KeyValuePair<double, double>[] sourceCoordinates;
        KeyValuePair<double, double>[] destinationCoordinates;
        double[] R;
        int NumOfRoads ;
        KeyValuePair<KeyValuePair<int, int>, KeyValuePair<double, double>>[] roads ;
        double[] time ;
        Dictionary<int, List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>>> parameter = new Dictionary<int, List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>>>();
        KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>[] mohemgdn;
        public Form1()
        {
            InitializeComponent();
        }
        public void displlay(Dictionary<int, KeyValuePair<int, KeyValuePair<double, double>>> ans)
    { 
        
     MessageBox.Show((ans[-2].Value.Key*60).ToString()); 


    }
        public void fill()
        {

            var vip = new KeyValuePair<int, KeyValuePair<double, double>>(-1, new KeyValuePair<double, double>(0, 0));
            ans[-1] = vip;
            vip = new KeyValuePair<int, KeyValuePair<double, double>>(-10, new KeyValuePair<double, double>(9999999999999999999, 9999999999999999999));
            ans[-2] = vip;
        }
        public void min()
        {
            

            var heap = new FibonacciHeap<int, double>(0);
            var node = new FibonacciHeapNode<int, double>(-1, 0);

            heap.Insert(node);
            while (!heap.IsEmpty())
            {
                node = heap.Min();
                heap.RemoveMin();
                int n = node.Data;
                double timee = node.Key;
                if (n == -2) continue ; 
                var con = parameter[n];


                foreach (var item in con)
                {
                    //ListValueData.Add(item);
                    int n1 = item.Key; // el node el connected beha 
                    double   t1 = item.Value.Key; // el weight 3ala el edge

                    double oldtime = ans[n1].Value.Key;
                    double dist = item.Value.Value.Key;

                    if (t1 + timee < oldtime)
                    {

                        var vip = new
                             KeyValuePair<int, KeyValuePair<double, double>>(n, new KeyValuePair<double, double>(t1 + timee, dist));
                        ans[n1] = vip;
                        var node2 = new FibonacciHeapNode<int, double>(n1, t1 + timee);
                        heap.Insert(node2);

                    }

                }
            }
         

        }
        private void GetinfoFromMapFile ()
        {
           
            parameter.Clear();
            FileStream fs = new FileStream(textBox5.Text.ToString(), FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            NumOfInter = Convert.ToInt32(SR.ReadLine().ToString());
             arr = new KeyValuePair<int, KeyValuePair<double, double>>[NumOfInter];
            mohemgdn = new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>[NumOfInter];
            for (int i = 0; i < NumOfInter; i++)
            {
                string s = SR.ReadLine();
                string[] d = s.Split(' ');
                arr[i] = new KeyValuePair<int, KeyValuePair<double, double>>
                    (Convert.ToInt32(d[0]), new KeyValuePair<double, double>
                    (Convert.ToDouble(d[1]), Convert.ToDouble(d[2])));
                ans.Add(arr[i].Key, new KeyValuePair<int, KeyValuePair<double, double>>(-10, new KeyValuePair<double, double>
                    (9999999999999999999, 9999999999999999999)));
                mohemgdn[i] = new
                    KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                    (-1, new KeyValuePair<double, KeyValuePair<double, double>>
                    (0, new KeyValuePair<double, double>(0, 0)));
            }
            //T(N) = seta(N)+seta(1) where N is the number of inetrsection ID's
             NumOfRoads = Convert.ToInt32(SR.ReadLine().ToString());
             roads =  new KeyValuePair<KeyValuePair<int, int>, KeyValuePair<double, double>>[NumOfRoads];
            time = new double[NumOfRoads];
            for (int i = 0; i < NumOfRoads; i++)
            {
                string s = SR.ReadLine();
                string[] d = s.Split(' ');
                roads[i] = new KeyValuePair<KeyValuePair<int, int>, KeyValuePair<double, double>>
                    (new KeyValuePair<int, int>(Convert.ToInt32(d[0]), Convert.ToInt32(d[1])),
                    new KeyValuePair<double, double>(Convert.ToDouble(d[2]), Convert.ToDouble(d[3])));
                time[i] = roads[i].Value.Key / roads[i].Value.Value;
            }
            //T(N)=|V|+Seta(1) where v is the number of vertices
            SR.Close();
        }
        private void GetinfoFromQueryFile()
        {
         
            FileStream fs1 = new FileStream(textBox6.Text.ToString(), FileMode.Open);
            StreamReader SR1 = new StreamReader(fs1);
            Q = Convert.ToInt32(SR1.ReadLine().ToString());
            sourceCoordinates = new KeyValuePair<double, double>[Q];
            destinationCoordinates = new KeyValuePair<double, double>[Q];
            R = new double[Q];
            for (int i = 0; i < Q; i++)
            {
                string s = SR1.ReadLine();
                string[] d = s.Split(' ');
                sourceCoordinates[i] = new KeyValuePair<double, double>(Convert.ToDouble(d[0]), Convert.ToDouble(d[1]));
                destinationCoordinates[i] = new KeyValuePair<double, double>(Convert.ToDouble(d[2]), Convert.ToDouble(d[3]));
                R[i] = Convert.ToDouble(d[4]);
            }
            //T(N)=T(N)+Seta(1) where N is the number of queries
            SR1.Close();
            for (int i = 0; i < NumOfRoads; i++)
            {
                int key1 = roads[i].Key.Key;
                int key2 = roads[i].Key.Value;
                KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>> valOfList1 =
                                new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>();
                valOfList1 = new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                        (roads[i].Key.Value, new KeyValuePair<double, KeyValuePair<double, double>>
                        (time[i], new KeyValuePair<double, double>(roads[i].Value.Key, roads[i].Value.Value)));
                KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>> valOfList2 =
                               new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>();
                valOfList2 = new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                        (roads[i].Key.Key, new KeyValuePair<double, KeyValuePair<double, double>>
                        (time[i], new KeyValuePair<double, double>(roads[i].Value.Key, roads[i].Value.Value)));
                if (parameter.ContainsKey(key1))
                {
                    parameter[key1].Add(valOfList1);
                }
                else
                {
                    parameter.Add(key1, new List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>> { valOfList1 });
                }
                if (parameter.ContainsKey(key2))
                {
                    parameter[key2].Add(valOfList2);
                }
                else
                {
                    parameter.Add(key2, new List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>> { valOfList2 });
                }
            }
            

        }

            private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open Query File";
            fdlg.Filter = "Text Files|*.doc;*.docx;*.txt;*.text";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = fdlg.FileName;
            }
            textBox11.Clear();
            GetinfoFromQueryFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog bDialog = new OpenFileDialog();
            bDialog.Title = "Open Text file";
            bDialog.Filter = "Text|*.txt";
            DialogResult result = bDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = bDialog.FileName.ToString();
                textBox10.Text = file;
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Open Map File";
            fdlg.Filter = "Text Files|*.doc;*.docx;*.txt;*.text";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = fdlg.FileName;
            }
            GetinfoFromMapFile();
            textBox11.Clear();
            textBox6.Clear();
            textBox10.Clear();
            count++;
            Fname = null;
            Fname = "Output";
            Fname += count;
            Fname += ".txt";
            FileStream FF;
            if (File.Exists(Fname))
               FF = new FileStream(Fname, FileMode.Truncate);
            else
                FF = new FileStream(Fname, FileMode.Create);
            FF.Close();
        }
        double Exetime;
        double totalextime;
        private void button1_Click(object sender, EventArgs e)
        {
            ans.Clear();
            Stopwatch SW = new Stopwatch();
            if (string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("You should choose query file and map file");

               
            }
            else
            {
                if (string.IsNullOrEmpty(textBox11.Text))
                    textBox11.Text = "1";
                else if (Convert.ToInt32(textBox11.Text) + 1 <= Q)
                    textBox11.Text = (Convert.ToInt32(textBox11.Text) + 1).ToString();
                else
                {
                    MessageBox.Show("You have reached the end of query file");
                    goto break1;

                }
                  //T(N)=|V|+Seta(1) where v is the number of vertices
                  if (parameter.ContainsKey(-1))
                {
                   parameter.Remove(-1);
                }
                  
                for (int i = 0; i < NumOfInter; i++)
                {
                    ans.Add(arr[i].Key, new KeyValuePair<int, KeyValuePair<double, double>>(-10, new KeyValuePair<double, double>
                 (9999999999999999999, 9999999999999999999)));
                    if (mohemgdn[i].Key ==-2)
                    {
                        parameter[i].Remove(mohemgdn[i]);
                        mohemgdn[i] = new
                       KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                       (-1, new KeyValuePair<double, KeyValuePair<double, double>>
                       (0, new KeyValuePair<double, double>(0, 0)));
                    }
                    double Xaxiss = arr[i].Value.Key - sourceCoordinates[Convert.ToInt32(textBox11.Text) - 1].Key;
                    double Yaxiss = arr[i].Value.Value - sourceCoordinates[Convert.ToInt32(textBox11.Text) - 1].Value;
                    Xaxiss *= Xaxiss;
                    Yaxiss *= Yaxiss;
                    double distances = Xaxiss + Yaxiss;
                    distances = Math.Sqrt(distances);
                    double Xaxisd = destinationCoordinates[Convert.ToInt32(textBox11.Text) - 1].Key - arr[i].Value.Key;
                    double Yaxisd = destinationCoordinates[Convert.ToInt32(textBox11.Text) - 1].Value - arr[i].Value.Value;
                    Xaxisd *= Xaxisd;
                    Yaxisd *= Yaxisd;
                    double distancesd = Xaxisd + Yaxisd;
                    distancesd = Math.Sqrt(distancesd);
                    if (distances*1000<= Convert.ToDouble(R[Convert.ToInt32(textBox11.Text) - 1]))
                    {
                        KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>> valOfList1 =
                                    new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>();
                        valOfList1 = new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                                (arr[i].Key, new KeyValuePair<double, KeyValuePair<double, double>>
                                ((distances / 5.0), new KeyValuePair<double, double>(distances, 5)));
                        if (parameter.ContainsKey(-1))
                        {
                            parameter[-1].Add(valOfList1);
                            
                        }
                        else
                        {
                            parameter.Add(-1, new List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>> { valOfList1 });
                        }
                    }
                    if (distancesd*1000 <= Convert.ToDouble(R[Convert.ToInt32(textBox11.Text) - 1]))
                    {
                        KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>> valOfList1 =
                                   new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>();
                        valOfList1 = new KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>
                                (-2, new KeyValuePair<double, KeyValuePair<double, double>>
                                ((distancesd / 5.0), new KeyValuePair<double, double>(distancesd, 5)));
                        mohemgdn[i] = valOfList1;
                        if (parameter.ContainsKey(arr[i].Key))
                        {
                            parameter[arr[i].Key].Add(valOfList1); 
                        }
                        else
                        {
                            parameter.Add(arr[i].Key, new List<KeyValuePair<int, KeyValuePair<double, KeyValuePair<double, double>>>> { valOfList1 });
                        }
                    }
                  

                }
                //T(N) = seta(N)+seta(1) where N is the number of inetrsection ID's
               
                fill();

                SW.Start();
                min();
                SW.Stop();
                Exetime = Convert.ToDouble(SW.ElapsedMilliseconds);
                totalextime += Exetime;
                getpath(-2, -1);

                break1:;
            }

        }
        public void getpath(int End, int start1)
        {

            FileStream fs = new FileStream(textBox10.Text, FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            for (int i = 0; i < (Convert.ToInt32(textBox11.Text) - 1) * 6; i++)
                SR.ReadLine();

            double sum = 0.0;
            double d1 = 0.0;
            double d2 = 0.0;
            double dis = 0.0;
            double timee = 0.0;
            timee = ans[-2].Value.Key;
            timee = timee * 60;
            timee = Math.Round(timee, 2);
           //* 
            while (End != start1)
            {
                if (End == -2)
                {
                    d1 = ans[End].Value.Value;
                    sum += d1;
                    End = ans[End].Key;


                }

                else
                {
                    if (ans[End].Key == -1)
                    {
                        d2 = ans[End].Value.Value;
                    }
                    sum += ans[End].Value.Value;
                    End = ans[End].Key;
                }
                dis = Math.Round(d1 + d2, 2);
                textBox2.Text = dis.ToString();
                double vicDis = 0;
                vicDis = Math.Round(sum - dis, 2);
                textBox1.Text = vicDis.ToString();

            } 
            // this  loop take o ( n) as a complxity as aworst case 
            sum = Math.Round(sum, 2);
            //string  fileline =Convert.ToInt32( SR.ReadLine().ToString());

            double[] arr1 = new double[5];
//*
            for (int i = 0; i < 5; i++)
            {
                string s = SR.ReadLine();
                string[] x = s.Split(' ');
                arr1[i] = Convert.ToDouble(x[0]);

            }
            // this loop take  O` (1) 


            if (timee ==arr1[1])
            {

               

                textBox4.Text = timee.ToString();
                timee = 0;
                // O` (1)
      
                if (sum == arr1[2])
                {
                    
                    textBox3.Text = sum.ToString();
                    sum = 0;
                    // O` (1)
      
                   
                }
                
                textBox9.Text = Exetime.ToString();
            

                //fe hna if talta bta3t excution time b3deha h3rid elpath
                Stack<int> st = new Stack<int>();
                int ss = -2;
               
               //
                while (ss != -1  )
                {
                    st.Push(ss);

                    ss = ans[ss].Key;


                }
                // this loop take o(n)
      
            // st.Push(-1);
                string[] arr = new string[ans.Count * 2];

              string pathh=null;
                textBox8.Text = null;

                while (st.Count != 0)
                {


                    pathh = st.Pop().ToString();
                    if (pathh != "-1" && pathh != "-2")
                        textBox8.Text += pathh;
                    if (st.Count == 0)
                    {
                       break;
                    }
                   
                    // t(n) = V
                    pathh = "  ";
                    textBox8.Text += pathh;
                }
                MessageBox.Show("Intersection IDs:  "+textBox8.Text);
                    SR.Close();
                st.Clear(); 
                FileStream FF = new FileStream(Fname, FileMode.Append);
                StreamWriter SW = new StreamWriter(FF);

                SW.WriteLine(textBox8.Text);
                SW.WriteLine(textBox4.Text + " mins");
                SW.WriteLine(textBox3.Text + " km");

                SW.WriteLine(textBox2.Text + " km");

                SW.WriteLine(textBox1.Text + " km");
 

                SW.WriteLine();

                // write f O (N)
                
                SW.Close();
               
                
               
            }
            else
            {
                MessageBox.Show("The Path Isn't Correct...! Try Again :D ");
            }

            start1 = 0;
            End = 0;
            
            // total comp = O(N) +  O(N) +v +1 = O(n )

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
       
    }
}
