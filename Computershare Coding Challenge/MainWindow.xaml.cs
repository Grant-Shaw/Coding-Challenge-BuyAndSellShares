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
using System.IO;
using Microsoft.Win32;

namespace Computershare_Coding_Challenge
{
    /// <summary>
    /// Coding challenge for Computershare. Buy and sell stock.
    /// </summary>
    public partial class MainWindow : Window
    {
       
        List<string> stockList;
        List<string> displayList;
        List<double> stockPrices;
        
       


        public MainWindow()
        {
            InitializeComponent();
        }

        //Reads the CSV file and displays the stock prices (mostly for ease of testing and comparison of results).
        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
                {
                 using (var reader = new StreamReader(openFile.FileName))
                {
                    stockList = new List<string>();
                    stockPrices = new List<double>();
                    while(!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        stockList.AddRange(values);
                        //Converts the stocklist to double values to be used in the calculate method.
                        foreach(string s in values)
                        {
                            stockPrices.Add(Convert.ToDouble(s));
                        }
                       
                        displayList = stockList;
                        for( int i = 0; i < displayList.Count; i++)
                        {
                            displayList[i] = $"Day {i + 1}: " + displayList[i];
                        }
                        //Displays the prices with the day.
                        listPrices.ItemsSource = displayList;
                    }
                   }
            }

           txt_trade.Text = CalculateBestTrade();
             
           

        }

       
        private string CalculateBestTrade()
        {
               
            double maxProfit = 0;
            double buyDay = 0;
            double sellDay = 0;
            double buyAmount = 0;
            double sellAmount = 0;
            string result;

            //nested for loop which updates the maximum amount of profit and updates it if it's greater. The nested loop ensures that the selldate can never come before the buy date.

            for (int i = 0; i < stockPrices.Count - 1; i++)
            {
                for(int j = i + 1; j < stockPrices.Count; j++)
                {
                    double profit = stockPrices[j] - stockPrices[i];
                    if(profit > maxProfit)
                    {
                        maxProfit = profit;
                        sellDay = j + 1;
                        buyDay = i + 1;
                        buyAmount = stockPrices[i];
                        sellAmount = stockPrices[j];
                    }
                }
            }

            return result = $"{buyDay}({buyAmount}),{sellDay}({sellAmount})";      

        }
    }
}
