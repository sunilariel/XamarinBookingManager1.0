﻿using Demo_App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditStaffPage : ContentPage
    {
        #region GlobleFields
        int StaffId;
        public Staff objStaff = null;
        #endregion

        public EditStaffPage()
        {
            GetEmployeeDetail();
            StaffId = Convert.ToInt32(Application.Current.Properties["SelectedEmployeeID"]);
            InitializeComponent();
            BindingContext = objStaff;
        }
        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            try
            {
                if (e.TotalY != 0 && Staffstack.HeightRequest > 30 && Staffstack.HeightRequest < 151)
                {
                    Staffstack.HeightRequest = Staffstack.HeightRequest + e.TotalY;
                    if (Staffstack.HeightRequest < 31)
                        Staffstack.HeightRequest = 31;
                    if (Staffstack.HeightRequest > 150)
                        Staffstack.HeightRequest = 150;
                }
                //stack.HeightRequest = 20;
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
        private void CrossClick(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
        }
        public void EditStaffInformation()
        {
            try
            {
                Staff obj = new Staff();
                obj.Id = StaffId;
                obj.CompanyId = Convert.ToInt32(Application.Current.Properties["CompanyId"]);
                obj.FirstName = EditStaffFirstName.Text;
                obj.LastName = EditStaffLastName.Text;

                obj.UserName = "";
                obj.Password = "";
                obj.Email = EditStaffEmail.Text;
                obj.TelephoneNo = EditStaffPhoneNumber.Text;
                obj.Address = EditStaffAddress.Text;
                obj.CreationDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

                var apiUrl = Application.Current.Properties["DomainUrl"] + "api/staff/Update";
                var serailizeddata = JsonConvert.SerializeObject(obj);

                var result = PostData("POST", serailizeddata, apiUrl);

                Navigation.PushAsync(new StaffProfileDetailsPage());
            }
            catch(Exception e)
            {
                e.ToString();
            }
        }
        public void GetEmployeeDetail()
        {
            try
            {
                var Url = Application.Current.Properties["DomainUrl"] + "api/staff/GetEmployeeById?id=" + Application.Current.Properties["SelectedEmployeeID"];
                var Method = "GET";
                var result = PostData(Method, "", Url);
                objStaff = JsonConvert.DeserializeObject<Staff>(result);
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }
        public string PostData(string Method, string SerializedData, string Url)
        {
            try
            {
                var result = "";
                HttpWebRequest httpRequest = HttpWebRequest.CreateHttp(Url);
                httpRequest.Method = Method;
                httpRequest.ContentType = "application/json";
                httpRequest.ProtocolVersion = HttpVersion.Version10;
                httpRequest.Headers.Add("Token", Convert.ToString(Application.Current.Properties["Token"]));

                if (SerializedData != "")
                {
                    var streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                    streamWriter.Write(SerializedData);
                    streamWriter.Close();
                }

                var httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();

                using (var StreamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    return result = StreamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}