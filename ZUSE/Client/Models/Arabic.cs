using System;
using System.Runtime.InteropServices;
namespace ZUSE.Client.Models
{
    public class Arabic : Interaction
    {
        public Arabic(string OrderReference = null, string TaxID = null)
        {
            base.LangName = "ع";
            base.OrderReferencePlaceHolder = ".. رقم الفاتورة";
            base.greeting_instruction = "الرجاء رفع صوت الهاتف لتصلك التنبيهات الصوتية عن طريق البقاء على صفحة الانتظار او الرسائل النصية";
            base.VQ_greeting = "أدخل الطابور";
            base.OrderReferenceInstruction = "أحرف وأراقم انجليزية فقط";
            base.EnterOrderReferenceButton = "تأكيد";
            base.OrderReference = $"{OrderReference} : رقم خدمتك هو";
            base.UnderProccesing = "أنت الآن على قائمة الإنتظار";
            base.MobileNumberLabel = ": نبهني برسالة واتساب ";

            base.MobileNumberInstruction = "0571234567 : الرجاء كتابة رقم جوالك كالتالي ";
            base.LatestCall = "النداء الأخير : ";
            base.OrderIsReadyToPick = "تفضل إلى مقدم الخدمة";
            base.Since = " منذ";
            base.IvePickedMyOrder = "لقد حصلت على خدمتي";
            base.GoodBye = "شكراً لإستخدامك زوس، وداعاً";
        }
    }
}
