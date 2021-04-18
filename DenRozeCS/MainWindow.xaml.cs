using DenRozeCS.Entities;
using DenRozeCS.Controllers;
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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Threading;
using System.Reflection;

namespace DenRozeCS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ItemController itemController = new ItemController();
        BillController billController = new BillController();
        OrderController orderController = new OrderController();
        UserController userController = new UserController();
        BillItemController billItemController = new BillItemController();
        int activeBill = -1;
        int activeBillIndex = 0;
        int activeOrder = -1;
        int activeOrderIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void InitializeViews()
        {
            var stock = await itemController.GetAllItemsAsync();
            await this.Dispatcher.BeginInvoke(new Action(() =>
            {
                StockView.ItemsSource = stock;
                OrderStockView.ItemsSource = stock;
                BillStockView.ItemsSource = stock;
            }));
            
            var bills = await billController.GetBillByDateAsync(DateTime.Now);
            if(bills.Count < 1)
            {
                await billController.InsertBillAsync(0, "", DateTime.Now, DateTime.Now, "");
                bills = await billController.GetBillByDateAsync(DateTime.Now);
            }
            activeBill = bills[activeBillIndex].BID;
            
            var billItems = await billItemController.GetBillItemByBillAsync(activeBill);
            var billitemjoinitem = billItems.Join(stock, item => item.IID, billItem => billItem.IID, (billitem, item) => new
            {
                BIID = billitem.BIID,
                Count = billitem.Count,
                Name = item.Name,
                Code = item.Code,
                Price = item.Price,
                IID = item.IID
            });
            decimal total = 0;
            foreach(var i in billitemjoinitem){
                total += i.Price * i.Count;
            }

            await billController.UpdateBillAsync(activeBill, total, bills[activeBillIndex].EETinfo, bills[activeBillIndex].Created_at, DateTime.Now, bills[activeBillIndex].TransactionType);
            await this.Dispatcher.BeginInvoke(new Action(() =>
            {
                selected_bill.Text = activeBill.ToString();
                billtotal.Text = total.ToString();
                BillItemView.ItemsSource = billitemjoinitem;
            }));

            var orders = await orderController.GetOrderByDateAsync(DateTime.Now);

            var activeOrderUser = (await userController.GetUserByIdAsync(orders[activeOrderIndex].UID))[0];

            if (orders.Count < 1)
            {
                await orderController.InsertOrderAsync("", DateTime.Now, DateTime.Now, 1);
                orders = await orderController.GetOrderByDateAsync(DateTime.Now);
            }
            activeOrder = orders[activeOrderIndex].OID;

            var orderItems = await billItemController.GetBillItemByOrderAsync(activeOrder);
            var orderitemjoinitem = orderItems.Join(stock, item => item.IID, billItem => billItem.IID, (billitem, item) => new
            {
                BIID = billitem.BIID,
                Count = billitem.Count,
                Name = item.Name,
                Code = item.Code,
                Price = item.Price,
                IID = item.IID
            });

            await this.Dispatcher.BeginInvoke(new Action(() =>
            {
                selected_order.Text = activeOrder.ToString();
                fullname.Text = activeOrderUser.Full_name.ToString();
                phone.Text = activeOrderUser.Phone.ToString();
                email.Text = activeOrderUser.Email.ToString();
                address.Text = activeOrderUser.Address.ToString();

                OrderItemView.ItemsSource = orderitemjoinitem;
            }));
        }
        private async void OrderItemRemClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderItemView.SelectedItem;
            if (selectedItem == null)
                return;
            PropertyInfo property = selectedItem.GetType().GetProperty("BIID");
            int biid = int.Parse(property.GetValue(selectedItem, null).ToString());
            await billItemController.DeleteBillItemAsync(biid);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void OrderItemAddClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = OrderStockView.SelectedItem;
            if (selectedItem == null)
                return;
            int iid = (selectedItem as ItemEntity).IID;
            int count = 1;
            if (orderaddremcount.Text != "")
            {
                count = int.Parse(orderaddremcount.Text);
            }
            await billItemController.InsertBillItemAsync(count, iid, 0, activeOrder);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }

        private async void BillItemRemClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = BillItemView.SelectedItem;
            if (selectedItem == null)
                return;
            PropertyInfo property = selectedItem.GetType().GetProperty("BIID");
            int biid = int.Parse(property.GetValue(selectedItem, null).ToString());
            await billItemController.DeleteBillItemAsync(biid);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void BillItemAddClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = BillStockView.SelectedItem;
            if (selectedItem == null)
                return;
            int iid = (selectedItem as ItemEntity).IID;
            int count = 1;
            if(billaddremcount.Text != "")
            {
                count = int.Parse(billaddremcount.Text);
            }
            await billItemController.InsertBillItemAsync(count, iid, activeBill, 0);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private void OrderPreviousClick(object sender, RoutedEventArgs e)
        {
            if (activeOrderIndex - 1 >= 0)
            {
                activeOrderIndex--;
                Thread thread = new Thread(InitializeViews);
                thread.Start();
            }
        }
        private async void OrderDeleteClick(object sender, RoutedEventArgs e)
        {
            await billItemController.DeleteBillItemByOrderIdAsync(activeOrder);
            await orderController.DeleteOrderAsync(activeOrder);
            activeOrderIndex = 0;
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void OrderNextClick(object sender, RoutedEventArgs e)
        {
            if (activeOrderIndex + 1 >= (await orderController.GetOrderByDateAsync(DateTime.Now)).Count)
            {
                await orderController.InsertOrderAsync("", DateTime.Now, DateTime.Now, 1);
            }
            activeOrderIndex++;
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private void BillPreviousClick(object sender, RoutedEventArgs e)
        {
            if(activeBillIndex - 1 >= 0)
            {
                activeBillIndex--;
                Thread thread = new Thread(InitializeViews);
                thread.Start();
            }
        }
        private async void BillDeleteClick(object sender, RoutedEventArgs e)
        {
            await billItemController.DeleteBillItemByBillIdAsync(activeBill);
            await billController.DeleteBillAsync(activeBill);
            activeBillIndex = 0;
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void BillNextClick(object sender, RoutedEventArgs e)
        {
            if(activeBillIndex + 1 >= (await billController.GetBillByDateAsync(DateTime.Now)).Count)
            {
                await billController.InsertBillAsync(0, "", DateTime.Now, DateTime.Now, "");
            }
            activeBillIndex++;
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void ItemDeleteClick(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);
            await itemController.DeleteItemAsync(id);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void ItemEditClick(object sender, RoutedEventArgs e)
        {
            int id_from_button = Convert.ToInt32(((Button)sender).Tag);
            var item = (await itemController.GetItemByIdAsync(id_from_button))[0];
            iid.Text = id_from_button.ToString();
            name.Text = item.Name;
            code.Text = item.Code;
            dph.Text = item.DPH.ToString();
            price.Text = item.Price.ToString();
            count.Text = item.Count.ToString();
            mincount.Text = item.Mincount.ToString();
            weight.Text = item.Weight.ToString();
        }
        private async void ItemNewClick(object sender, RoutedEventArgs e)
        {
            
            int id = Convert.ToInt32(((Button)sender).Tag);
            await itemController.InsertItemAsync("name", "code", 0, 0, 0, 0, 0);
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
        private async void ItemSaveClick(object sender, RoutedEventArgs e)
        {
            await itemController.UpdateItemAsync(int.Parse(iid.Text), name.Text, code.Text, decimal.Parse(price.Text), decimal.Parse(dph.Text), int.Parse(count.Text), int.Parse(mincount.Text), int.Parse(weight.Text));
            Thread thread = new Thread(InitializeViews);
            thread.Start();
        }
    }
}
