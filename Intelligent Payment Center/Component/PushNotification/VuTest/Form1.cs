using Newtonsoft.Json.Linq;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;

namespace VuTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Gcm_Send_Single();
        }
        public void Gcm_Send_Single()
        {
            var succeeded = 0;
            var failed = 0;
            var attempted = 0;

            var config = new GcmConfiguration("82203050102", "AIzaSyDBnNrzQGsQ6-0-L8ZZ-s-qin5CpXAAWsk", null);
            //var config = new GcmConfiguration("82203050102", "AIzaSyCJOSXXSg6LpXyUM3E7wIeftSYdRw63Jt8", null);
            var broker = new GcmServiceBroker(config);
            broker.OnNotificationFailed += (notification, exception) => {
                failed++;
                //MessageBox.Show(exception.InnerException.Message);
            };
            broker.OnNotificationSucceeded += (notification) => {
                succeeded++;
                //MessageBox.Show("Success");
            };

            broker.Start();

            List<string> lsReceiver = new List<string>();
            //for(int i=0;i<1000;i++)
            //lsReceiver.Add("APA91bEYNE03s8z5TR_BnjQ97tqfx_D1n1pA6gg7bFPk0x85s2EgKmQugjaQI02fEi9uhHo5GTf6pT4LRxaYiFHkIIbbA8u3TL8kLR_W0rzPNYK7gvH2jQcE4tgjOf-3SSO9zHFebPNMYGyr3uBd7QD_sXZTXWCAQw");

            //lsReceiver.Add("APA91bHGRj3BeUAWrko8Gr3Lxsh9RCVa4m2tg147eabCw1WjsHb4sgO0dbGCG14rUtsM8N4yMzM-4rvNYUX9QcwrcfklE7JNOXwpVSMUMKvv2jRaA0l6VkCCpSxi-uqYhUfZ82lnBA4LhEAv7l2wbQaKl76J6csg7Q");

            //lsReceiver.Add("APA91bFZYCSGKzLpdzC5Q5iPTLi0WzQ29SRnEeUkCjjx7OXeTTMTYl-78o9R5kp0kNPgPZ6Ya4PsrYQEw48B-sIjSPitBMPocEvpQb-VJUrXDThem6cFNe2OkD33umMw-XqGiKCqXtOW7wqEFIcTUZPa7yXyAQ1N8w");

            //lsReceiver.Add("APA91bFr8EWhLRPp_kIfMvLOhsSQF_2aRinRzaa2HOfZUJzkqn4yxPk0vwS0RKKi7vnLvb-xs5Ye_nuX6Xmh6_IxZsh1Ns_Bge7MdUcSJ79HQfp8wctn9AaluNLn_6MBVPhnBBTGsFXmLYRSZ0VqBK4qkZesMgMYPA");

            //ut mi mix 2
            //lsReceiver.Add("cS0vbIlMHoo:APA91bF87xft5cMrUVKHbM03UmsfzZjahxlJ-UU4xDHv5DzxNAdsYMSCAkXwihMNbI0y7YqDadmuGBfVUcmeV0RV1qvHkoYKR8qh8gBWMowhQZHs6uMuVoDLTpnzT30WpQnuZKZwXhP_");

            //ut readmi note 2
            lsReceiver.Add("dwJRSNizYiQ:APA91bGBUU_T6SkPyh4jeKbeF0U5Q5NyAoWoc-sAs-r8Frc1NYapRK1AR7loesV_6ylm4FwIO1NfN5fGfzrXu2ntXrq5thCOahhqhZK76a7JNnrfdCEqDkoM4ItfFYSGX4GA_1A4YTV-");

            foreach (var regId in lsReceiver)
            {
               for(int i=0; i<1;i++)
                {
                    attempted++;

                    string message = @"{""title"":""" + "My"
                            + @""",""message"":""" + "message"
                            + @""",""url"":""" + "Url"
                            + @""",""icon_url"":""" + "IconUrl"
                            + @""",""image_url"":""" + "ImageUrl"
                            + @""",""options"":""" + "options"
                            + @""",""sound"":""sound.caf""}";

                    broker.QueueNotification(new GcmNotification
                    {
                        RegistrationIds = new List<string> {
                        regId
                    },
                        Data = JObject.Parse("{ "
                        + "\"title\" : \" [" + attempted.ToString() + DateTime.Now.ToString() + "] Fuck u\","
                        + "\"subtitle\" : \"R u crazy\","
                        + "\"alert\" : \"We've some promotion " + attempted.ToString() + " u, let's check it\","
                        + "\"json\" : \"json\""
                        + " }")
                        //Data = JObject.Parse(message)
                    });
                    //System.Threading.Thread.Sleep(1000);
                }
            }

            broker.Stop();
            System.Threading.Thread.Sleep(1000);
            txtRs.Text += $"\r\n {succeeded}/{failed}";
        }
    }
}
