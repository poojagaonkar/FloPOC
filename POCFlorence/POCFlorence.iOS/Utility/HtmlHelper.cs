using System;
using System.Collections.Generic;
using System.Text;

namespace POCFlorence.iOS.Utilities
{
    public class HtmlHelper
    {
        public static string BuildHtml(string bodyText)
        {
			string boilerplateBeforeBody = @"<!DOCTYPE html>
                                            <html>
                                                <head>
                                                    <meta charset='utf-8'>
             
                                                    <title></title>
                                                    <meta name='description' content=''>
                                                    <meta name='HandheldFriendly' content='True'>
                                                    <meta name='MobileOptimized' content='480'>
                                                    <meta name='viewport' content='width=device-width, initial-scale=0, minimal-ui'>
                                                    <meta http-equiv='cleartype' content='on'>
                                                </head>
                                                <body> 
                                                <style>
                                                    img{ width:100% !important; height:auto !important; margin: 5px 0px!important;} 
                                                    iframe{ width:100% !important; height:auto !important; margin: 5px 0px!important;} 
                                                    a {  word-wrap: break-word; } *{ word-wrap: break-word;  text-align : left !important;}
                                                    p, span { font-family: sans-serif, serif,'Open Sans', Tahoma !important; font-size:16px ; text-align:left !important}
                                                </style>";

			string boilerplateAfterBody = "</body></html>";

			return boilerplateBeforeBody + bodyText + boilerplateAfterBody;

        }





        internal static string BuildCompleteViewHtml(string bodyText, string articleTitle, string imageName, bool isOrientationHorizontal = false)
        {
            

            var titleString = "<p class='top-title'>" + articleTitle;
            titleString = titleString + "</p>";

            var bodyImage = "<img src='"+imageName+"'>";

            string boilerplateBeforeBody = @"<!DOCTYPE html>
                                            <html>
                                                <head>
                                                    <meta charset='utf-8'>
                                                    <title></title>
                                                    <meta name='description' content=''>
                                                    <meta name='HandheldFriendly' content='True'>
                                                    <meta name='MobileOptimized' content='480'>
                                                    <meta name='viewport' content='width=device-width, initial-scale=1, minimal-ui'>
                                                    <meta http-equiv='cleartype' content='on'>
                                                </head>
                                                <body>
                                                    <style type='text/css'>
                                                        .top-title{font-family:'Roboto-Regular';font-size:18pt; line-height:18pt;}
                                                        html{margin:0;padding:0;}
                                                       
                                                        a{  word-wrap: break-word; }
                                                    </style>";

            string boilerplateAfterBody = "</body></html>";

            return boilerplateBeforeBody + titleString  + "<div>" + bodyText + "</div>"+ bodyImage +boilerplateAfterBody;
        }
    }
}
