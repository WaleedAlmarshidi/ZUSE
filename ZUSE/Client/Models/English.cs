using System;
namespace ZUSE.Client.Models
{
    public class English : Interaction
    {
        public English(string OrderReference = null)
        {
            base.LangName = "English";
            base.VQ_greeting = "Enter service queue";
            base.OrderReferencePlaceHolder = "Order number .. ";
            base.greeting_instruction = "please make sure that your phone is not in silent mode\nso that you can receive sound messages in the next waiting page or SMS";
            base.OrderReferenceInstruction = "Eng numbers and letter only.";
            base.EnterOrderReferenceButton = "Enter";
            base.OrderReference = $"Order refernce : {OrderReference}";
            base.OrderIsReadyToPick = "It's your turn !";
            base.UnderProccesing = "You are in the waiting line now ..";
            base.MobileNumberLabel = "Notify me via Whatsapp : ";
            base.MobileNumberInstruction = "please write your mobile number as followes : 0571234567";
            base.Since = "since ";
            base.IvePickedMyOrder = "my service is done!";
            base.GoodBye = "Thank you for using ZUSE,\nHave a nice day! 👋🏼";
        }
    }
}
