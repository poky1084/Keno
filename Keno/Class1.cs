using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno
{


    //public class ApiHelper
    //{



    //    public static RestRequest CreateDefaultRestRequest(string apiKey)
    //    {
    //        RestRequest restRequest = new RestRequest(Method.POST);

    //        restRequest.AddHeader("authorization", string.Format("Bearer {0}", apiKey));
    //        restRequest.AddHeader("x-access-token", apiKey);

    //        restRequest.AddHeader("X-Requested-With", "XMLHttpRequest");

    //        restRequest.AddHeader("Content-type", "application/json");

    //        return restRequest;
    //    }

    //}


    public class BetQuery
    {
        public string operationName { get; set; }
        public string query { get; set; }
        public BetClass variables { get; set; }
    }
    public class BetClass
    {
        public string identifier { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string game { get; set; }
        public string guess { get; set; }
        public int minesCount { get; set; }
        public List<int> fields { get; set; }
        public string seed { get; set; }
        public string risk { get; set; }
        public List<int> numbers { get; set; }
    }
    public class Card
    {
        public string rank { get; set; }
        public string suit { get; set; }
    }
    public class Data
    {
        public Betdata data { get; set; }
        public List<Errors> errors { get; set; }
    }
    public class Errors
    {
        public List<string> path { get; set; }
        public string message { get; set; }
        public string errorType { get; set; }
        public string data { get; set; }
    }

    public class ActiveData
    {
        public User data { get; set; }
        public List<Errors> errors { get; set; }
    }
    public class User
    {
        public Active user { get; set; }
    }
    public class Active
    {
        public string id { get; set; }
        public string name { get; set; }
        public kenoBet activeCasinoBet { get; set; }
        public List<Balances> balances { get; set; }
    }
    public class Balances
    {
        public Available available { get; set; }
    }
    public class Available
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
    public class Betdata
    {
        public kenoBet kenoBet { get; set; }
        public kenoBet minesNext { get; set; }
        public kenoBet minesCashout { get; set; }
        public object rotateSeedPair { get; set; }
        public object createVaultDeposit { get; set; }
    }
    public class kenoBet
    {
        public string id { get; set; }
        public string iid { get; set; }
        public double payoutMultiplier { get; set; }
        public decimal amount { get; set; }
        public decimal payout { get; set; }
        public string updatedAt { get; set; }
        public string currency { get; set; }
        public Active user { get; set; }
        public State state { get; set; }
    }
    public class State
    {
        public List<int> drawnNumbers { get; set; }
        public List<int> selectedNumbers { get; set; }
        public string risk { get; set; }

    }
    public class Rounds
    {
        public int field { get; set; }
        public double payoutMultiplier { get; set; }

    }
    public class BalancesData
    {
        public User data { get; set; }
        public List<Errors> errors { get; set; }
    }
}
