using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.IO;
using System.Net;
using System.Xml.Xsl;



namespace monacos.us_mvc.Controllers.Components
{
    public class RSS_Feed_ComponentController : Controller
    {
        // GET: RSS_Feed_Component
       
        public ActionResult Index()
        {

            String strRSSFeedTitle = null;
            String strRSSFeedURI = null;
            String Id = null;
            String RSSFieldHtml = null;



            try
            {

                Id = (String)this.ControllerContext.RouteData.Values["Id"];

                strRSSFeedURI = (string)this.ControllerContext.RouteData.Values["RSSURL"];

                strRSSFeedTitle = (string)this.ControllerContext.RouteData.Values["Title"];


                if (Id == null || strRSSFeedURI == null || strRSSFeedTitle == null)
                {

                    throw new Exception("Required Parameters not specified.", null);

                }


                if (System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_" + Id] == null)
                {

                    // New RSS Feed Instance

                    try
                    { 
                        RSSFieldHtml = RenderRSSFeed(strRSSFeedTitle, strRSSFeedURI);

                        System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_" + Id] = RSSFieldHtml;

                        System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_Create_Date_" + Id] = System.DateTime.Now;


                    }
                    catch (Exception e)
                    {
                        // Catches Everything including TimeOuts
                    }

                    
                }
                else
                {

                    // Existing RSS Feed Instance


                    // Cache 10 Minutes

                    if (((DateTime)System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_Create_Date_" + Id]).AddMinutes(10) <= System.DateTime.Now)
                    {
                        // Existing RSS Feed Instance Cache Timeout Not Exceeded


                        RSSFieldHtml = (String)System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_" + Id];
                    }
                    else
                    {

                        // Existing RSS Feed Instance Cache Timeout Exceeded
                        try
                        { 
                            RSSFieldHtml = RenderRSSFeed(strRSSFeedTitle, strRSSFeedURI);

                            System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_" + Id] = RSSFieldHtml;

                            System.Web.HttpContext.Current.Cache["RSS_Feed_Cache_Create_Date_" + Id] = System.DateTime.Now;

                        }
                        catch (Exception e)
                        {
                            // Catches Everything including TimeOuts
                        }


                        
                    }
                }

              
                ViewBag.RSS_Feed_Markup = RSSFieldHtml;


            }

            catch (System.Exception e)
            {

                throw new Exception("RSS_Feed_ComponentController::Index Error: " + e.Message);

            }

            return PartialView();

        }


        private string RenderRSSFeed(String strRSSFeedTitle, String strRSSFeedURI)
        {

            String XSLT_FilePath = "";
            System.Xml.Xsl.XslTransform objXSLTransform = null;
            System.Xml.XmlDocument objXSLTDocument = null;
            System.Xml.XmlDocument objXMLDocument = null;
            System.IO.StringWriter objStringWriter = null;
            System.Xml.XmlNamespaceManager objNameSpaceManager = null;
            String strPath = "";
            Stream InputDataStream = null;
            StreamReader InputDataStreamReader = null;
            HttpWebRequest objHttpWebRequest = null;
            string strFinalRSSFeedURI = null;
            string RSSFeedMarkup = null;
            string strXmlDocumentRSSFeed = null;
         





            //## Set Result Structure

            try
            {





                // Get RSS Feed Content
                
          
                strFinalRSSFeedURI = GetBaseUrl() + "/PostProxy.aspx?url=" + Server.UrlEncode(strRSSFeedURI);

                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(strFinalRSSFeedURI);

                objHttpWebRequest.Timeout = 10000;


                WebResponse objWebResponse = objHttpWebRequest.GetResponse();

                InputDataStream = objWebResponse.GetResponseStream();


                InputDataStreamReader = new StreamReader(InputDataStream);

               
                strXmlDocumentRSSFeed = InputDataStreamReader.ReadToEnd();

                XSLT_FilePath = "RSSFeedRender.xslt";

                strPath = Server.MapPath("~");

                XSLT_FilePath = strPath + "\\" + XSLT_FilePath;




                // Open XSLT Document Into XML Document

                objXSLTDocument = new System.Xml.XmlDocument();

                try
                {
                    objXSLTDocument.Load(XSLT_FilePath);
                }

                catch (System.Exception e)
                {

                    // Reformat Exception Message
                    throw new System.Exception("Failure to open XSL Translation File. Err Description: " + e.ToString() + " Err Source: " + e.Source, e);

                }

                // Prepare For XPath With Domain Space Prefix

                try
                {

                    objNameSpaceManager = new XmlNamespaceManager(new NameTable());

                    objNameSpaceManager.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

                }

                catch (System.Exception e)
                {
                    // Reformat Exception Message		
                    throw new System.Exception("Failure to Register XML Namespace. Err Description: " + e.ToString() + " Err Source: " + e.Source, e);

                }

                // Modify XSLT Rendering Depending on if Demo Mode Is Enabled
                // Default Value For This XSLT Param Is FALSE
                /*
                if ( (bool)Session["DemoMode"] ==  true ) 
                {

                    objXMLNode = objXSLTDocument.SelectSingleNode("//xsl:param[@name='DemoMode']", objNameSpaceManager );

                    objXMLNode.InnerText = "TRUE";

                }
                */


                objXSLTransform = new System.Xml.Xsl.XslTransform();

                try
                {
                    // objXSLTransform.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ControlDeviceInterface_XSLT_FilePath);

                    objXSLTransform.Load(objXSLTDocument);

                }

                catch (System.Exception e)
                {

                    // Reformat Exception Message

                    throw new System.Exception("Failure to open XSL Translation File. : Err Description: " + e.ToString() + " Err Source: " + e.Source, e);

                }

                objXMLDocument = new System.Xml.XmlDocument();


                // Load RSS Feed XML

                try
                {


                    //_strXmlDocumentRSSFeed = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><rss version=\"2.0\"><channel><title>American Radio Relay League | Ham Radio Association and Resources</title><description>The American Radio Relay League (ARRL) is the national association for amateur radio, connecting hams around the U.S. with news, information and resources.</description><link>http://www.arrl.org</link><url>http://www.arrl.org</url><language>en</language><item><title>The K7RA Solar Update</title><link>http://www.arrl.org/news/view/the-k7ra-solar-update-318</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/the-k7ra-solar-update-318</guid><description>&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;ARRL Headquarters is closed for Good Friday on April 18, so the bulletin comes to you a day early.&lt;br /&gt;&lt;br /&gt;After a few days in the doldrums on April 8-13 with sunspot numbers in the double digits, solar activity made a strong recovery over the following three days, with daily sunspot numbers of 105, 149 and 245. Sunspot numbers have reached this level several times in the past 12 months. On February 28,...&lt;/p&gt;</description><creator /><pubDate>Thu, 17 Apr 2014 11:22:00 -0400</pubDate></item>\n<item><title>Amateur Radio Volunteers Provide Critical Support for 30th Challenge Cup Relay</title><link>http://www.arrl.org/news/view/amateur-radio-volunteers-provide-critical-support-for-30th-challenge-cup-relay</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/amateur-radio-volunteers-provide-critical-support-for-30th-challenge-cup-relay</guid><description>&lt;p&gt;Amateur Radio volunteers were key to maintaining safety and security for the thousands of law enforcement personnel who took part in the Baker to Vegas &lt;strong&gt;&lt;span&gt;Challenge Cup Relay&lt;/span&gt;&lt;/strong&gt; (B2V)&lt;span&gt;&lt;span&gt;&lt;/span&gt;&lt;/span&gt; race on March 22. This year marked the 30th anniversary of the event, sometimes called “The world’s longest police foot pursuit.” The relay event covers some 120 miles of remote territory, from the Mojave Desert near Ba...&lt;/p&gt;</description><creator /><pubDate>Wed, 16 Apr 2014 17:46:00 -0400</pubDate></item>\n<item><title>ARRL Headquarters Will Be Closed on Good Friday, April 18 </title><link>http://www.arrl.org/news/view/arrl-headquarters-will-be-closed-on-good-friday-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/arrl-headquarters-will-be-closed-on-good-friday-april-18</guid><description>&lt;p&gt;ARRL Headquarters will be closed on Good Friday, April 18. There will be no &lt;strong&gt;&lt;span&gt;W1AW bulletin or code practice transmissions&lt;/span&gt;&lt;/strong&gt; and no &lt;b&gt;&lt;i&gt;ARRL Audio News&lt;/i&gt;&lt;/b&gt; on April 18. ARRL Headquarters will reopen Monday, April 21, at 8 AM Eastern Daylight Time. We wish everyone a safe and enjoyable holiday!&lt;/p&gt;&lt;p&gt;&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 19:20:00 -0400</pubDate></item>\n<item><title>W1AW/x Portable Operations, W100AW, and W1HQ Can QSL via Bureau</title><link>http://www.arrl.org/news/view/w1aw-x-portable-operations-w100aw-and-w1hq-can-qsl-via-bureau</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/w1aw-x-portable-operations-w100aw-and-w1hq-can-qsl-via-bureau</guid><description>&lt;p&gt;You may request that QSL cards be delivered via the &lt;strong&gt;ARRL QSL Bureau system&lt;/strong&gt; for contacts with the W1AW/x portable operations, W100AW, and W1HQ, now underway during the ARRL Centennial. You must first have an account with the QSL Bureau that handles your cards and have sufficient postage or envelopes on file with that bureau. Any cards that cannot be delivered will not be held or stored.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Please do...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 19:13:00 -0400</pubDate></item>\n<item><title>World Amateur Radio Day is Friday, April 18</title><link>http://www.arrl.org/news/view/world-amateur-radio-day-is-friday-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/world-amateur-radio-day-is-friday-april-18</guid><description>&lt;p&gt;“Amateur Radio: Your Gateway to Wireless Communication” is the theme for World Amateur Radio Day 2014, Friday, April 18. Radio amateurs celebrate World Amateur Radio Day each year on April 18 to recognize the anniversary of the founding of the International Amateur Radio Union (&lt;strong&gt;&lt;span&gt;IARU&lt;/span&gt;&lt;/strong&gt;) in Paris in 1925. ARRL Co-Founder Hiram Percy Maxim, 1AW, was its first president. The primary purpose of World ...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 17:30:00 -0400</pubDate></item>\n<item><title>Deadline is May 1 to Apply for ARRL Teachers Institute</title><link>http://www.arrl.org/news/view/deadline-is-may-1-to-apply-for-arrl-teachers-institute</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/deadline-is-may-1-to-apply-for-arrl-teachers-institute</guid><description>&lt;p&gt;The ARRL Education &amp;amp; Technology Program (&lt;b&gt;ETP&lt;/b&gt;) is still accepting applications for all sessions of the ARRL Teachers Institute this summer. &lt;i&gt;The &lt;b&gt;application&lt;/b&gt; deadline is May 1. &lt;/i&gt;Admission decisions will be made by May 9.&lt;/p&gt;&lt;p&gt;Learn how to i&lt;span&gt;ntegrate science and mathematics with engineering and technology by exploring wireless technology! &lt;/span&gt;&lt;/p&gt;&lt;p&gt;Now in its 11th year, the ARRL Teachers Institute is a four-day, exp...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 17:22:00 -0400</pubDate></item>\n<item><title>Gear Up Now for ARRL Field Day 2014!</title><link>http://www.arrl.org/news/view/gear-up-now-for-arrl-field-day-2014</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/gear-up-now-for-arrl-field-day-2014</guid><description /><creator /><pubDate>Tue, 15 Apr 2014 13:45:00 -0400</pubDate></item>\n<item><title>Today’s National Hurricane Conference Amateur Radio Workshops Being Streamed Live</title><link>http://www.arrl.org/news/view/today-s-national-hurricane-conference-amateur-radio-workshops-being-streamed-live</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/today-s-national-hurricane-conference-amateur-radio-workshops-being-streamed-live</guid><description>&lt;p&gt;Amateur Radio workshop sessions at the National Hurricane Conference taking place today in Miami will be streamed live to the Web via the &lt;strong&gt;VoIP SKYWARN/Hurricane Net Support&lt;/strong&gt; and &lt;strong&gt;North Shore Radio Association&lt;/strong&gt; sites. The session schedule is listed on these links and below. Times are in Eastern Daylight Time:&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;10:30 Opening Remarks (John McHugh, K4AG)&lt;/p&gt;&lt;p&gt;10:35 Importance of Amateur Radio/Spotter Surface ...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 13:39:00 -0400</pubDate></item>\n<item><title>KickSat Project Announces Telemetry Download Competition</title><link>http://www.arrl.org/news/view/kicksat-project-announces-telemetry-download-competition</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/kicksat-project-announces-telemetry-download-competition</guid><description>&lt;p&gt;[Update: 2014-04-15 1905 UTC] The Spacex ISS resupply mission launch has been postponed due to a problem with the vehicle. Another attempt has been scheduled for April 18.]&lt;/p&gt;&lt;p&gt;Zac Manchester, KD2BHC, of the KickSat project is offering prizes to the first stations to copy telemetry from KickSat and from the “Sprite” satellites.&lt;/p&gt;&lt;p&gt;“I’ll be offering prizes to the first several people who receive telemetr...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 17:44:00 -0400</pubDate></item>\n<item><title>FUNcube/AO-73 Transponder Will Be Active Each Weekend </title><link>http://www.arrl.org/news/view/funcube-ao-73-transponder-will-be-active-each-weekend</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/funcube-ao-73-transponder-will-be-active-each-weekend</guid><description>&lt;p&gt;The &lt;strong&gt;FUNcube/AO-73&lt;/strong&gt; Amateur Radio transponder will be active full time (with low-power beacon) on weekends. FUNcube’s controllers will switch to full-time transponder mode during the first suitable pass over the UK on Friday, typically between 1930 and 22:30 UTC. (If this is not possible, the switch will be made on the first suitable pass on Saturday, typically between 0930 and 1200 UTC. Controll...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 17:02:00 -0400</pubDate></item>\n<item><title>K4ZLE Wins April QST Cover Plaque Award</title><link>http://www.arrl.org/news/view/k4zle-wins-april-qst-cover-plaque-award</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/k4zle-wins-april-qst-cover-plaque-award</guid><description>&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;The winner of the April Q&lt;i&gt;ST&lt;/i&gt; Cover Plaque award is Jay Slough, K4ZLE, for his article “&lt;b&gt;A 10 Meter Moxon Antenna&lt;/b&gt;.”&lt;/p&gt;&lt;p&gt;The &lt;em&gt;QST &lt;/em&gt;Cover Plaque Award -- given to the author or authors of the most popular article in each issue -- is determined by a vote of ARRL members on the &lt;strong&gt;&lt;i&gt;QST&lt;/i&gt; Cover Plaque Poll web page&lt;/strong&gt;. Cast a ballot for your favorite article in the May issue today.&lt;/p&gt;&lt;p&gt; &lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 15:56:00 -0400</pubDate></item>\n<item><title>W1AW Centennial Operations Shift States on April 16 </title><link>http://www.arrl.org/news/view/w1aw-centennial-operations-shift-states-on-april-16</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/w1aw-centennial-operations-shift-states-on-april-16</guid><description>&lt;p&gt;The ARRL Centennial “&lt;strong&gt;&lt;span&gt;W1AW WAS&lt;/span&gt;&lt;/strong&gt;“ operations taking place throughout 2014 from each of the 50 states will relocate at 0000 UTC on Wednesday, April 16 (the evening of April 15 in US time zones), from Massachusetts and Virginia to Mississippi (W1AW/5) and North Dakota (W1AW/0). During 2014 W1AW will be on the air from every state (at least twice) and most US territories, and it will be easy to work ...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 12:06:00 -0400</pubDate></item>\n<item><title>Live Amateur Radio Digital TV Test Transmits Astronaut’s Image from the ISS</title><link>http://www.arrl.org/news/view/live-amateur-radio-digital-tv-test-transmits-astronaut-s-image-from-the-iss</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/live-amateur-radio-digital-tv-test-transmits-astronaut-s-image-from-the-iss</guid><description>&lt;p&gt;Japanese Astronaut Koichi Wakata, KC5ZTA, served as the host for a successful final commissioning pass April 13 for the Amateur Radio on the International Space Station (&lt;strong&gt;ARISS&lt;/strong&gt;) “&lt;strong&gt;Ham Video&lt;/strong&gt;” transmitter and camera. Operating as OR4ISS from the ISS &lt;i&gt;Columbus&lt;/i&gt; module, Wakata transmitted digital video and audio, as ground stations in France and Italy tracked the station. The Earth stations received cl...&lt;/p&gt;</description><creator /><pubDate>Sun, 13 Apr 2014 19:43:00 -0400</pubDate></item>\n<item><title>No ARRL Audio News for April 18</title><link>http://www.arrl.org/news/view/no-arrl-audio-news-for-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/no-arrl-audio-news-for-april-18</guid><description>&lt;p&gt;There will be no ARRL Audio News this Friday, April 18, due to the fact that Headquarters will be closed for the Good Friday holiday. Audio News will resume April 25.&lt;/p&gt;</description><creator /><pubDate>Sat, 12 Apr 2014 12:31:00 -0400</pubDate></item>\n<item><title>ARRL Asks FCC to Dismiss “Fatally Flawed” Petition for Rule Making Affecting 10 GHz</title><link>http://www.arrl.org/news/view/arrl-asks-fcc-to-dismiss-fatally-flawed-petition-for-rule-making-affecting-10-ghz</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/arrl-asks-fcc-to-dismiss-fatally-flawed-petition-for-rule-making-affecting-10-ghz</guid><description>&lt;p&gt;The ARRL has &lt;strong&gt;told the FCC&lt;/strong&gt; that a &lt;i&gt;Petition for Rule Making&lt;/i&gt; (&lt;b&gt;&lt;span&gt;RM-11715&lt;/span&gt;&lt;/b&gt;) from Mimosa Networks to permit unlicensed Part 15 wireless broadband services in the 10.0-10.5 GHz band is “fatally flawed” and should be dismissed. Mimosa filed the &lt;i&gt;Petition&lt;/i&gt; in May 2013, and the FCC invited public comments last month. The &lt;i&gt;Petition&lt;/i&gt; includes a band plan for 10.0 to 10.5 GHz that would cede Amateur Radio and Ama...&lt;/p&gt;</description><creator /><pubDate>Fri, 11 Apr 2014 20:37:00 -0400</pubDate></item>\n</channel></rss>\r\n\r\n<!DOCTYPE html>\r\n\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head><title>\r\n\r\n</title></head>\r\n<body>\r\n    <form method=\"post\" action=\"PostProxy.aspx?url=http%3a%2f%2fwww.arrl.org%2farrl.rss\" id=\"form1\">\r\n<div class=\"aspNetHidden\">\r\n<input type=\"hidden\" name=\"__VIEWSTATE\" id=\"__VIEWSTATE\" value=\"/wEPDwULLTE2MTY2ODcyMjlkZMBP10/lcSefrC6HbtbjlIan2bwG6sCcc1O/D5h2b1zJ\" />\r\n</div>\r\n\r\n    <div>\r\n    \r\n    </div>\r\n    </form>\r\n</body>\r\n</html>\r\n";
                    //_strXmlDocumentRSSFeed = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><rss version=\"2.0\"><channel><title>American Radio Relay League | Ham Radio Association and Resources</title><description>The American Radio Relay League (ARRL) is the national association for amateur radio, connecting hams around the U.S. with news, information and resources.</description><link>http://www.arrl.org</link><url>http://www.arrl.org</url><language>en</language><item><title>The K7RA Solar Update</title><link>http://www.arrl.org/news/view/the-k7ra-solar-update-318</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/the-k7ra-solar-update-318</guid><description>&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;ARRL Headquarters is closed for Good Friday on April 18, so the bulletin comes to you a day early.&lt;br /&gt;&lt;br /&gt;After a few days in the doldrums on April 8-13 with sunspot numbers in the double digits, solar activity made a strong recovery over the following three days, with daily sunspot numbers of 105, 149 and 245. Sunspot numbers have reached this level several times in the past 12 months. On February 28,...&lt;/p&gt;</description><creator /><pubDate>Thu, 17 Apr 2014 11:22:00 -0400</pubDate></item>\n<item><title>Amateur Radio Volunteers Provide Critical Support for 30th Challenge Cup Relay</title><link>http://www.arrl.org/news/view/amateur-radio-volunteers-provide-critical-support-for-30th-challenge-cup-relay</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/amateur-radio-volunteers-provide-critical-support-for-30th-challenge-cup-relay</guid><description>&lt;p&gt;Amateur Radio volunteers were key to maintaining safety and security for the thousands of law enforcement personnel who took part in the Baker to Vegas &lt;strong&gt;&lt;span&gt;Challenge Cup Relay&lt;/span&gt;&lt;/strong&gt; (B2V)&lt;span&gt;&lt;span&gt;&lt;/span&gt;&lt;/span&gt; race on March 22. This year marked the 30th anniversary of the event, sometimes called “The world’s longest police foot pursuit.” The relay event covers some 120 miles of remote territory, from the Mojave Desert near Ba...&lt;/p&gt;</description><creator /><pubDate>Wed, 16 Apr 2014 17:46:00 -0400</pubDate></item>\n<item><title>ARRL Headquarters Will Be Closed on Good Friday, April 18 </title><link>http://www.arrl.org/news/view/arrl-headquarters-will-be-closed-on-good-friday-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/arrl-headquarters-will-be-closed-on-good-friday-april-18</guid><description>&lt;p&gt;ARRL Headquarters will be closed on Good Friday, April 18. There will be no &lt;strong&gt;&lt;span&gt;W1AW bulletin or code practice transmissions&lt;/span&gt;&lt;/strong&gt; and no &lt;b&gt;&lt;i&gt;ARRL Audio News&lt;/i&gt;&lt;/b&gt; on April 18. ARRL Headquarters will reopen Monday, April 21, at 8 AM Eastern Daylight Time. We wish everyone a safe and enjoyable holiday!&lt;/p&gt;&lt;p&gt;&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 19:20:00 -0400</pubDate></item>\n<item><title>W1AW/x Portable Operations, W100AW, and W1HQ Can QSL via Bureau</title><link>http://www.arrl.org/news/view/w1aw-x-portable-operations-w100aw-and-w1hq-can-qsl-via-bureau</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/w1aw-x-portable-operations-w100aw-and-w1hq-can-qsl-via-bureau</guid><description>&lt;p&gt;You may request that QSL cards be delivered via the &lt;strong&gt;ARRL QSL Bureau system&lt;/strong&gt; for contacts with the W1AW/x portable operations, W100AW, and W1HQ, now underway during the ARRL Centennial. You must first have an account with the QSL Bureau that handles your cards and have sufficient postage or envelopes on file with that bureau. Any cards that cannot be delivered will not be held or stored.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Please do...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 19:13:00 -0400</pubDate></item>\n<item><title>World Amateur Radio Day is Friday, April 18</title><link>http://www.arrl.org/news/view/world-amateur-radio-day-is-friday-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/world-amateur-radio-day-is-friday-april-18</guid><description>&lt;p&gt;“Amateur Radio: Your Gateway to Wireless Communication” is the theme for World Amateur Radio Day 2014, Friday, April 18. Radio amateurs celebrate World Amateur Radio Day each year on April 18 to recognize the anniversary of the founding of the International Amateur Radio Union (&lt;strong&gt;&lt;span&gt;IARU&lt;/span&gt;&lt;/strong&gt;) in Paris in 1925. ARRL Co-Founder Hiram Percy Maxim, 1AW, was its first president. The primary purpose of World ...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 17:30:00 -0400</pubDate></item>\n<item><title>Deadline is May 1 to Apply for ARRL Teachers Institute</title><link>http://www.arrl.org/news/view/deadline-is-may-1-to-apply-for-arrl-teachers-institute</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/deadline-is-may-1-to-apply-for-arrl-teachers-institute</guid><description>&lt;p&gt;The ARRL Education &amp;amp; Technology Program (&lt;b&gt;ETP&lt;/b&gt;) is still accepting applications for all sessions of the ARRL Teachers Institute this summer. &lt;i&gt;The &lt;b&gt;application&lt;/b&gt; deadline is May 1. &lt;/i&gt;Admission decisions will be made by May 9.&lt;/p&gt;&lt;p&gt;Learn how to i&lt;span&gt;ntegrate science and mathematics with engineering and technology by exploring wireless technology! &lt;/span&gt;&lt;/p&gt;&lt;p&gt;Now in its 11th year, the ARRL Teachers Institute is a four-day, exp...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 17:22:00 -0400</pubDate></item>\n<item><title>Gear Up Now for ARRL Field Day 2014!</title><link>http://www.arrl.org/news/view/gear-up-now-for-arrl-field-day-2014</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/gear-up-now-for-arrl-field-day-2014</guid><description /><creator /><pubDate>Tue, 15 Apr 2014 13:45:00 -0400</pubDate></item>\n<item><title>Today’s National Hurricane Conference Amateur Radio Workshops Being Streamed Live</title><link>http://www.arrl.org/news/view/today-s-national-hurricane-conference-amateur-radio-workshops-being-streamed-live</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/today-s-national-hurricane-conference-amateur-radio-workshops-being-streamed-live</guid><description>&lt;p&gt;Amateur Radio workshop sessions at the National Hurricane Conference taking place today in Miami will be streamed live to the Web via the &lt;strong&gt;VoIP SKYWARN/Hurricane Net Support&lt;/strong&gt; and &lt;strong&gt;North Shore Radio Association&lt;/strong&gt; sites. The session schedule is listed on these links and below. Times are in Eastern Daylight Time:&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;10:30 Opening Remarks (John McHugh, K4AG)&lt;/p&gt;&lt;p&gt;10:35 Importance of Amateur Radio/Spotter Surface ...&lt;/p&gt;</description><creator /><pubDate>Tue, 15 Apr 2014 13:39:00 -0400</pubDate></item>\n<item><title>KickSat Project Announces Telemetry Download Competition</title><link>http://www.arrl.org/news/view/kicksat-project-announces-telemetry-download-competition</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/kicksat-project-announces-telemetry-download-competition</guid><description>&lt;p&gt;[Update: 2014-04-15 1905 UTC] The Spacex ISS resupply mission launch has been postponed due to a problem with the vehicle. Another attempt has been scheduled for April 18.]&lt;/p&gt;&lt;p&gt;Zac Manchester, KD2BHC, of the KickSat project is offering prizes to the first stations to copy telemetry from KickSat and from the “Sprite” satellites.&lt;/p&gt;&lt;p&gt;“I’ll be offering prizes to the first several people who receive telemetr...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 17:44:00 -0400</pubDate></item>\n<item><title>FUNcube/AO-73 Transponder Will Be Active Each Weekend </title><link>http://www.arrl.org/news/view/funcube-ao-73-transponder-will-be-active-each-weekend</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/funcube-ao-73-transponder-will-be-active-each-weekend</guid><description>&lt;p&gt;The &lt;strong&gt;FUNcube/AO-73&lt;/strong&gt; Amateur Radio transponder will be active full time (with low-power beacon) on weekends. FUNcube’s controllers will switch to full-time transponder mode during the first suitable pass over the UK on Friday, typically between 1930 and 22:30 UTC. (If this is not possible, the switch will be made on the first suitable pass on Saturday, typically between 0930 and 1200 UTC. Controll...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 17:02:00 -0400</pubDate></item>\n<item><title>K4ZLE Wins April QST Cover Plaque Award</title><link>http://www.arrl.org/news/view/k4zle-wins-april-qst-cover-plaque-award</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/k4zle-wins-april-qst-cover-plaque-award</guid><description>&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;The winner of the April Q&lt;i&gt;ST&lt;/i&gt; Cover Plaque award is Jay Slough, K4ZLE, for his article “&lt;b&gt;A 10 Meter Moxon Antenna&lt;/b&gt;.”&lt;/p&gt;&lt;p&gt;The &lt;em&gt;QST &lt;/em&gt;Cover Plaque Award -- given to the author or authors of the most popular article in each issue -- is determined by a vote of ARRL members on the &lt;strong&gt;&lt;i&gt;QST&lt;/i&gt; Cover Plaque Poll web page&lt;/strong&gt;. Cast a ballot for your favorite article in the May issue today.&lt;/p&gt;&lt;p&gt; &lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 15:56:00 -0400</pubDate></item>\n<item><title>W1AW Centennial Operations Shift States on April 16 </title><link>http://www.arrl.org/news/view/w1aw-centennial-operations-shift-states-on-april-16</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/w1aw-centennial-operations-shift-states-on-april-16</guid><description>&lt;p&gt;The ARRL Centennial “&lt;strong&gt;&lt;span&gt;W1AW WAS&lt;/span&gt;&lt;/strong&gt;“ operations taking place throughout 2014 from each of the 50 states will relocate at 0000 UTC on Wednesday, April 16 (the evening of April 15 in US time zones), from Massachusetts and Virginia to Mississippi (W1AW/5) and North Dakota (W1AW/0). During 2014 W1AW will be on the air from every state (at least twice) and most US territories, and it will be easy to work ...&lt;/p&gt;</description><creator /><pubDate>Mon, 14 Apr 2014 12:06:00 -0400</pubDate></item>\n<item><title>Live Amateur Radio Digital TV Test Transmits Astronaut’s Image from the ISS</title><link>http://www.arrl.org/news/view/live-amateur-radio-digital-tv-test-transmits-astronaut-s-image-from-the-iss</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/live-amateur-radio-digital-tv-test-transmits-astronaut-s-image-from-the-iss</guid><description>&lt;p&gt;Japanese Astronaut Koichi Wakata, KC5ZTA, served as the host for a successful final commissioning pass April 13 for the Amateur Radio on the International Space Station (&lt;strong&gt;ARISS&lt;/strong&gt;) “&lt;strong&gt;Ham Video&lt;/strong&gt;” transmitter and camera. Operating as OR4ISS from the ISS &lt;i&gt;Columbus&lt;/i&gt; module, Wakata transmitted digital video and audio, as ground stations in France and Italy tracked the station. The Earth stations received cl...&lt;/p&gt;</description><creator /><pubDate>Sun, 13 Apr 2014 19:43:00 -0400</pubDate></item>\n<item><title>No ARRL Audio News for April 18</title><link>http://www.arrl.org/news/view/no-arrl-audio-news-for-april-18</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/no-arrl-audio-news-for-april-18</guid><description>&lt;p&gt;There will be no ARRL Audio News this Friday, April 18, due to the fact that Headquarters will be closed for the Good Friday holiday. Audio News will resume April 25.&lt;/p&gt;</description><creator /><pubDate>Sat, 12 Apr 2014 12:31:00 -0400</pubDate></item>\n<item><title>ARRL Asks FCC to Dismiss “Fatally Flawed” Petition for Rule Making Affecting 10 GHz</title><link>http://www.arrl.org/news/view/arrl-asks-fcc-to-dismiss-fatally-flawed-petition-for-rule-making-affecting-10-ghz</link><guid isPermaLink=\"true\">http://www.arrl.org/news/view/arrl-asks-fcc-to-dismiss-fatally-flawed-petition-for-rule-making-affecting-10-ghz</guid><description>&lt;p&gt;The ARRL has &lt;strong&gt;told the FCC&lt;/strong&gt; that a &lt;i&gt;Petition for Rule Making&lt;/i&gt; (&lt;b&gt;&lt;span&gt;RM-11715&lt;/span&gt;&lt;/b&gt;) from Mimosa Networks to permit unlicensed Part 15 wireless broadband services in the 10.0-10.5 GHz band is “fatally flawed” and should be dismissed. Mimosa filed the &lt;i&gt;Petition&lt;/i&gt; in May 2013, and the FCC invited public comments last month. The &lt;i&gt;Petition&lt;/i&gt; includes a band plan for 10.0 to 10.5 GHz that would cede Amateur Radio and Ama...&lt;/p&gt;</description><creator /><pubDate>Fri, 11 Apr 2014 20:37:00 -0400</pubDate></item>\n</channel></rss>";



                    objXMLDocument.LoadXml(strXmlDocumentRSSFeed);



                }

                catch (System.Exception e)
                {
                    // Reformat Exception Message
                    throw new System.Exception("Failure to load RSS XML. : Err Description: " + e.ToString() + " Err Source: " + e.Source, e);
                }


                XsltArgumentList argsList = new XsltArgumentList();

                argsList.AddParam("RSS_Feed_Title", "", strRSSFeedTitle);

                objStringWriter = new System.IO.StringWriter();

                try
                {
                    objXSLTransform.Transform(objXMLDocument, argsList, objStringWriter);
                }

                catch (System.Exception e)
                {
                    // Reformat Exception Message
                    throw new System.Exception("Error, Failure to perform XSL Translation. : Err Description: " + e.ToString() + " Err Source: " + e.Source, e);
                }

                RSSFeedMarkup = objStringWriter.ToString();


            }

            catch (System.Exception e)
            {

                throw new Exception("RSS_Feed_ComponentController::RenderRSSFeed Error: " + e.Message);


            }

            finally
            {
                objXSLTransform = null;
                objXSLTDocument = null;
                objXMLDocument = null;

                objNameSpaceManager = null;
                objStringWriter = null;
            }


            return RSSFeedMarkup;

        }

        

    
        private string GetBaseUrl()
        {
            var request = HttpContext.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }


    }

   


}