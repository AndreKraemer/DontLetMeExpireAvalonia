﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DontLetMeExpireAvalonia.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;


namespace DontLetMeExpireAvalonia.ViewModels
{
    public partial class ShellViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool _flyoutIsPresented;

        [ObservableProperty]
        private ViewModelBase _content;

        [ObservableProperty]
        private FlyoutItem _activeFlyoutItem;

        public ObservableCollection<FlyoutItem> FlyoutItems { get; }

        public ShellViewModel()
        {

            FlyoutItems = new ObservableCollection<FlyoutItem>
            {
                new FlyoutItem(typeof(MainViewModel),"Home", "Home", []),
                new FlyoutItem(typeof(ItemViewModel),"dummy", "Item", []),
            };

            ActiveFlyoutItem = FlyoutItems.First();            
        }

        async partial void OnActiveFlyoutItemChanged(FlyoutItem value)
        {
            if (value is null) return;

            var vm = Design.IsDesignMode
                ? Activator.CreateInstance(value.ModelType)
                : Ioc.Default.GetService(value.ModelType);

            if (vm is not ViewModelBase vmb) return;

            await vmb.OnNavigatedToAsync(value.NavigationParameters);
            Content = vmb;
            FlyoutIsPresented = false;
        }

        [RelayCommand]
        private void ToggleFlyout() => FlyoutIsPresented = !FlyoutIsPresented;


    }
}
