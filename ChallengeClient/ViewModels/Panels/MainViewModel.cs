﻿using ChallengeClient.Helpers;
using ChallengeClient.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace ChallengeClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService navService;
        private AuthViewModel authViewModel;


        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ChallengeViewModel> AvailableItems { get; private set; }
        public ObservableCollection<MapPinViewModel> MapItems { get; private set; }

        private bool isDataLoaded;
        public bool IsDataLoaded
        {
            get { return this.isDataLoaded; }
            set { this.Set("IsDataLoaded", ref this.isDataLoaded, value); }
        }

        private RelayCommand loadedCommand;

        public RelayCommand LoadedCommand
        {
            get { return loadedCommand; }
            set { this.Set("LoadedCommand", ref this.loadedCommand, value); }
        }

        public LocationRectangle LocationView
        {
            get
            {
                if (this.MapItems != null && this.MapItems.Count > 0)
                {
                    return LocationRectangle.CreateBoundingRectangle(from l in this.MapItems select l.Pin.Location);
                }
                else
                {
                    return null;
                }
            }
        }

        public MainViewModel(INavigationService navService, AuthViewModel authViewModel)
        {
            this.AvailableItems = new ObservableCollection<ChallengeViewModel>();
            this.MapItems = new ObservableCollection<MapPinViewModel>();
            this.navService = navService;
            this.authViewModel = authViewModel;

            this.LoadedCommand = new RelayCommand(this.Load);            
        }

        private void Load()
        {
            if (!this.authViewModel.IsLoggedIn())
            {
                this.navService.NavigateTo(new Uri("/Pages/SetupPage.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            this.IsDataLoaded = false;
            this.AvailableItems = new ObservableCollection<ChallengeViewModel>();
            this.MapItems = new ObservableCollection<MapPinViewModel>();

            //TODO: unhardcode this going forward
            using (HttpResponseMessage response = await ServiceHelpers.DoGetRequest("1234"))
            {
                try
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.ReasonPhrase));
                    }

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(ChallengeResponse[]));
                    object objResponse = jsonSerializer.ReadObject(await response.Content.ReadAsStreamAsync());
                    ChallengeResponse[] jsonResponse = objResponse as ChallengeResponse[];

                    if (jsonResponse != null && jsonResponse.Count() > 0)
                    {
                        IList<Challenge> items = new List<Challenge>();

                        foreach (ChallengeResponse c in jsonResponse)
                        {
                            items.Add(new Challenge() { Id = c.Id.Oid, Address = c.FromPhone, Message = c.Message, Location = new GeoCoordinate() { Latitude = c.Latitude, Longitude = c.Longitude }, ProgressStatus = Status.Locked });
                        }

                        int orderAvailableItems = 0;

                        foreach (Challenge c in items)
                        {
                            if (c.ProgressStatus == Status.Locked)
                            {
                                c.Order = (orderAvailableItems++).ToString();
                                this.AvailableItems.Add(new ChallengeViewModel(c));

                                MapPinViewModel pinExists = this.MapItems.Where(x => x.Pin.Location == c.Location).FirstOrDefault();

                                if (pinExists != null)
                                {
                                    int index = this.MapItems.IndexOf(pinExists);
                                    this.MapItems[index].Pin.DisplayText += string.Format(", {0}", c.Order);
                                }
                                else
                                {
                                    this.MapItems.Add(new MapPinViewModel(new MapPin() { Id = c.Id, DisplayText = c.Order, Location = c.Location, Order = c.Order }));
                                }
                            }
                            else
                            {
                                //TODO: deal with items with other status differently (how?)
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    //update the map 
                    this.RaisePropertyChanged("LocationView");
                    this.RaisePropertyChanged("AvailableItems");
                    this.IsDataLoaded = true;
                }
            }
        }
    }
}