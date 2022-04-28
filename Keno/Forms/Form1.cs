using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Defaults;

namespace Keno
{
    public partial class Form1 : Form
    {
        public List<int> StratergyArray = new List<int>();

        public string StakeSite = "stake.com";
        public string token = "";

        public bool running = false;
        public static Form1 initForm;
        public string riskSelected = "low";
        public string currencySelected = "btc";
        public decimal BaseBet = 0;
        public decimal amount = 0;
        public decimal currentBal = 0;
        public decimal currentProfit = 0;
        public int counter = 0;

        public string[] curr = { "BTC", "ETH", "LTC", "DOGE", "XRP", "BCH", "TRX", "EOS" };

        CartesianChart ch = new CartesianChart();
        ChartValues<ObservablePoint> data = new ChartValues<ObservablePoint>();

        List<double> xList = new List<double>();
        List<double> yList = new List<double>();
        public Form1()
        {
            InitializeComponent();
            Text += " - " + Application.ProductVersion;
            initForm = this;
            currencySelect.SelectedIndex = 0;
            riskSelect.SelectedIndex = 1;
            siteStake.SelectedIndex = 0;
            this.listView1.ItemChecked += this.listView1_ItemChecked;

            xList.Add(0);
            yList.Add(0);
            data.Add(new ObservablePoint
            {
                X = xList[counter],
                Y = yList[counter]
            });
            ch.Series = new SeriesCollection
            {
                new LiveCharts.Wpf.LineSeries
                {
                    Title = "Profit",
                    Values = data,
                    PointGeometrySize = 0,
                    AreaLimit = 0
                }
            };
            ch.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 5
                }
            });
            

            //ch.Series[0].ScalesYAt = 0;
            ch.Width = 250;
            panel1.Controls.Add(ch);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                if (StratergyArray.Count > 0)
                {
                    stratSelector.Clear(StratergyArray);
                }
                List<int> selectedSquares = new List<int>();
                for (int i = 0; i < stratSelector.squareData.Length; i++)
                {
                    if (stratSelector.squareData[i] == 1)
                        selectedSquares.Add(i);
                }
                StratergyArray = selectedSquares;
                button1.Enabled = false;
                stratSelector.selectAllowed = false;
                running = true;
                await KenoBet(true);
                running = false;
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                button1.Enabled = false;
                autoPickBtn.Enabled = false;
                clearTable.Enabled = false;
                if (StratergyArray.Count > 0)
                {
                    stratSelector.Clear(StratergyArray);
                }
                List<int> selectedSquares = new List<int>();
                for (int i = 0; i < stratSelector.squareData.Length; i++)
                {
                    if (stratSelector.squareData[i] == 1)
                        selectedSquares.Add(i);
                }
                StratergyArray = selectedSquares;
                running = true;
                button2.Text = "Stop";
                button2.Enabled = false;
                currencySelect.Enabled = false;
                //textBox1.Enabled = false;
                stratSelector.selectAllowed = false;
                KenoBet(false);
            }
            else
            {
                running = false;
                bSta();
            }
        }

        async Task StartBet(bool clear)
        {
            if (running == true)
            {

                button2.Enabled = true;
                if (clear)
                {
                    await Task.Delay(40);
                    if (StratergyArray.Count > 0)
                    {
                        stratSelector.Clear(StratergyArray);
                    }
                }
                await StartBet(false);
            }
        }

        public void bSta()
        {
            running = false;
            button1.Enabled = true;
            autoPickBtn.Enabled = true;
            clearTable.Enabled = true;
            stratSelector.selectAllowed = true;
            currencySelect.Enabled = true;
            //textBox1.Enabled = true;
            StratergyArray.Clear();
            button2.Text = "Start";
        }

        public void Log(Data response)
        {
            List<int> selected = response.data.kenoBet.state.selectedNumbers.Select(n => n + 1).OrderBy(p => p).ToList();
            List<int> result = response.data.kenoBet.state.drawnNumbers.Select(n => n + 1).OrderBy(p => p).ToList();


            var matchList = result.Intersect(selected).OrderBy(p => p).ToList();

            //matched.Remove(matched.Length - 1);
            string[] row = { response.data.kenoBet.id, string.Format("({1}x) {0}", string.Join(",", matchList), matchList.Count), string.Join(",", selected), string.Join(",", result), riskSelected, string.Format("{0} {1}", amount.ToString("0.00000000"), currencySelected), response.data.kenoBet.payoutMultiplier.ToString("0.00") + "x", response.data.kenoBet.payout.ToString("0.00000000") };
            var log = new ListViewItem(row);
            //betitem.Font = new Font("Consolas", 10f);
            listView1.Items.Insert(0, log);
            if (listView1.Items.Count > 50)
            {
                listView1.Items[listView1.Items.Count - 1].Remove();
            }


        }

        private async Task Authorize()
        {
            try
            {
                var mainurl = "https://api." + StakeSite + "/graphql";
                var request = new RestRequest(Method.POST);
                var client = new RestClient(mainurl);
                BetQuery payload = new BetQuery();
                payload.operationName = "initialUserRequest";
                payload.variables = new BetClass() { };
                payload.query = "query initialUserRequest {\n  user {\n    ...UserAuth\n    __typename\n  }\n}\n\nfragment UserAuth on User {\n  id\n  name\n  email\n  hasPhoneNumberVerified\n  hasEmailVerified\n  hasPassword\n  intercomHash\n  createdAt\n  hasTfaEnabled\n  mixpanelId\n  hasOauth\n  isKycBasicRequired\n  isKycExtendedRequired\n  isKycFullRequired\n  kycBasic {\n    id\n    status\n    __typename\n  }\n  kycExtended {\n    id\n    status\n    __typename\n  }\n  kycFull {\n    id\n    status\n    __typename\n  }\n  flags {\n    flag\n    __typename\n  }\n  roles {\n    name\n    __typename\n  }\n  balances {\n    ...UserBalanceFragment\n    __typename\n  }\n  activeClientSeed {\n    id\n    seed\n    __typename\n  }\n  previousServerSeed {\n    id\n    seed\n    __typename\n  }\n  activeServerSeed {\n    id\n    seedHash\n    nextSeedHash\n    nonce\n    blocked\n    __typename\n  }\n  __typename\n}\n\nfragment UserBalanceFragment on UserBalance {\n  available {\n    amount\n    currency\n    __typename\n  }\n  vault {\n    amount\n    currency\n    __typename\n  }\n  __typename\n}\n";
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-access-token", token);

                request.AddParameter("application/json", JsonConvert.SerializeObject(payload), ParameterType.RequestBody);
                //request.AddJsonBody(payload);
                //IRestResponse response = client.Execute(request);

                var restResponse =
                    await client.ExecuteAsync(request);

                // Will output the HTML contents of the requested page
                //Debug.WriteLine(restResponse.Content);
                ActiveData response = JsonConvert.DeserializeObject<ActiveData>(restResponse.Content);
                //System.Diagnostics.Debug.WriteLine(restResponse.Content);
                if (response.errors != null)
                {
                    LiveBalLabel.Text = "Live Balance";
                    LiveBalLabel.ForeColor = SystemColors.ControlDarkDark;
                    StatusLogIn.Text = "Unauthorized";
                }
                else
                {
                    if (response.data != null)
                    {
                        StatusLogIn.Text = String.Format("({0}) Authorized ", response.data.user.name);
                        textBox1.Enabled = false;
                        for (var i = 0; i < response.data.user.balances.Count; i++)
                        {
                            if (response.data.user.balances[i].available.currency == currencySelected.ToLower())
                            {
                                LiveBalLabel.ForeColor = Color.Black;
                                LiveBalLabel.Text = String.Format("{0} | {1}", currencySelected.ToUpper(), response.data.user.balances[i].available.amount.ToString("0.00000000"));
                                //currentBal = response.data.user.balances[i].available.amount;

                            }
                            //currencySelect.Items.Clear();
                            if (true)
                            {
                                for (int s = 0; s < curr.Length; s++)
                                {
                                    if (response.data.user.balances[i].available.currency == curr[s].ToLower())
                                    {
                                        currencySelect.Items[s] = string.Format("{0} {1}", curr[s], response.data.user.balances[i].available.amount.ToString("0.00000000"));
                                        //currencySelect.Items.Add(string.Format("{0} {1}", s, response.data.user.balances[i].available.amount.ToString("0.00000000")));
                                        break;
                                    }
                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task CheckBalance(bool isCurrency)
        {
            try
            {
                var mainurl = "https://api." + StakeSite + "/graphql";
                var request = new RestRequest(Method.POST);
                var client = new RestClient(mainurl);
                BetQuery payload = new BetQuery();
                payload.operationName = "UserBalances";
                payload.query = "query UserBalances {\n  user {\n    id\n    balances {\n      available {\n        amount\n        currency\n        __typename\n      }\n      vault {\n        amount\n        currency\n        __typename\n      }\n      __typename\n    }\n    __typename\n  }\n}\n";

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-access-token", token);

                request.AddParameter("application/json", JsonConvert.SerializeObject(payload), ParameterType.RequestBody);



                var restResponse =
                    await client.ExecuteAsync(request);


                //Debug.WriteLine(restResponse.Content);
                BalancesData response = JsonConvert.DeserializeObject<BalancesData>(restResponse.Content);


                if (response.errors != null)
                {
                    LiveBalLabel.Text = "Live Balance";
                    LiveBalLabel.ForeColor = SystemColors.ControlDarkDark;

                    ///StatusLogIn.Text = "unauthorized";
                }
                else
                {
                    if (response.data != null)
                    {
                        if (isCurrency)
                        {
                            //currencySelect.Items.Clear();
                        }
                        for (var i = 0; i < response.data.user.balances.Count; i++)
                        {
                            if (response.data.user.balances[i].available.currency == currencySelected.ToLower())
                            {
                                LiveBalLabel.ForeColor = Color.Black;
                                LiveBalLabel.Text = String.Format("{0} | {1}", currencySelected.ToUpper(), response.data.user.balances[i].available.amount.ToString("0.00000000"));
                                currentBal = response.data.user.balances[i].available.amount;

                            }
                            //currencySelect.Items.Clear();
                            if (true)
                            {
                                for (int s = 0; s < curr.Length; s++)
                                {
                                    if (response.data.user.balances[i].available.currency == curr[s].ToLower())
                                    {
                                        currencySelect.Items[s] = string.Format("{0} {1}", curr[s], response.data.user.balances[i].available.amount.ToString("0.00000000"));
                                        //currencySelect.Items.Add(string.Format("{0} {1}", s, response.data.user.balances[i].available.amount.ToString("0.00000000")));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    //StatusLogIn.Text = "authorized";
                }
            }
            catch (Exception ex)
            {

            }

        }
        async Task KenoBet(bool showRes)
        {

            if (running)
            {
                var mainurl = "https://api." + StakeSite + "/graphql";
                var request = new RestRequest(Method.POST);
                var client = new RestClient(mainurl);
                BetQuery payload = new BetQuery();
                payload.variables = new BetClass()
                {
                    currency = currencySelected,
                    amount = amount,
                    risk = riskSelected.ToLower(),
                    numbers = StratergyArray,
                    identifier = RandomString(21)

                };

                payload.query = "mutation KenoBet($amount: Float!, $currency: CurrencyEnum!, $numbers: [Int!]!, $identifier: String!, $risk: CasinoGameKenoRiskEnum) {\n  kenoBet(\n    amount: $amount\n    currency: $currency\n    numbers: $numbers\n    risk: $risk\n    identifier: $identifier\n  ) {\n    ...CasinoBet\n    state {\n      ...CasinoGameKeno\n    }\n  }\n}\n\nfragment CasinoBet on CasinoBet {\n  id\n  active\n  payoutMultiplier\n  amountMultiplier\n  amount\n  payout\n  updatedAt\n  currency\n  game\n  user {\n    id\n    name\n  }\n}\n\nfragment CasinoGameKeno on CasinoGameKeno {\n  drawnNumbers\n  selectedNumbers\n  risk\n}\n";

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-access-token", token);

                request.AddParameter("application/json", JsonConvert.SerializeObject(payload), ParameterType.RequestBody);


                var restResponse =
                    await client.ExecuteAsync(request);

                //label4.Text = restResponse.Content;

                button2.Enabled = true;
                Data response = JsonConvert.DeserializeObject<Data>(restResponse.Content);

                if (response.errors != null)
                {
                    //Log(response.errors[0].message);
                    ;

                    if (running == true)
                    {
                        //running = false;
                        //bSta();
                        //await Task.Delay(2000);
                        //PrepRequest();
                    }
                    else
                    {
                        running = false;
                        //BSta(true);
                    }
                }
                else
                {
                    //clearSquares();
                    //stratSelector.Reset();
                    //await Task.Delay(50);
                    Log(response);
                    if (amount > 0)
                    {
                        CheckBalance(false);
                    }
                    currentProfit += response.data.kenoBet.payout - response.data.kenoBet.amount;
                    profitLabel.Text = currentProfit.ToString("0.00000000");

                    //ch.Series[0].Values.Add(counter);
                    //data.Add((double)currentProfit);
                    counter++;
                    xList.Add(counter);
                    yList.Add((double)currentProfit);



                    data.Add(new ObservablePoint
                    {
                        X = xList[xList.Count - 1],
                        Y = yList[yList.Count - 1]
                    });
                    

                    if (data.Count > 30)
                    {
                        data.RemoveAt(0);
                        xList.RemoveAt(0);
                        yList.RemoveAt(0);

                    }
                    if (showRes)
                    {
                        ShowResult(response.data.kenoBet.state.drawnNumbers, response.data.kenoBet.state.selectedNumbers);
                    }
                }
                if (showRes == false)
                {
                    await KenoBet(false);
                }
            }

        }
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //
            ListViewItem item = e.Item as ListViewItem;
            if (e.Item.Checked == true)
            {
                //MessageBox.Show(e.Item.Text);
                Process.Start(new ProcessStartInfo(string.Format("https://{1}/casino/home?betId={0}&modal=bet", e.Item.Text, StakeSite)) { UseShellExecute = true });
            }
        }
        void ShowResult(List<int> result, List<int> selected)
        {

            for (int i = 0; i < selected.Count; i++)
            {
                stratSelector.SetValue(selected[i], 1);
            }
            var matchList = result.Intersect(selected);
            foreach (var c in matchList)
            {
                stratSelector.SetValue(c, 3);
            }
            var nonMatch = result.Except(matchList);
            foreach (var c in nonMatch)
            {
                stratSelector.SetValue(c, 2);
            }

        }

        private void clearTable_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                stratSelector.selectAllowed = true;
                StratergyArray.Clear();
                listView2.Clear();
                stratSelector.Reset();
            }
        }

        public void AppendMultipliers(int[] squareData)
        {
            if (squareData.Count(x => x == 1) > 0)
            {
                listView2.Clear();
                if (riskSelected.Contains("low"))
                {
                    riskLow riskselect = new riskLow();
                    string[] tilesRange = Enumerable.Range(0, squareData.Count(x => x == 1) + 1).ToArray().Select(x => x.ToString() + "x").ToArray();
                    var tileCount = new ListViewItem(tilesRange);
                    foreach (double s in riskselect.Tile[squareData.Count(x => x == 1) - 1])
                    {
                        listView2.Columns.Add(s.ToString() + "x", TextRenderer.MeasureText(s.ToString() + "x", new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)).Width + 3, HorizontalAlignment.Center);
                    }
                    listView2.Items.Add(tileCount);
                    //listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
                if (riskSelected.Contains("classic"))
                {
                    riskClassic riskselect = new riskClassic();
                    string[] tilesRange = Enumerable.Range(0, squareData.Count(x => x == 1) + 1).ToArray().Select(x => x.ToString() + "x").ToArray();
                    var tileCount = new ListViewItem(tilesRange);
                    foreach (double s in riskselect.Tile[squareData.Count(x => x == 1) - 1])
                    {
                        listView2.Columns.Add(s.ToString() + "x", TextRenderer.MeasureText(s.ToString() + "x", new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)).Width + 3, HorizontalAlignment.Center);
                    }
                    listView2.Items.Add(tileCount);
                    //listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
                if (riskSelected.Contains("medium"))
                {
                    riskMedium riskselect = new riskMedium();
                    string[] tilesRange = Enumerable.Range(0, squareData.Count(x => x == 1) + 1).ToArray().Select(x => x.ToString() + "x").ToArray();
                    var tileCount = new ListViewItem(tilesRange);
                    foreach (double s in riskselect.Tile[squareData.Count(x => x == 1) - 1])
                    {
                        listView2.Columns.Add(s.ToString() + "x", TextRenderer.MeasureText(s.ToString() + "x", new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)).Width + 3, HorizontalAlignment.Center);
                    }
                    listView2.Items.Add(tileCount);
                    //listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
                if (riskSelected.Contains("high"))
                {
                    riskHigh riskselect = new riskHigh();
                    string[] tilesRange = Enumerable.Range(0, squareData.Count(x => x == 1) + 1).ToArray().Select(x => x.ToString() + "x").ToArray();
                    var tileCount = new ListViewItem(tilesRange);
                    foreach (double s in riskselect.Tile[squareData.Count(x => x == 1) - 1])
                    {
                        listView2.Columns.Add(s.ToString() + "x", TextRenderer.MeasureText(s.ToString() + "x", new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point)).Width + 3, HorizontalAlignment.Center);
                    }
                    listView2.Items.Add(tileCount);
                    //listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                listView2.Clear();
            }

        }

        private async void autoPickBtn_Click(object sender, EventArgs e)
        {
            if (running == false)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                stratSelector.selectAllowed = true;
                StratergyArray = new List<int>();

                int[] tilesRange = Enumerable.Range(0, 40).ToArray();
                tilesRange = Shuffle(tilesRange).ToList().Take(10).ToArray();
                stratSelector.Reset();
                foreach (int tile in tilesRange)
                {
                    await Task.Delay(50);
                    stratSelector.SetValue(tile, 1);
                    AppendMultipliers(stratSelector.squareData);
                }
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static int[] Shuffle(int[] arr)
        {
            Random rand = new Random();
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);
                int tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
            return arr;
        }

        private void riskSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            riskSelected = riskSelect.Text.ToLower();
            riskLabel.Text = string.Format("Risk: {0}", riskSelect.Text);
            AppendMultipliers(stratSelector.squareData);
        }

        private async void currencySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            currencySelected = curr[currencySelect.SelectedIndex].ToLower();
            string[] current = currencySelect.Text.Split(' ');
            if (current.Length > 1)
            {
                LiveBalLabel.Text = String.Format("{0} | {1}", current[0], current[1]);
            }

        }

        private void BetCost_ValueChanged(object sender, EventArgs e)
        {
            BaseBet = BetCost.Value;
            amount = BaseBet;
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            token = textBox1.Text;
            if (token.Length == 96)
            {
                await Authorize();
                await Task.Delay(200);
            }
            else
            {
                StatusLogIn.Text = "Unauthorized";
            }
        }

        private void LiveBalLabel_TextChanged(object sender, EventArgs e)
        {
            Size size = TextRenderer.MeasureText(LiveBalLabel.Text, LiveBalLabel.Font);
            LiveBalLabel.Width = size.Width;
            LiveBalLabel.Height = size.Height;
        }

        private void siteStake_SelectedIndexChanged(object sender, EventArgs e)
        {
            StakeSite = siteStake.Text.ToLower();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }




    public class stratGrid : Control
    {
        private Rectangle perimeterRect;

        private Rectangle[] squareRects;
        public int[] squareData { get; set; }

        private Brush idleColor = Brushes.LightGray;
        private Brush SetecledColor = Brushes.MediumPurple;
        private Brush WinColor = Brushes.LimeGreen;
        private Brush UnhitColor = Brushes.MistyRose;
        private int _squareSpacing;

        public bool selectAllowed = true;
        public int SquareSpacing
        {
            get { return _squareSpacing; }
            set
            {
                _squareSpacing = Math.Abs(value);
                Invalidate();
            }
        }

        public stratGrid()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

            Size = new Size(100, 100);
            SquareSpacing = 6;

            Reset();

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            e.Graphics.Clear(Parent.BackColor);

            int index = 0;

            int squareWidth = (212 - (SquareSpacing * 6)) / 5;
            int start = (212 - (squareWidth * 5) - (SquareSpacing * 4)) / 2;

            for (int col = 0; col < 5; col++)
            {
                int y = start + (squareWidth + SquareSpacing) * col;
                for (int row = 0; row < 8; row++)
                {

                    int x = start + (squareWidth + SquareSpacing) * row;
                    squareRects[index] = new Rectangle(x, y, squareWidth, squareWidth);

                    if (squareData[index] == 1)
                    {
                        e.Graphics.FillRectangle(SetecledColor, squareRects[index]);
                        e.Graphics.DrawString(" " + (index + 1).ToString(), new Font("Segoe UI", 11), Brushes.Black, squareRects[index]);
                    }
                    else if (squareData[index] == 0)
                    {
                        e.Graphics.FillRectangle(idleColor, squareRects[index]);
                        e.Graphics.DrawString(" " + (index + 1).ToString(), new Font("Segoe UI", 11), Brushes.Black, squareRects[index]);
                    }
                    else if (squareData[index] == 2)
                    {
                        e.Graphics.FillRectangle(UnhitColor, squareRects[index]);
                        e.Graphics.DrawString(" " + (index + 1).ToString(), new Font("Segoe UI", 11), Brushes.Red, squareRects[index]);
                    }
                    else if (squareData[index] == 3)
                    {
                        e.Graphics.FillRectangle(WinColor, squareRects[index]);
                        e.Graphics.DrawString(" " + (index + 1).ToString(), new Font("Segoe UI", 11), Brushes.Black, squareRects[index]);
                    }

                    index++;

                }
            }

        }

        protected override void OnSizeChanged(EventArgs e)
        {

            base.OnSizeChanged(e);

            if (Width > Height)
            {
                Height = Width;
            }
            else if (Height > Width)
            {
                Width = Height;
            }

            perimeterRect = new Rectangle(0, 0, Width - 1, Height - 1);

        }

        public void Reset()
        {

            squareRects = new Rectangle[40];
            squareData = new int[40];

            for (int i = 0; i < squareData.Length; i++) squareData[i] = 0;

            Invalidate();

        }

        public void Clear(List<int> StratergyArray)
        {

            squareRects = new Rectangle[40];
            squareData = new int[40];

            for (int i = 0; i < squareData.Length; i++)
            {
                squareData[i] = 0;
            }

            foreach (var s in StratergyArray)
            {
                squareData[s] = 1;
            }

            Invalidate();

        }

        public void SetValue(int index, int c)
        {
            if (c == 1)
            {
                if (squareData.Count(x => x == 1) < 10)
                {
                    squareData[index] = c;
                    Invalidate();
                }
            }
            else
            {
                squareData[index] = c;
                Invalidate();
            }


        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            for (int i = 0; i < squareRects.Length; i++)
            {
                if (squareRects[i].Contains(e.Location))
                {
                    if (selectAllowed == true)
                    {
                        if (squareData[i] == 0)
                        {
                            SetValue(i, 1);
                            Form1.initForm.button1.Enabled = true;
                            Form1.initForm.button2.Enabled = true;
                        }
                        else
                        {
                            SetValue(i, 0);
                        }
                        Form1.initForm.StratergyArray.Clear();
                        Form1.initForm.AppendMultipliers(squareData);
                    }
                }
            }
        }
    }

    public class riskLow
    {
        public List<Array> Tile = new List<Array>
        {
            new double[] { 0.7, 1.85 },
            new double[] { 0, 2, 3.80 },
            new double[] { 0, 1.10, 1.38, 26 },
            new double[] { 0, 0, 2.20, 7.90, 90 },
            new double[] { 0, 0, 1.50, 4.20, 13, 300 },
            new double[] { 0, 0, 1.10, 2, 6.20, 100, 700 },
            new double[] { 0, 0, 1.10, 1.60, 3.50, 15, 225, 700 },
            new double[] { 0, 0, 1.10, 1.50, 2, 5.50, 39, 100, 800 },
            new double[] { 0, 0, 1.10, 1.30, 1.70, 2.50, 7.50, 50, 250, 1000 },
            new double[] { 0, 0, 1.10, 1.20, 1.30, 1.80, 3.50, 13, 50, 250, 1000 }
        };

    }
    class riskClassic
    {
        public List<Array> Tile = new List<Array>
        {
            new double[] { 0, 3.96 },
            new double[] { 0, 1.90, 4.50 },
            new double[] { 0, 1.00, 3.10, 10.40 },
            new double[] { 0, 0.80, 1.80, 5.00, 22.5 },
            new double[] { 0, 0.25, 1.40, 4.10, 16.5, 36 },
            new double[] { 0, 0, 1.00, 3.68, 7, 16.5, 40 },
            new double[] { 0, 0, 0.47, 3.00, 4.50, 14, 31, 60 },
            new double[] { 0, 0, 0, 2.20, 4, 13, 22, 55, 70 },
            new double[] { 0, 0, 0, 1.55, 3, 8, 15, 44, 60, 85 },
            new double[] { 0, 0, 0, 1.40, 2.25, 4.5, 8, 17, 50, 80, 100 }
        };
    }
    class riskMedium
    {
        public List<Array> Tile = new List<Array>
        {
            new double[] { 0.4, 2.75 },
            new double[] { 0, 1.8, 5.10 },
            new double[] { 0, 0, 2.8, 50 },
            new double[] { 0, 0, 1.7, 10, 100 },
            new double[] { 0, 0, 1.40, 4, 14, 390 },
            new double[] { 0, 0, 0, 3, 9, 180, 710 },
            new double[] { 0, 0, 0, 2, 7, 30, 400, 800 },
            new double[] { 0, 0, 0, 2, 4, 11, 67, 400, 900 },
            new double[] { 0, 0, 0, 2, 2.5, 5, 15, 100, 500, 1000 },
            new double[] { 0, 0, 0, 1.60, 2, 4, 7, 26, 100, 500, 1000 }
        };
    }
    class riskHigh
    {
        public List<Array> Tile = new List<Array>
        {
            new double[] { 0, 3.96 },
            new double[] { 0, 0, 17.10 },
            new double[] { 0, 0, 0, 81.5 },
            new double[] { 0, 0, 0, 10, 259 },
            new double[] { 0, 0, 0, 4.5, 48, 450 },
            new double[] { 0, 0, 0, 0, 11, 350, 710 },
            new double[] { 0, 0, 0, 0, 7, 90, 400, 800 },
            new double[] { 0, 0, 0, 0, 5, 20, 270, 600, 900 },
            new double[] { 0, 0, 0, 0, 4, 11, 56, 500, 800, 1000 },
            new double[] { 0, 0, 0, 0, 3.5, 8, 13, 63, 500, 800, 1000 }
        };
    }

}
