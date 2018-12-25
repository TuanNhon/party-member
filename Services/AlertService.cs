using System;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WebApp1.Data;
using WebApp1.Utilities;
using WebApp1.Services;
using Newtonsoft.Json;

namespace WebApp1.Services
{
    public class AlertService
    {
        private readonly ITempDataDictionary _tempData;
        public AlertService(IHttpContextAccessor contextAccessor, ITempDataDictionaryFactory tempData)
        {
            _tempData = tempData.GetTempData(contextAccessor.HttpContext);
        }
        public void Success(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }
        public void Infomation(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Infomation, message, dismissable);
        }
        public void Warning(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }
        public void Danger(string message, bool dismissable = true)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = _tempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)_tempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            // _tempData[Alert.TempDataKey] = alerts;
            _tempData.Put(Alert.TempDataKey, alerts);
        }

    }
}
