using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Web.Mvc;
using AutoMapper;
using Data_Q2.Models;
using Data_Q2.ViewModels;
using Newtonsoft.Json;

namespace Data_Q2.Controllers
{
    public class HomeController : Controller
    {
        private int avgResponseMinutes;

        public ActionResult Index()
        {
            Random ran = new Random();
            int i = ran.Next(1, 4);
            string FileName = "data" + i + ".json";
            List<TicketData> ticketDataList = new List<TicketData>();

            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/" + FileName)))
            {
                string json = sr.ReadToEnd();
                List<TicketData> items = JsonConvert.DeserializeObject<List<TicketData>>(json);
                ticketDataList = items;
            }

            int ticketCount = 0;
            foreach (var item in ticketDataList)
            {
                ticketCount += item.Ticket;
            }

            DashboardViewModel model = new DashboardViewModel();
            List<DashboardTicketData> listForView = new List<DashboardTicketData>();
          
            var mappingResult = mappingData(ticketDataList);


            listForView = mappingResult;
            model.FileName = FileName;
            model.AvgRspsTime = GetAvgRspsTime(GetAvgRspsMinute(listForView));
            model.TicketSum = ticketCount.ToString();
            model.TicketDataList = listForView;

            ViewBag.filename = model.FileName;
            ViewBag.avgrspstime = model.AvgRspsTime;
            ViewData["data"] = listForView;
            return View(model);
        }

      
        private object GetAvgRspsMinute(List<DashboardTicketData> listForView)
        {
            int totalRspsMinute = 0;

            for (int i = 0; i < listForView.Count; i++)
            {
                totalRspsMinute += listForView[i].ResponseMinutes;
            }

            int avgRspsMinute = totalRspsMinute / listForView.Count;

            return avgRspsMinute;
            throw new NotImplementedException();
        }

        private string GetAvgRspsTime(object p)
        {
            string avgRspsTime = "";

            if (avgResponseMinutes < 60) // 1小時內
            {
                avgRspsTime = avgResponseMinutes.ToString() + " minutes ";
            }
            else if (avgResponseMinutes >= 60 && avgResponseMinutes < 1440) // 1小時 - 1天內
            {
                avgRspsTime = GetAvgRspsHour(avgResponseMinutes) + " Hours "; 
            }
            else  // 1天以上
            {
                avgRspsTime = GetAvgRspsDay(avgResponseMinutes) + " Days "; 
            }

            return avgRspsTime;
            throw new NotImplementedException();
        }

        private string GetAvgRspsDay(int avgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(avgResponseMinutes);
            string avgRspsDay = Math.Round(ts.TotalDays).ToString(); // 取整數
            return avgRspsDay;
            throw new NotImplementedException();
        }

        private string GetAvgRspsHour(int avgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(avgResponseMinutes);
            string avgRspsHour = Math.Round(ts.TotalHours, 2).ToString();
            return avgRspsHour;
            throw new NotImplementedException();
        }

        private List<DashboardTicketData> mappingData(List<TicketData> source)
        {
            List<DashboardTicketData> result = new List<DashboardTicketData>();
            
            foreach (var item in source) 
            {
                DashboardTicketData data = new DashboardTicketData();
                data.Date = item.Date;
                data.Ticket = item.Ticket;
                data.ResponseMinutes = item.ResponseMinutes;
                result.Add(data);
            }
            
            return result;
        }
    }
}