using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduApp.Helpers
{
   public static class PalBrokerHtmlHelper
   {

      public static PalBrokerHelper PalBrokerHtml(this HtmlHelper helper)
      {
         return new PalBrokerHelper(helper);
      }
   }
}