using ChallengeClient.Helpers;
using ChallengeClient.Models;
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
        public MainViewModel()
        {
            this.CompletedItems = new ObservableCollection<ChallengeViewModel>();
            this.AvailableItems = new ObservableCollection<ChallengeViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ChallengeViewModel> CompletedItems { get; private set; }
        public ObservableCollection<ChallengeViewModel> AvailableItems { get; private set; }

        private bool isDataLoaded;
        public bool IsDataLoaded
        {
            get { return this.isDataLoaded; }
            set
            {
                this.isDataLoaded = value;
                this.NotifyPropertyChanged("IsDataLoaded");
            }
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            this.IsDataLoaded = false;
            this.AvailableItems = new ObservableCollection<ChallengeViewModel>();
            this.CompletedItems = new ObservableCollection<ChallengeViewModel>();

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
                        int orderCompletedItems = 0;

                        foreach (Challenge c in items)
                        {
                            if (c.ProgressStatus == Status.Locked)
                            {
                                c.Order = (orderAvailableItems++).ToString();
                                this.AvailableItems.Add(new ChallengeViewModel(c));
                            }
                            else
                            {
                                c.Order = (orderCompletedItems++).ToString();
                                this.CompletedItems.Add(new ChallengeViewModel(c));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    this.IsDataLoaded = true;
                }
            }
        }
    }
}